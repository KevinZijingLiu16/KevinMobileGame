using UnityEngine;

public class FollowAndFacePlayer : MonoBehaviour
{
    [SerializeField] Transform player; // ��Ҷ���� Transform
    [SerializeField] float followDistance = 5f; // ����ľ���
    [SerializeField] float followHeight = 2f; // ����ĸ߶�
    [SerializeField] float followSpeed = 5f; // �����ƶ����ٶ�
    [SerializeField] float rotationSpeed = 5f; // ��ת�ٶ�

    private Vector3 offset; // �����ƫ��λ��

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        // ��ʼ��ƫ��ΪĬ�ϵĸ������͸߶�
        offset = new Vector3(0, followHeight, -followDistance);
    }

    private void LateUpdate()
    {
        if (player == null) return;

        FollowPlayer();
        FacePlayer();
    }

    private void FollowPlayer()
    {
        // ��������λ��
        Vector3 targetPosition = player.position + player.rotation * offset;

        // ƽ���ƶ���Ŀ��λ��
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void FacePlayer()
    {
        // ����������ҵķ���
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // ���ɳ�����ҵ���ת
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // ƽ����ת��Ŀ�귽��
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
