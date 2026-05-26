using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public static int numberOfCoins;
    public static Vector2 lastCheckPointPos;

    [Header("Spawn")]
    public Vector2 levelStartPos = new Vector2(-3, 0); // set this in Inspector

    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public TextMeshProUGUI coinsText;
    public CinemachineVirtualCamera VCam;
    public GameObject[] playerPrefabs;

    private void Awake()
    {
        // reset checkpoint to level start every time scene loads fresh
        if (!PlayerPrefs.HasKey("ContinueGame"))
            lastCheckPointPos = levelStartPos;

        // always reset checkpoint on fresh start
        lastCheckPointPos = levelStartPos;

        int characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player = Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        VCam.m_Follow = player.transform;
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        isGameOver = false;
    }

    void Update()
    {
        coinsText.text = numberOfCoins.ToString();
        if (isGameOver)
            gameOverScreen.SetActive(true);
    }

    public void ReplayLevel()
    {
        // reset checkpoint so replay starts from beginning
        lastCheckPointPos = levelStartPos;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        // reset checkpoint when going back to menu
        lastCheckPointPos = levelStartPos;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}