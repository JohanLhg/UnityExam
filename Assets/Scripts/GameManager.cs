using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct MedalThreshold {
    public Sprite medalSprite;
    public int threshold;
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public BirdBehaviour bird;
    public TMP_Text scoreText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverBestScoreText;
    public Image medalImage;
    public MedalThreshold[] medals;

    private bool gameRunning = false;
    private bool gamePaused = false;
    private bool gameOver = false;
    private int score = 0;
    private int bestScore = 0;

    private GameObject uiStart;
    private GameObject uiGameOver;
    private GameObject uiInGame;
    private GameObject uiPause;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start() {
        Time.timeScale = 0;
        scoreText.text = score.ToString();
        bestScore = PlayerPrefs.GetInt("bestScore");

        uiStart = GameObject.FindGameObjectWithTag("UiStart");
        uiGameOver = GameObject.FindGameObjectWithTag("UiGameOver");
        uiInGame = GameObject.FindGameObjectWithTag("UiInGame");
        uiPause = GameObject.FindGameObjectWithTag("UiPause");

        SetUi(Ui.Start);
    }

    void Update() {
        if (!gameRunning && Input.GetKeyDown(KeyCode.Space)) {
            if (!gameOver) StartGame();
            else RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) ResumeGame();
            else PauseGame();
        }
    }

    private void StartGame() {
        bird.transform.position = new Vector3(0, 0, 0);
        Time.timeScale = 1;
        gameRunning = true;
        score = 0;
        scoreText.text = score.ToString();
        SetUi(Ui.InGame);
    }

    private void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameRunning() {
        return gameRunning;
    }

    private void PauseGame() {
        Time.timeScale = 0;
        gamePaused = true;
        SetUi(Ui.Pause);
    }

    public bool IsGamePaused() {
        return gamePaused;
    }

    private void ResumeGame() {
        Time.timeScale = 1;
        gamePaused = false;
        SetUi(Ui.InGame);
    }
    
    public void GameOver() {
        gameOver = true;
        gameRunning = false;
        SetUi(Ui.GameOver);
    }

    public void AddScore() {
        score++;
        scoreText.text = score.ToString();
        if (score > bestScore) {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
    }

    private void SetUi(Ui uiType) {
        uiStart.SetActive(false);
        uiGameOver.SetActive(false);
        uiInGame.SetActive(false);
        uiPause.SetActive(false);

        switch (uiType) {
            case Ui.Start:
                uiStart.SetActive(true);
                break;
            case Ui.GameOver:
                uiGameOver.SetActive(true);
                gameOverScoreText.text = score.ToString();
                gameOverBestScoreText.text = bestScore.ToString();
                foreach (MedalThreshold medal in medals) {
                    if (score >= medal.threshold) {
                        medalImage.sprite = medal.medalSprite;
                        medalImage.color = Color.white;
                        break;
                    } else {
                        medalImage.color = new Color(0, 0, 0, 0);
                    }
                }
                break;
            case Ui.InGame:
                uiInGame.SetActive(true);
                break;
            case Ui.Pause:
                uiPause.SetActive(true);
                break;
        }
    }

    private enum Ui {
        Start,
        Pause,
        GameOver,
        InGame
    }
}
