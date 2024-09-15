using UnityEngine;

public class ObjCollision : MonoBehaviour 
{
    public bool once = true;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            KiteScore _playerScore = other.GetComponent<KiteScore>();

            Invoke("Destructor", 0.25f);
            _playerScore.kitesCollided();
            
            once = false;
            GameManager.instance.FailedLevel();

        }
    }

    public void Destuctor()
    {
        Destroy(GameObject.FindWithTag("Player"));
    }
}
