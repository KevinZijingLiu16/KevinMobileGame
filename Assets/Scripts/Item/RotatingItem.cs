using UnityEngine;

public class RotatingItem : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f; 

    void Update()
    {
        // би Y жса§зЊ
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
