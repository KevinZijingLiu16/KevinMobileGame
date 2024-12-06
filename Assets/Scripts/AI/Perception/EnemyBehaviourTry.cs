using System.Collections;
using UnityEngine;

public class EnemyBehaviorTry : MonoBehaviour
{
    [SerializeField] Transform player; // 玩家 Transform
    [SerializeField] float projectileInterval = 2f; // 水球发射间隔
    [SerializeField] GameObject projectilePrefab1; // 水球1 Prefab (enemyball1)
    [SerializeField] GameObject projectilePrefab2; // 水球2 Prefab (enemyball2)
    [SerializeField] float projectileSpeed = 5f; // 水球速度

    private AlwaysAwareSense alwaysAwareSense;
    private bool isShooting = false; // 是否正在发射水球
    private int projectileCount = 0; // 计数器

    private void Start()
    {
        alwaysAwareSense = GetComponent<AlwaysAwareSense>();

        if (alwaysAwareSense != null)
            alwaysAwareSense.onPerceptionUpdated += OnAlwaysAwareSenseUpdated;
    }

    private void OnAlwaysAwareSenseUpdated(PerceptionStimuli stimuli, bool successfullySensed)
    {
        // 如果感知到玩家，开始发射水球
        if (stimuli.CompareTag("Player") && successfullySensed)
        {
            if (!isShooting)
            {
                StartCoroutine(ShootProjectiles());
            }
        }
        else
        {
            StopShooting();
        }
    }

    private void StopShooting()
    {
        isShooting = false;
        StopAllCoroutines(); // 停止所有协程，防止重复执行
    }

    private IEnumerator ShootProjectiles()
    {
        isShooting = true;

        while (isShooting)
        {
            if (player != null)
            {
                // 每 5 次发射 projectilePrefab1 后发射一次 projectilePrefab2
                GameObject projectilePrefab = (projectileCount % 5 == 0 && projectileCount != 0) ? projectilePrefab2 : projectilePrefab1;

                // 生成水球并朝玩家方向发射
                if (projectilePrefab != null)
                {
                    GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        Vector3 direction = (player.position - transform.position).normalized;
                        rb.velocity = direction * projectileSpeed;
                    }
                }

                // 更新计数器
                projectileCount++;
            }

            yield return new WaitForSeconds(projectileInterval); // 等待间隔
        }
    }
}
