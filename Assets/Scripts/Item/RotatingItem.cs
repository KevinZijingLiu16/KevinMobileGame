using UnityEngine;

public class RotatingItem : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f; 

    void Update()
    {
        // �� Y ����ת
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
