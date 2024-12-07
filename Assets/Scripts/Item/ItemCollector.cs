using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collectedCountText; // 显示收集数目的 UI Text
    [SerializeField] CanvasGroup winScreenCanvasGroup; // 胜利屏幕 UI 的 CanvasGroup
    [SerializeField] TextMeshProUGUI winMessageText; // 胜利信息文本
    [SerializeField] float fadeDuration = 2f; // 黑屏淡入时间

    private int collectedCount = 0; // 收集的物品数目
    private float startTime; // 游戏开始时间
    private bool hasWon = false; // 是否已胜利

    [SerializeField] AudioSource collectAudio;

    void Start()
    {
        // 初始化 UI
        collectedCountText.text = "Items Collected: 0/5";
        winScreenCanvasGroup.alpha = 0;
        winMessageText.text = "";

        // 记录游戏开始时间
        startTime = Time.time;

        collectAudio = GetComponent<AudioSource>();
    }

    

    void OnTriggerEnter(Collider other)
    {
        // 检测碰撞的物品是否是可收集的物品
        if (other.CompareTag("Collectible"))
        {
            collectAudio.Play();
            collectedCount++;
            collectedCountText.text = $"Items Collected: {collectedCount}/5";

            // 销毁被收集的物品
            Destroy(other.gameObject);

            // 检查胜利条件
            if (collectedCount >= 5 && !hasWon)
            {
                hasWon = true;
                StartCoroutine(ShowWinScreen());
            }
        }
    }

    private IEnumerator ShowWinScreen()
    {
        // 计算所花费的时间
        float totalTime = Time.time - startTime;

        // 黑屏淡入
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            winScreenCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        // 显示胜利信息
        winMessageText.text = $"You Win\nTime Taken: {totalTime:F1} seconds";

        // 等待 3 秒后重新加载场景
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
