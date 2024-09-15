using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class KiteScore : MonoBehaviour
{
    public UnityEvent<KiteScore> onLevelIncDec;
    public Material hostileMaterial;

    private int kiteLevel;
    public int KiteLevel
    {
        get { return kiteLevel; }
        set
        {
            TextMeshProUGUI canvas = transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>();

            kiteLevel = value;
            if (kiteLevel < 0)
            {
                canvas.text = kiteLevel.ToString();
                transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                transform.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>().text = kiteLevel.ToString();
                Destroy(gameObject.GetComponentInChildren<MeshRenderer>().material);
                gameObject.GetComponentInChildren<MeshRenderer>().material = hostileMaterial;
            }
            else
                canvas.text = kiteLevel.ToString();
        }
    }

    //Get current kite level
    public int getKiteLevel()
    {
        return kiteLevel;
    }

    // ** random generation variable **
    private static Dictionary<string, int> weightTable = new Dictionary<string, int>() {
        {"LeftLower", 45},
        {"RightLower", 45},
    };
    // ********************************

    public void kitesCollided()
    {
        onLevelIncDec.Invoke(this); 
    }

    #region WeighedRandomProbability

    private string WeightedProbability()
    {
        int _totalWeight = 0;
        for (int i = 0; i < weightTable.Count; i++)
            _totalWeight += weightTable.ElementAt(i).Value;

        int _random = Random.Range(0, _totalWeight);
        float _runningTotal = 0;

        for (int i = 0; i < weightTable.Count; i++)
        {
            _runningTotal += weightTable.ElementAt(i).Value;

            if (_random < _runningTotal)
                return weightTable.ElementAt(i).Key;
        }

        return "";
    }

    /// <summary>
    /// either left or right kite will have a level greater than the player's kite.
    /// Func. will randomly decide which kite is at greater level or vice versa.
    /// </summary>
    /// <returns></returns>
    public System.Tuple<int, int> RandomFunction(int minRange, int maxRange)
    {
        string _func = WeightedProbability();

        int _kiteLevel = gameObject.GetComponent<KiteScore>().KiteLevel;
        maxRange = gameObject.GetComponent<KiteScore>().KiteLevel;
        int randomlvlgen = 0;


        int _leftEnemy = 0, _rightEnemy = 0;
        randomlvlgen = Random.Range(minRange , -1);

        switch (_func)//from weighted prop=> min range subracted to decrease <==========
        {
            // leftKite.Level < PlayerKite.Level && rightKite.Level > PlayerKite.Level
            case "LeftLower":
                _leftEnemy = Random.Range(minRange+randomlvlgen, ((-kiteLevel) - randomlvlgen));
                _rightEnemy = Random.Range(_kiteLevel+1, KiteLevel+20);
                break;

            // leftKite.Level > PlayerKite.Level && rightKite.Level < PlayerKite.Level
            case "RightLower":
                _leftEnemy = Random.Range(_kiteLevel, kiteLevel + 20);
                _rightEnemy = Random.Range(minRange + randomlvlgen, ((-kiteLevel)-randomlvlgen));
                break;

            //// leftKite.Level < PlayerKite.Level && rightKite.Level < PlayerKite.Level 
            //case "BothLower":
            //    _leftEnemy = Random.Range(minRange, _kiteLevel);
            //    _rightEnemy = Random.Range(minRange, _kiteLevel);
            //    break;

            //// leftKite.Level > PlayerKite.Level && rightKite.Level > PlayerKite.Level 
            //case "BothGreater":
            //    _leftEnemy = Random.Range(_kiteLevel, kiteLevel+50);
            //    _rightEnemy = Random.Range(_kiteLevel, maxRange+60);
            //    break;
        }

        return new System.Tuple<int, int>(_leftEnemy, _rightEnemy);
    }

    #endregion WeightedRandomProbability
}