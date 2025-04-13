using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FB_GameManager : MonoBehaviour
{
    public static FB_GameManager Instance { get; private set; }
    public bool isGameOver = false;
    public bool isGameStarted = false;
    public float gameSpeed;
    private int score;
    public int GetScore
    {
        get { return score; }
    }
    public delegate void ScoreUpdated();
    public event ScoreUpdated OnScoreUpdated;

    public delegate void PlayGameOverSound();
    public event PlayGameOverSound OnGameOver;

    public Image gameOverMessage;

    private int highScore;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadData();

        isGameOver = false;
        isGameStarted = false;

        score = 0;
        OnScoreUpdated?.Invoke();
        gameOverMessage.gameObject.SetActive(false);

        UpdateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreText();
        }

        OnScoreUpdated?.Invoke();
    }

    public void GameOver()
    {
        isGameOver = true;
        gameSpeed = 0f;

        OnGameOver?.Invoke();
        StartCoroutine(DisplayGameOverMessage());
    }

    IEnumerator DisplayGameOverMessage()
    {
        yield return new WaitForSeconds(1f);
        gameOverMessage.gameObject.SetActive(true);
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }
    void OnDestroy()
    {
        SaveData();
    }
}
