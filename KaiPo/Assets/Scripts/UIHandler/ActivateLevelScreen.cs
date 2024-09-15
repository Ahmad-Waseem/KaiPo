using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateLevelScreen: MonoBehaviour
{
    public void Level(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}