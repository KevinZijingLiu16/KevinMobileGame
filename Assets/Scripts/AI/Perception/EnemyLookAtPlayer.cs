using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    [SerializeField] Transform player; // ��ҽ�ɫ�� Transform
    [SerializeField] float rotationSpeed = 5f; // ������ת�ٶ�
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // ���㳯����ҵķ���
            Vector3 direction = (player.position - transform.position).normalized;

            // ���ֵ��˴�ֱ����y�ᣩ���ı�
            direction.y = 0;

            // ����Ŀ����ת
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // ƽ����ת��Ŀ�귽��
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
