using UnityEngine;
using UnityEngine.UI;

public class IconHandler : MonoBehaviour
{
     // Array of UI Image components representing the shots available
     [SerializeField] private Image[] _icons;

     // Color to indicate a used shot
     [SerializeField] private Color _usedColour;

     /// <summary>
     /// Updates the shot icon to indicate that a shot has been used.
     /// </summary>
     /// <param name="shotNumber">The number of the shot that was used.</param>
     public void useShot(int shotNumber)
     {
          // Loop through all icons and find the one that corresponds to the used shot
          for (int i = 0; i < _icons.Length; i++)
          {
               if (shotNumber == i + 1) // Check if the shotNumber matches the current index (+1 since array is 0-based)
               {
                    // Change the icon color to indicate that the shot has been used
                    _icons[i].color = _usedColour;
                    return; // Exit the loop early since the required shot has been updated
               }
          }
     }
}
