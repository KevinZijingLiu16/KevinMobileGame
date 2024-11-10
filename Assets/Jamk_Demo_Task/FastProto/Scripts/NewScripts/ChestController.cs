using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the Playable Director
    private bool isOpened = false;

    public GameObject panel; // The panel object
    public TextMeshProUGUI luckText; // Text component for the luck value
    [SerializeField] private float panelshowTime = 5f;
    private int luckValue;

    // Open the chest when the button is pressed
    public void OpenChest()
    {
        if (!isOpened)
        {
            playableDirector.Play(); // Play the timeline animation
            isOpened = true;
            Invoke("ShowLuckPanel", panelshowTime); // Delay for animation
        }
    }

    // Show the luck value and animate the panel
    public void ShowLuckPanel()
    {
        luckValue = Random.Range(0, 11) * 10; // Generate a value between 0 and 100 (steps of 10)
        luckText.text = "Your luck value today is " + luckValue + "%";

        // Save the luck value so it can be used in the next scene
        PlayerPrefs.SetInt("LuckValue", luckValue);
        PlayerPrefs.Save(); // Ensure the value is stored

        // Start the coroutine to animate the panel from small to large
        StartCoroutine(ScaleUpPanel());
    }

    // Coroutine to scale up the panel from 0 to its original size
    IEnumerator ScaleUpPanel()
    {
        // Ensure the panel starts from scale 0 (invisible)
        Vector3 originalScale = panel.transform.localScale;
        panel.transform.localScale = Vector3.zero;

        // Activate the panel (since it starts hidden)
        panel.SetActive(true);

        float elapsedTime = 0f;
        float duration = 1f; // You can adjust this duration to control how fast it scales up

        // Smoothly scale the panel from 0 to its original size
        while (elapsedTime < duration)
        {
            panel.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the panel is set to its original scale at the end of the animation
        panel.transform.localScale = originalScale;
    }

    // Optionally, you can add a method to ensure the panel starts hidden when the scene begins
    void Start()
    {
        panel.SetActive(false); // Make sure the panel is hidden when the scene starts
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("DemoPlayScene");
    }
}
