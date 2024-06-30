using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // This will make the script run in the editor as well

public class CameraArm : MonoBehaviour
{
    [SerializeField] float armLength = 5.0f;
    [SerializeField] Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        child.position = transform.position - child.forward * armLength;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(child.position , transform.position);
    }
}
