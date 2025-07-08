using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ComplexMatrixOperations
{
     // Complex number class
     public class ComplexNumber
     {
          public double Real { get; set; }
          public double Imaginary { get; set; }

          public ComplexNumber(double real, double imaginary)
          {
               Real = real;
               Imaginary = imaginary;
          }

          // Add two complex numbers
          public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
          {
               return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
          }

          // Subtract two complex numbers
          public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
          {
               return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
          }

          // Multiply two complex numbers
          public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
          {
               double realPart = a.Real * b.Real - a.Imaginary * b.Imaginary;
               double imaginaryPart = a.Real * b.Imaginary + a.Imaginary * b.Real;
               return new ComplexNumber(realPart, imaginaryPart);
          }

          // Divide two complex numbers
          public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
          {
               double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
               double realPart = (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator;
               double imaginaryPart = (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator;
               return new ComplexNumber(realPart, imaginaryPart);
          }

          // Magnitude of the complex number
          public double Magnitude()
          {
               return Math.Sqrt(Real * Real + Imaginary * Imaginary);
          }

          // Conjugate of the complex number
          public ComplexNumber Conjugate()
          {
               return new ComplexNumber(Real, -Imaginary);
          }

          // Display complex number as a string
          public override string ToString()
          {
               return $"{Real} + {Imaginary}i";
          }
     }

     // Matrix class that handles complex numbers
     public class Matrix
     {
          private ComplexNumber[,] _matrix;

          public int Rows => _matrix.GetLength(0);
          public int Columns => _matrix.GetLength(1);

          public Matrix(int rows, int columns)
          {
               _matrix = new ComplexNumber[rows, columns];
          }

          // Indexer for matrix access
          public ComplexNumber this[int row, int col]
          {
               get => _matrix[row, col];
               set => _matrix[row, col] = value;
          }

          // Matrix addition
          public static Matrix operator +(Matrix a, Matrix b)
          {
               if (a.Rows != b.Rows || a.Columns != b.Columns)
                    throw new InvalidOperationException("Matrix dimensions must match for addition.");

               Matrix result = new Matrix(a.Rows, a.Columns);
               for (int i = 0; i < a.Rows; i++)
               {
                    for (int j = 0; j < a.Columns; j++)
                    {
                         result[i, j] = a[i, j] + b[i, j];
                    }
               }
               return result;
          }

          // Matrix multiplication by scalar
          public static Matrix operator *(Matrix m, ComplexNumber scalar)
          {
               Matrix result = new Matrix(m.Rows, m.Columns);
               for (int i = 0; i < m.Rows; i++)
               {
                    for (int j = 0; j < m.Columns; j++)
                    {
                         result[i, j] = m[i, j] * scalar;
                    }
               }
               return result;
          }

          // Matrix multiplication
          public static Matrix operator *(Matrix a, Matrix b)
          {
               if (a.Columns != b.Rows)
                    throw new InvalidOperationException("Matrix dimensions must match for multiplication.");

               Matrix result = new Matrix(a.Rows, b.Columns);
               for (int i = 0; i < a.Rows; i++)
               {
                    for (int j = 0; j < b.Columns; j++)
                    {
                         ComplexNumber sum = new ComplexNumber(0, 0);
                         for (int k = 0; k < a.Columns; k++)
                         {
                              sum += a[i, k] * b[k, j];
                         }
                         result[i, j] = sum;
                    }
               }
               return result;
          }

          // Transpose the matrix
          public Matrix Transpose()
          {
               Matrix result = new Matrix(Columns, Rows);
               for (int i = 0; i < Rows; i++)
               {
                    for (int j = 0; j < Columns; j++)
                    {
                         result[j, i] = _matrix[i, j];
                    }
               }
               return result;
          }

          // Display matrix as string
          public override string ToString()
          {
               string result = "";
               for (int i = 0; i < Rows; i++)
               {
                    for (int j = 0; j < Columns; j++)
                    {
                         result += _matrix[i, j].ToString() + "\t";
                    }
                    result += "\n";
               }
               return result;
          }
     }

     class Program
     {
          static void Main(string[] args)
          {
               // Complex number operations
               ComplexNumber c1 = new ComplexNumber(3, 2);
               ComplexNumber c2 = new ComplexNumber(1, 4);

               Console.WriteLine($"c1 = {c1}");
               Console.WriteLine($"c2 = {c2}");
               Console.WriteLine($"c1 + c2 = {c1 + c2}");
               Console.WriteLine($"c1 - c2 = {c1 - c2}");
               Console.WriteLine($"c1 * c2 = {c1 * c2}");
               Console.WriteLine($"c1 / c2 = {c1 / c2}");
               Console.WriteLine($"Magnitude of c1 = {c1.Magnitude()}");
               Console.WriteLine($"Conjugate of c1 = {c1.Conjugate()}");

               // Matrix operations
               Matrix m1 = new Matrix(2, 2);
               m1[0, 0] = new ComplexNumber(1, 2);
               m1[0, 1] = new ComplexNumber(3, 4);
               m1[1, 0] = new ComplexNumber(5, 6);
               m1[1, 1] = new ComplexNumber(7, 8);

               Matrix m2 = new Matrix(2, 2);
               m2[0, 0] = new ComplexNumber(9, 10);
               m2[0, 1] = new ComplexNumber(11, 12);
               m2[1, 0] = new ComplexNumber(13, 14);
               m2[1, 1] = new ComplexNumber(15, 16);

               Console.WriteLine("\nMatrix m1:");
               Console.WriteLine(m1);

               Console.WriteLine("Matrix m2:");
               Console.WriteLine(m2);

               Console.WriteLine($"m1 + m2 = \n{m1 + m2}");
               Console.WriteLine($"m1 * m2 = \n{m1 * m2}");
               Console.WriteLine($"Transpose of m1 = \n{m1.Transpose()}");

               // Matrix multiplication with a complex scalar
               ComplexNumber scalar = new ComplexNumber(2, 3);
               Console.WriteLine($"m1 * scalar = \n{m1 * scalar}");
          }
     }
}


// Explanation:

// 1.ComplexNumber Class:
// This class models a complex number with two properties: Real and Imaginary.

// It supports arithmetic operations such as addition, subtraction, multiplication, and division.

// It also includes methods to calculate the magnitude and the conjugate of the complex number.

// 2. Matrix Class:
// This class represents a matrix of complex numbers.

// Supports matrix operations like addition, scalar multiplication, multiplication between matrices, and matrix transposition.

// The matrix is represented as a 2D array of ComplexNumber objects.

// 3. Program Class:
// The main program demonstrates how to use the ComplexNumber and Matrix classes.

// It creates two complex numbers and performs arithmetic operations.

// It creates two 2x2 matrices of complex numbers and demonstrates matrix addition, multiplication, and transposition.







namespace LibraryManagementSystem
{
     // Base class representing an entity in the library system
     public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

     // Author class, inheriting from LibraryEntity
     public class Author : LibraryEntity
     {
          public string Biography { get; set; }

          public Author(int id, string title, string biography) : base(id, title)
          {
               Biography = biography;
          }

          public override void DisplayInfo()
          {
               Console.WriteLine($"Author: {Title}");
               Console.WriteLine($"Biography: {Biography}");
          }
     }

     // Book class, inheriting from LibraryEntity
     public class Book : LibraryEntity
     {
          public Author BookAuthor { get; set; }
          public string Genre { get; set; }
          public bool IsAvailable { get; set; }

          public Book(int id, string title, Author author, string genre) : base(id, title)
          {
               BookAuthor = author;
               Genre = genre;
               IsAvailable = true;
          }

          public override void DisplayInfo()
          {
               Console.WriteLine($"Book: {Title} (Genre: {Genre})");
               Console.WriteLine($"Author: {BookAuthor.Title}");
               Console.WriteLine($"Availability: {(IsAvailable ? "Available" : "Not Available")}");
          }
     }

     // User class that interacts with the library system
     public class User
     {
          public string Name { get; set; }
          public List<Book> BorrowedBooks { get; private set; }

          public User(string name)
          {
               Name = name;
               BorrowedBooks = new List<Book>();
          }

          public void BorrowBook(Book book)
          {
               if (book.IsAvailable)
               {
                    book.IsAvailable = false;
                    BorrowedBooks.Add(book);
                    Console.WriteLine($"{Name} has borrowed the book '{book.Title}'.");
               }
               else
               {
                    Console.WriteLine($"Sorry, '{book.Title}' is currently not available.");
               }
          }

          public void ReturnBook(Book book)
          {
               if (BorrowedBooks.Contains(book))
               {
                    book.IsAvailable = true;
                    BorrowedBooks.Remove(book);
                    Console.WriteLine($"{Name} has returned the book '{book.Title}'.");
               }
               else
               {
                    Console.WriteLine($"{Name} did not borrow the book '{book.Title}'.");
               }
          }

          public void DisplayBorrowedBooks()
          {
               if (BorrowedBooks.Count == 0)
               {
                    Console.WriteLine($"{Name} has no borrowed books.");
               }
               else
               {
                    Console.WriteLine($"{Name}'s borrowed books:");
                    foreach (var book in BorrowedBooks)
                    {
                         Console.WriteLine($"- {book.Title}");
                    }
               }
          }
     }

     // Library class to manage books, authors, and users
     public class Library
     {
          private List<Book> _books;
          private List<Author> _authors;
          private List<User> _users;

          // Events to notify when a book is borrowed or returned
          public event Action<Book> BookBorrowed;
          public event Action<Book> BookReturned;

          public Library()
          {
               _books = new List<Book>();
               _authors = new List<Author>();
               _users = new List<User>();
          }

          // Add a book to the library
          public void AddBook(Book book)
          {
               _books.Add(book);
               Console.WriteLine($"Book '{book.Title}' added to the library.");
          }

          // Add an author to the library system
          public void AddAuthor(Author author)
          {
               _authors.Add(author);
               Console.WriteLine($"Author '{author.Title}' added.");
          }

          // Add a user to the library
          public void AddUser(User user)
          {
               _users.Add(user);
               Console.WriteLine($"User '{user.Name}' added to the library system.");
          }

          // Search books by title or genre using LINQ
          public List<Book> SearchBooks(string keyword)
          {
               var searchResults = _books.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                                       b.Genre.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

               Console.WriteLine($"Search results for '{keyword}':");
               foreach (var book in searchResults)
               {
                    book.DisplayInfo();
               }
               return searchResults;
          }

          // Borrow a book by user
          public void BorrowBook(User user, Book book)
          {
               user.BorrowBook(book);
               BookBorrowed?.Invoke(book);  // Notify subscribers about the book being borrowed
          }

          // Return a borrowed book by user
          public void ReturnBook(User user, Book book)
          {
               user.ReturnBook(book);
               BookReturned?.Invoke(book);  // Notify subscribers about the book being returned
          }

          // Display all books
          public void DisplayAllBooks()
          {
               Console.WriteLine("All Books in Library:");
               foreach (var book in _books)
               {
                    book.DisplayInfo();
               }
          }
     }

     // Exception class for custom library errors
     public class LibraryException : Exception
     {
          public LibraryException(string message) : base(message) { }
     }

     class Program
     {
          static void Main(string[] args)
          {
               try
               {
                    // Initialize library system
                    var library = new Library();

                    // Adding authors
                    var author1 = new Author(1, "J.K. Rowling", "British author, best known for the Harry Potter series.");
                    var author2 = new Author(2, "J.R.R. Tolkien", "English writer, known for The Lord of the Rings series.");
                    library.AddAuthor(author1);
                    library.AddAuthor(author2);

                    // Adding books
                    var book1 = new Book(101, "Harry Potter and the Sorcerer's Stone", author1, "Fantasy");
                    var book2 = new Book(102, "The Hobbit", author2, "Fantasy");
                    library.AddBook(book1);
                    library.AddBook(book2);

                    // Add a user
                    var user = new User("Alice");
                    library.AddUser(user);

                    // Subscribing to events
                    library.BookBorrowed += (book) => Console.WriteLine($"Event: '{book.Title}' has been borrowed.");
                    library.BookReturned += (book) => Console.WriteLine($"Event: '{book.Title}' has been returned.");

                    // Searching for books
                    library.SearchBooks("Harry");

                    // Borrow and return books
                    library.BorrowBook(user, book1);
                    user.DisplayBorrowedBooks();
                    library.ReturnBook(user, book1);
                    user.DisplayBorrowedBooks();

                    // Demonstrating file handling (writing borrowed books to a file)
                    File.WriteAllText("borrowed_books.txt", $"User: {user.Name}\nBooks Borrowed: {string.Join(", ", user.BorrowedBooks.Select(b => b.Title))}");
                    Console.WriteLine("Borrowed books written to 'borrowed_books.txt'");
               }
               catch (LibraryException ex)
               {
                    Console.WriteLine($"Library Error: {ex.Message}");
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
               }
          }
     }
}


//ERRR

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }

public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }


public abstract class LibraryEntity
     {
          public int Id { get; set; }
          public string Title { get; set; }

          protected LibraryEntity(int id, string title)
          {
               Id = id;
               Title = title;
          }

          public abstract void DisplayInfo();
     }
