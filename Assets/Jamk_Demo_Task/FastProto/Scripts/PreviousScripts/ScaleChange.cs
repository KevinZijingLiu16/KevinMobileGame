using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    public Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f); 
    public Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f); 
    public float scaleChangeSpeedMoving = 20f; 
    public float scaleChangeSpeedNotMoving = 10f;

    public Vector3 jumpingScale = new Vector3(0.8f, 0.8f, 1.2f);

    private Vector3 targetScale; 
    private Vector3 lastParentPosition;

    [SerializeField] private BasicMovement basicMovement;

    void Start()
    {
        
        targetScale = maxScale;
        
        lastParentPosition = transform.parent.position;
    }

    void Update()
    {
        
        if (transform.parent.position != lastParentPosition)
        {
            
            ChangeScale(scaleChangeSpeedMoving);
        }
        else
        {
            
            ChangeScale(scaleChangeSpeedNotMoving);
        }

       
        lastParentPosition = transform.parent.position;


        if (basicMovement.isGrounded == false)
        {
            transform.localScale = jumpingScale;
        }
    }

    void ChangeScale(float scaleChangeSpeed)
    {
        
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleChangeSpeed * Time.deltaTime);

       
        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            targetScale = (targetScale == minScale) ? maxScale : minScale;
        }
    }
}
