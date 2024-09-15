using UnityEngine;

public class CollectBalloon : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSys;
    public bool once = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && once)
        {
            particleSys.Play();

            AudioManager.instance.PlayAudio("Balloon");
            ScoreManager.Instance.Collectible = 1;

            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Renderer>().enabled = false;
            
            once = false;
        }
    }
}