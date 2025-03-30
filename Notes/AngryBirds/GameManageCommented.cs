using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance of the GameManager
    public static GameManager instance;

    // Maximum number of shots a player can take in the game
    public int MaxNumberOfShots = 3;

    // Keeps track of the number of shots used by the player
    private int _usedNumberOfShots;

    // Time to wait before checking if the game should end after the last shot
    [SerializeField] private float _secondsToWaitBeforeDeathCheck = 3f;

    // UI object to display when the game is over and the player needs to restart
    [SerializeField] private GameObject _restartScreenObject;

    // Reference to the SlingShotHandler to disable it upon game win
    [SerializeField] private SlingShotHandler _slingShotHandler;

    // IconHandler for updating shot icons when a shot is used
    private IconHandler _iconHandler;

    // List of all the piggies in the game
    private List<Piggie> _piggies = new List<Piggie>();

    // This method is called when the game starts (or when the scene is loaded)
    [System.Obsolete]
    public void Awake()
    {
        // Set the instance of GameManager if it doesn't exist
        if(instance == null)
        {
            instance = this;
        }

        // Find the IconHandler to update shot icons during the game
        _iconHandler = GameObject.FindFirstObjectByType<IconHandler>();

        // Find all Piggie objects in the scene and add them to the _piggies list
        Piggie[] piggies = FindObjectsOfType<Piggie>();
        for (int i = 0; i < piggies.Length; i++)
        {
            _piggies.Add(piggies[i]);
        }
    }

    /// <summary>
    /// Increments the number of shots used and updates the shot icons.
    /// Checks if the last shot was used and triggers game end checks.
    /// </summary>
    public void UsedShot()
    {
        _usedNumberOfShots++;  // Increment the used shots counter
        _iconHandler.useShot(_usedNumberOfShots);  // Update the shot icon UI

        // Check if the player has used all available shots
        CheckForLastShot();
    }

    /// <summary>
    /// Returns whether the player has enough shots remaining.
    /// </summary>
    public bool HasEnoughShots()
    {
        return _usedNumberOfShots < MaxNumberOfShots;
    }

    /// <summary>
    /// Checks if the player has used the last shot.
    /// If so, it starts a coroutine to check if the game is won or lost.
    /// </summary>
    public void CheckForLastShot()
    {
        if (_usedNumberOfShots == MaxNumberOfShots)
        {
            StartCoroutine(CheckAfterWaitTime());
        }
    }

    /// <summary>
    /// Waits a specified amount of time before checking if the game is won or lost.
    /// </summary>
    private IEnumerator CheckAfterWaitTime()
    {
        // Wait for the specified time before performing the check
        yield return new WaitForSeconds(_secondsToWaitBeforeDeathCheck);

        // If no piggies are left, the player wins; otherwise, the game restarts
        if (_piggies.Count == 0)
        {
            WinGame();
        }
        else
        {
            RestartGame();
        }
    }

    /// <summary>
    /// Removes a pig from the game and checks if all piggies have been defeated.
    /// </summary>
    public void RemovePiggie(Piggie baddie)
    {
        _piggies.Remove(baddie);  // Remove the pig from the list
        CheckForAllDeadPiggies();  // Check if all piggies are dead
    }

    /// <summary>
    /// Checks if all piggies are dead and if so, triggers the win condition.
    /// </summary>
    private void CheckForAllDeadPiggies()
    {
        if (_piggies.Count == 0)
        {
            WinGame();
        }
    }

    #region Win/Lose

    /// <summary>
    /// Triggered when the player wins the game. Displays the restart screen and disables the slingshot.
    /// </summary>
    private void WinGame()
    {
        _restartScreenObject.SetActive(true);  // Show the restart screen
        _slingShotHandler.enabled = false;    // Disable the slingshot handler to stop gameplay
    }

    /// <summary>
    /// Restarts the game by reloading the current scene.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }

    #endregion
}
