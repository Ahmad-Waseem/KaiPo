using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI GameViewLevelText;
    public GameObject GameViewCollectibleText;

    public GameObject levelCompletionPanel;
    public GameObject levelFailedPanel;

    public GameObject[] playerKite;
    public GameObject[] playercamera;

    public GameObject lvlCompleteText;
    public GameObject lvlFailText;

    [SerializeField] 

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        AudioManager.instance.PlayAudio("Background");
        AudioManager.instance.PlayAudio("Wind");
    }

    public void Start()
    {
        Time.timeScale = 1f;

        if (!PlayerPrefs.HasKey("SelectedKite"))
            PlayerPrefs.SetInt("SelectedKite", 0);

        playerKite[PlayerPrefs.GetInt("SelectedKite")].SetActive(true);
        playercamera[PlayerPrefs.GetInt("SelectedKite")].SetActive(true);
    }

    public void CompleteLevel()
    {
        LevelFinalization();
        levelCompletionPanel.SetActive(true);
        // AudioManager.instance.PlayAudio("Level Win");
    }

    public void FailedLevel()
    {
        LevelFinalization();
        levelFailedPanel.SetActive(true);
        AudioManager.instance.PlayAudio("Level Loss");
    }

    public void LevelFinalization()
    {
        Time.timeScale = 0f;
        PlayerPrefs.SetInt("CollectedBallon", ScoreManager.Instance.Collectible);
        ScoreManager.Instance.Score = Convert.ToInt32(playerKite[PlayerPrefs.GetInt("SelectedKite")].GetComponent<KiteScore>().KiteLevel);
        playerKite[PlayerPrefs.GetInt("SelectedKite")].transform.Find("Canvas").gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (currentLevel >= 6)
        {
            // randomized level
            int rnd = UnityEngine.Random.Range(0, 6);

            while (rnd == ScoreManager.Instance.CurrentLevel)
                rnd = UnityEngine.Random.Range(1, 6);
            
            rnd = rnd - 1;
            SceneManager.LoadScene(rnd);
        }
        else
        {
            SceneManager.LoadScene(currentLevel);
        }

        ScoreManager.Instance.CurrentLevel = currentLevel;
    }

    public void RestartLevel()
    { 
        PlayerPrefs.SetInt("Restart", 1);
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene(0);
    }
    
    public  void ActivateSettingPanel(bool active)
    {
       playerKite[PlayerPrefs.GetInt("SelectedKite")].transform.Find("Canvas").gameObject.SetActive(active);
    }

}
