using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanelManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioListener audioListener;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        AudioManager.instance.PlayAudio("UI Swipe");
        Time.timeScale = 0f;
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        AudioManager.instance.PlayAudio("Button Click");
        Time.timeScale = 1f;
    }

    public void Sound()
    {
        audioListener.enabled = !(audioListener.enabled);
    }

    public void Home()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(0);
    }
}
