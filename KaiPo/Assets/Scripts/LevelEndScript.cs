using UnityEngine;

public class LevelEndScript : MonoBehaviour
{  
    void OnTriggerEnter()
    {
        GameManager.instance.CompleteLevel();
    }
}
