using UnityEngine;
using DG.Tweening;

public class KiteController : MonoBehaviour
{
    [Header("Joystick")]
    [SerializeField] Joystick joystick;
    [SerializeField] float thrust;
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] float forwardspeedMultiplier = 10f;
    [SerializeField] float forwardSpeed = 1f;
    [SerializeField] float horizonatalSpeed = 2.6f;
    [SerializeField] float verticalSpeed = 2f;
    [SerializeField] float smoothness = 5f;
    private float speedMultiplier = 200f;

    [SerializeField] float maxHorizontalRotation = 0.1f;
    [SerializeField] float maxVerticalRotation = 0.06f;
    [SerializeField] float rotationSmoothness = 5f;

    private float horizontalInput;
    private float verticalInput;

    [Header("Spawning Variables")]
    [SerializeField] int spawnLimit;
    [SerializeField] GameObject spawnKitePrefab;
    [SerializeField] Transform spawnedLeftArray;
    [SerializeField] Transform spawnedRightArray;
    private int spawnCount;
    private float spawningOffset = 40f;

    [Header("Random Generation Variables")]
    public float reptime =10;
    private int minRange = -10, maxRange = 10;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        horizonatalSpeed = 2.6f;

        // set kite's level
        gameObject.GetComponent<KiteScore>().KiteLevel = 1;
        InvokeRepeating("SpawnKites", 1f, reptime);
        InvokeRepeating("DestroyKites", 5f, 20f);
    }

    void Update()
    {
        if(Input.GetMouseButton(0) || Input.touches.Length != 0)
        {
            horizontalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;
        }
        else 
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
       HandleKiteRotation();
    }
    
    private void FixedUpdate()
    {
        if (gameObject.transform.position.x > LevelBoundary.leftSide && gameObject.transform.position.x < LevelBoundary.rightSide && gameObject.transform.position.y < LevelBoundary.up && gameObject.transform.position.y > LevelBoundary.down)
            HandleKiteMovement();
        
        if (gameObject.transform.position.x<LevelBoundary.leftSide)
            GetComponent<Rigidbody>().AddForce(thrust,0,0, ForceMode.Impulse);

        if (gameObject.transform.position.x>LevelBoundary.rightSide)
            GetComponent<Rigidbody>().AddForce(-thrust,0,0, ForceMode.Impulse);
        
        if (gameObject.transform.position.y<LevelBoundary.down)
            GetComponent<Rigidbody>().AddForce(0,thrust,0, ForceMode.Impulse);
    }
    
    private void HandleKiteMovement()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed * forwardspeedMultiplier * Time.deltaTime);
        float velocity_X = horizontalInput*speedMultiplier*horizonatalSpeed * Time.deltaTime;
        float velocity_Y = -verticalInput*speedMultiplier*verticalSpeed * Time.deltaTime;
        
        rb.velocity=Vector3.Lerp(rb.velocity, new Vector3(velocity_X, velocity_Y, rb.velocity.z), Time.deltaTime * smoothness);
    }
    
    private void HandleKiteRotation()
    {
        float horizontalRot = -horizontalInput * maxHorizontalRotation;
        float verticalRot = verticalInput * maxVerticalRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(verticalRot, transform.rotation.y, horizontalRot, transform.rotation.w), Time.deltaTime * rotationSmoothness);
    }

    #region SpawningFunction

    private void SpawnKites()
    {
        spawnCount += 1;
        if (spawnLimit <= spawnCount)
            CancelInvoke("SpawnKites");
       
        var tuple = gameObject.GetComponent<KiteScore>().RandomFunction(minRange, maxRange);

        // Left Kite Spawn
        GameObject leftSpawn = Instantiate(spawnKitePrefab, spawnedLeftArray);
        leftSpawn.GetComponent<KiteScore>().KiteLevel = tuple.Item1;
        leftSpawn.transform.DOMoveX(LevelBoundary.leftSide + 1, 3f).SetEase(Ease.Flash).From(LevelBoundary.leftSide - 5);
        leftSpawn.transform.position = new Vector3(LevelBoundary.leftSide + 2, spawnKitePrefab.transform.position.y+4, gameObject.transform.position.z + spawningOffset);

        // Right Kite Spawn
        GameObject rightSpawn = Instantiate(spawnKitePrefab, spawnedRightArray);
        rightSpawn.GetComponent<KiteScore>().KiteLevel = tuple.Item2;
        rightSpawn.transform.DOMoveX(LevelBoundary.rightSide - 2, 3f).SetEase(Ease.Flash).From(LevelBoundary.rightSide + 5);
        rightSpawn.transform.position = new Vector3(LevelBoundary.rightSide - 2, spawnKitePrefab.transform.position.y +4, gameObject.transform.position.z + spawningOffset + 0.5f);
    }

    private void DestroyKites()
    {
        if (spawnedLeftArray != null) {
            if (spawnedLeftArray.childCount >= 6)
            {
                for (int i=0; i < spawnedLeftArray.childCount - 4; i++) { 
                    Destroy(spawnedLeftArray.GetChild(i).gameObject); 
                }
            }
        }
        if (spawnedRightArray != null)
        {
            if (spawnedRightArray.childCount >= 6)
            {
                for (int i = 0; i < spawnedRightArray.childCount - 4; i++)
                    Destroy(spawnedRightArray.GetChild(i).gameObject);
            }
        }
    }

    #endregion SpawningFunction
}