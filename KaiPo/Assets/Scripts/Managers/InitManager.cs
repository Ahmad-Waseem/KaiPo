using UnityEngine;

public class InitManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject LevelScene;
    [SerializeField] GameObject MainMenuCanvas;
    [SerializeField] GameObject MainPanel;
    
    void Awake()
    {
        ActivatePanels();
        Initialization();
    }

    public void ActivatePanels()
    {
        LevelScene.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);
    }

    public void Initialization()
    {
        if (PlayerPrefs.HasKey("Restart"))
        {
            if (PlayerPrefs.GetInt("Restart") == 1)
            {
                // restart
                LevelScene.SetActive(true);
                MainMenuCanvas.SetActive(false);
                PlayerPrefs.SetInt("Restart", 0);
            }
        }

        if (PlayerPrefs.HasKey("ShowLevel"))
        {
            if (PlayerPrefs.GetInt("ShowLevel") == 1)
            {
                // settings < LevelSelection 
                MainPanel.SetActive(false);
                PlayerPrefs.SetInt("ShowLevel", 0);
            }
        }
    }

    #region PlayPanel

    public void PlayPanelInit()
    {
        AudioManager.instance.PlayAudio("Button Click");
        MainPanel.SetActive(false);
        LevelScene.SetActive(true);
    }

    #endregion PlayPanel
}
