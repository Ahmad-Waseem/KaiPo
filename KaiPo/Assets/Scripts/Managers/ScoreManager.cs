using UnityEngine.UI;

public class ScoreManager
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScoreManager();
            }
            return _instance;
        }
    }

    private int _CurrentLevel;
    public int CurrentLevel
    {
        get
        {
            return _CurrentLevel;
        }
        set
        {
            _CurrentLevel = value;
            GameManager.instance.GameViewLevelText.text = "Level " + _CurrentLevel.ToString();
        }
    }

    private int _Score;
    public int Score
    {
        get
        {
            return _Score;
        }
        set
        {
            _Score = value * Collectible;
            GameManager.instance.lvlCompleteText.GetComponent<Text>().text += value * Collectible;
            GameManager.instance.lvlFailText.GetComponent<Text>().text += value * Collectible;
        }
    }

    private int _Collectible;
    public int Collectible
    {
        get
        {
            return _Collectible;
        }
        set
        {
            _Collectible += value;
            GameManager.instance.GameViewCollectibleText.GetComponent<Text>().text = _Collectible.ToString();
        }
    }

    private ScoreManager()
    {
        CurrentLevel = 1;
        Collectible = 0;
        Score = 0;
    }

    public void ResetScore()
    {
        Collectible = 0;
        Score = 0;
    }
}
