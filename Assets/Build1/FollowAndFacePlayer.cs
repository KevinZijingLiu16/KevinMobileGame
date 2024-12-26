using UnityEngine;

public class FollowAndFacePlayer : MonoBehaviour
{
    [SerializeField] Transform player; // 玩家对象的 Transform
    [SerializeField] float followDistance = 5f; // 跟随的距离
    [SerializeField] float followHeight = 2f; // 跟随的高度
    [SerializeField] float followSpeed = 5f; // 跟随移动的速度
    [SerializeField] float rotationSpeed = 5f; // 旋转速度

    private Vector3 offset; // 跟随的偏移位置

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        // 初始化偏移为默认的跟随距离和高度
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
        // 计算跟随的位置
        Vector3 targetPosition = player.position + player.rotation * offset;

        // 平滑移动到目标位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    private void FacePlayer()
    {
        // 计算面向玩家的方向
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // 生成朝向玩家的旋转
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // 平滑旋转到目标方向
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
