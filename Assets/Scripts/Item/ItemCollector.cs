using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collectedCountText; // ��ʾ�ռ���Ŀ�� UI Text
    [SerializeField] CanvasGroup winScreenCanvasGroup; // ʤ����Ļ UI �� CanvasGroup
    [SerializeField] TextMeshProUGUI winMessageText; // ʤ����Ϣ�ı�
    [SerializeField] float fadeDuration = 2f; // ��������ʱ��

    private int collectedCount = 0; // �ռ�����Ʒ��Ŀ
    private float startTime; // ��Ϸ��ʼʱ��
    private bool hasWon = false; // �Ƿ���ʤ��

    [SerializeField] AudioSource collectAudio;

    void Start()
    {
        // ��ʼ�� UI
        collectedCountText.text = "Items Collected: 0/5";
        winScreenCanvasGroup.alpha = 0;
        winMessageText.text = "";

        // ��¼��Ϸ��ʼʱ��
        startTime = Time.time;

        collectAudio = GetComponent<AudioSource>();
    }

    

    void OnTriggerEnter(Collider other)
    {
        // �����ײ����Ʒ�Ƿ��ǿ��ռ�����Ʒ
        if (other.CompareTag("Collectible"))
        {
            collectAudio.Play();
            collectedCount++;
            collectedCountText.text = $"Items Collected: {collectedCount}/5";

            // ���ٱ��ռ�����Ʒ
            Destroy(other.gameObject);

            // ���ʤ������
            if (collectedCount >= 5 && !hasWon)
            {
                hasWon = true;
                StartCoroutine(ShowWinScreen());
            }
        }
    }

    private IEnumerator ShowWinScreen()
    {
        // ���������ѵ�ʱ��
        float totalTime = Time.time - startTime;

        // ��������
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            winScreenCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        // ��ʾʤ����Ϣ
        winMessageText.text = $"You Win\nTime Taken: {totalTime:F1} seconds";

        // �ȴ� 3 ������¼��س���
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
