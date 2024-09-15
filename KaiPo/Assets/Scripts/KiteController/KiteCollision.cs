using UnityEngine;

public class KiteCollision : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private KiteAnimationController _animationController;

    public ParticleSystem ColparticleSystem;
    public bool once = true;

    private void OnTriggerEnter(Collider other)
    {
        KiteScore _playerScore = other.GetComponent<KiteScore>();
        KiteScore _enemyScore = gameObject.GetComponent<KiteScore>();
        _animationController = new KiteAnimationController(_animator);

        if (_playerScore != null) 
        {
            if (_playerScore.KiteLevel > 0 && once)
            {
                transform.GetChild(2).gameObject.SetActive(false);

                AudioManager.instance.PlayAudio("Kite Collision");

                if (_playerScore.KiteLevel + _enemyScore.KiteLevel > 0)
                {
                    _playerScore.KiteLevel += _enemyScore.KiteLevel;
                    _playerScore.kitesCollided();

                    _animationController.PlayAnimation(AnimationType.RFA);
                    
                    Invoke("Destruct", 2.5f);
                    once = false;
                }
                else
                {
                    AudioManager.instance.PlayAudio("Kite Collision");

                    Invoke("DestructKite", 0f);
                    
                    GameManager.instance.FailedLevel();

                    _playerScore.kitesCollided();
                }

            }
        }

    }
    
    public void Destruct()
    {
        Destroy(gameObject);
    }
    
    public void DestructKite()
    {
        Destroy(GameObject.FindWithTag("Player"));
        GameManager.instance.FailedLevel();
    }
}

   
