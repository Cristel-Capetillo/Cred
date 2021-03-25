using UnityEngine;

namespace Utilities {
    /// <summary>
    /// Add this script to any button with an OnClick event.
    /// Choose the button itself and in dropdown menu select this script
    /// and the public method ActivateOrDeactivate.
    /// Then drag in the game object that you want to toggle into the
    /// empty slot in the OnClick event.
    /// </summary>
    public class Toggler : MonoBehaviour
    {
        public void ActivateDeactivate(GameObject gameObject) {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}