using System.Collections;
using UnityEngine;

public class EnemyBehaviorTry : MonoBehaviour
{
    [SerializeField] Transform player; // ��� Transform
    [SerializeField] float projectileInterval = 2f; // ˮ������
    [SerializeField] GameObject projectilePrefab1; // ˮ��1 Prefab (enemyball1)
    [SerializeField] GameObject projectilePrefab2; // ˮ��2 Prefab (enemyball2)
    [SerializeField] float projectileSpeed = 5f; // ˮ���ٶ�

    private AlwaysAwareSense alwaysAwareSense;
    private bool isShooting = false; // �Ƿ����ڷ���ˮ��
    private int projectileCount = 0; // ������

    private void Start()
    {
        alwaysAwareSense = GetComponent<AlwaysAwareSense>();

        if (alwaysAwareSense != null)
            alwaysAwareSense.onPerceptionUpdated += OnAlwaysAwareSenseUpdated;
    }

    private void OnAlwaysAwareSenseUpdated(PerceptionStimuli stimuli, bool successfullySensed)
    {
        // �����֪����ң���ʼ����ˮ��
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
        StopAllCoroutines(); // ֹͣ����Э�̣���ֹ�ظ�ִ��
    }

    private IEnumerator ShootProjectiles()
    {
        isShooting = true;

        while (isShooting)
        {
            if (player != null)
            {
                // ÿ 5 �η��� projectilePrefab1 ����һ�� projectilePrefab2
                GameObject projectilePrefab = (projectileCount % 5 == 0 && projectileCount != 0) ? projectilePrefab2 : projectilePrefab1;

                // ����ˮ�򲢳���ҷ�����
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

                // ���¼�����
                projectileCount++;
            }

            yield return new WaitForSeconds(projectileInterval); // �ȴ����
        }
    }
}
