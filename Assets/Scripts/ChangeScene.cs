using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneByName(string sceneName) {
        SceneManager.LoadScene(sceneName); 
    }

    public void ChangeSceneByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}
