using UnityEngine;

public class RotateBalloon : MonoBehaviour
{
    public int rotatespeed = 1;
    public float speed = 1f;
    public float amplitude = 0.5f;

    private Vector3 tempPos;
    private float tempValue;
    
    void Start()
    {
        tempPos = transform.position;
        tempValue = transform.position.y; 
    }

    void Update()
    {
        tempPos.y = tempValue + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;   
    }
}
