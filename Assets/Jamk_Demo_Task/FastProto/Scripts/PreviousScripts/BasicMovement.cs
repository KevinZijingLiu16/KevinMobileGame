using System.Collections;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool isGrounded;
    public ParticleSystem particles; // Reference to the Particle System
    [SerializeField] float particleEmissionRate = 10f;
    //[SerializeField] AudioSource jumpSound;
    private Rigidbody rb;
    private bool isRotating; // Flag to indicate if rotating

    private void Start()
    {
       // jumpSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        particles = GetComponentInChildren<ParticleSystem>(); // Assign the Particle System
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movementDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

        isGrounded = Physics.CheckSphere(groundCheck.position, 1f, groundMask); // Adjust the radius as needed

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //jumpSound.Play();
        }

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

        // Control particle system emission based on player movement
        if (movementDirection.magnitude > 0)
        {
            
            var emission = particles.emission;
            emission.rateOverTime = particleEmissionRate;
        }
        else
        {
            
            var emission = particles.emission;
            emission.rateOverTime = 0f;
        }

        
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Calculate rotation towards camera's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
           

            StartCoroutine(DestroyEnemy(collision.gameObject));
        }
    }

    IEnumerator DestroyEnemy(GameObject enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(2f);

        Destroy(enemy);
    }
}
