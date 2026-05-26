using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Chest : MonoBehaviour
{
    [Header("Chest Sprites")]
    public Sprite openChestSprite;

    [Header("Win Panel")]
    public GameObject winPanel;
    public TextMeshProUGUI coinsText;

    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpened)
        {
            isOpened = true;
            OpenChest();
        }
    }

    void OpenChest()
    {
        if (openChestSprite != null)
            GetComponent<SpriteRenderer>().sprite = openChestSprite;

        if (coinsText != null)
            coinsText.text = "Coins collected: " + PlayerManager.numberOfCoins;

        PlayerPrefs.SetInt("NumberOfCoins", PlayerManager.numberOfCoins);
        PlayerPrefs.Save();

        if (winPanel != null)
            winPanel.SetActive(true);

        Time.timeScale = 0;

        StartCoroutine(GoToMenuAfterDelay());
    }

    IEnumerator GoToMenuAfterDelay()
    {
        // WaitForSecondsRealtime ignores timeScale = 0
        yield return new WaitForSecondsRealtime(5f);
        GoToMenu();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}