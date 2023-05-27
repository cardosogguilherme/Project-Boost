using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);

        switch (other.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Fuel":
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    private void LoadNextLevel()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        var nextIndex = nextSceneIndex == SceneManager.sceneCountInBuildSettings ? 0 : nextSceneIndex;
        
        SceneManager.LoadScene(nextIndex);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
