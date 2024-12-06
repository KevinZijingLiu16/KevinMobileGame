using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    [SerializeField] Transform player; // 玩家角色的 Transform
    [SerializeField] float rotationSpeed = 5f; // 敌人旋转速度
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // 计算朝向玩家的方向
            Vector3 direction = (player.position - transform.position).normalized;

            // 保持敌人垂直方向（y轴）不改变
            direction.y = 0;

            // 计算目标旋转
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // 平滑旋转到目标方向
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
