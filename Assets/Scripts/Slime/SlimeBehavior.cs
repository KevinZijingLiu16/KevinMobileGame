using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public Transform player; // ��Ҷ���
    public float minFollowRadius = 3f; // ��С����뾶
    public float maxFollowRadius = 5f; // ������뾶
    public float moveSpeed = 2f; // ʷ��ķ�ƶ��ٶ�
    public GameObject waterBulletPrefab; // ˮ��Ԥ�Ƽ�
    public float fireRate = 0.5f; // ˮ��������
    public Transform firePoint; // �����

    public Vector3 minScale = new Vector3(0.9f, 0.9f, 0.9f); // ��С����
    public Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f); // �������
    public float scaleChangeSpeedMoving = 20f; // �ƶ�ʱ�����ٶ�
    public float scaleChangeSpeedNotMoving = 10f; // ����ʱ�����ٶ�
   // public Vector3 jumpingScale = new Vector3(0.8f, 0.8f, 1.2f); // ��Ծʱ����

    private Vector3 targetScale; // ��ǰĿ������
    private Vector3 lastPosition; // ��һ֡λ��
    private float nextFireTime = 0f;

    public bool isGrounded = true; // ������һ�������ж��Ƿ��ڵ�����

    void Start()
    {
        targetScale = maxScale;
        lastPosition = transform.position;
    }

    void Update()
    {
        FollowPlayer();

        if (Input.GetKey(KeyCode.Space))
        {
            Attack();
        }

        ScaleChangeLogic();
    }

    void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > maxFollowRadius)
        {
            MoveTowards(player.position);
        }
        else if (distanceToPlayer > minFollowRadius && distanceToPlayer <= maxFollowRadius)
        {
            Vector3 randomPosition = player.position + (Random.insideUnitSphere * (maxFollowRadius - minFollowRadius));
            randomPosition.y = transform.position.y;
            MoveTowards(randomPosition);
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // ���ڽӽ�Ŀ��λ��ʱ����ת
        if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // ƽ����ת
        }
    }


    void Attack()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            GameObject waterBullet = Instantiate(waterBulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = waterBullet.GetComponent<Rigidbody>();

            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Vector3 direction = (nearestEnemy.transform.position - firePoint.position).normalized;
                rb.velocity = direction * 10f;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    void ScaleChangeLogic()
    {
       
            float scaleChangeSpeed = (transform.position != lastPosition) ? scaleChangeSpeedMoving : scaleChangeSpeedNotMoving;
            ChangeScale(scaleChangeSpeed);
       
        lastPosition = transform.position;
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
