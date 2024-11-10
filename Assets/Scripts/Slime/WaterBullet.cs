using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 在这里处理敌人的伤害
            Debug.Log("Hit Enemy!");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); // 碰撞其他物体时销毁水弹
        }
    }
}
