using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // �����ﴦ����˵��˺�
            Debug.Log("Hit Enemy!");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); // ��ײ��������ʱ����ˮ��
        }
    }
}
