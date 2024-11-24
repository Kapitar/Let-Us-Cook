using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit() {
        Debug.Log("Quit the game.");
        Application.Quit();
    }
}
