using GamePlay;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    //Public members
    static public LevelManager Instance { get; private set; }

    public GameObject GameOverPanel;
    public GameObject GameWinPanel;
    public UnityEvent OnLevelLoad;
    public GameObject PauseMenu;
    public InputAction confirm;
    public AudioSource Audio;
    public AudioClip[] steam;
    public float TimeLimit = 300.0f;
    public float TimeRemaining = 0.0f;

    private void OnEnable()
    {
        confirm.performed += (InputAction.CallbackContext ctx) => { PauseGame(); };
        confirm.Enable();
    }

    private void OnDisable()
    {
        confirm.performed -= (InputAction.CallbackContext ctx) => { PauseGame(); };
        confirm.Disable();
    }

    public void PauseTime(bool paused)
    {
        timerIsRunning = !paused;
    }

    public void GameOver()
    {
        if (!GameWinPanel.activeInHierarchy)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0.0f;
            GameOverPanel.SetActive(true);
            GameOverPanel.GetComponentInChildren<Button>().Select();
            Audio.clip = steam[0];
            Audio.Play();
            Audio.loop = false;
        }
    }

    public void GameWin()
    {
        if (!GameOverPanel.activeInHierarchy)
        {
            Debug.Log("You Win");
            Time.timeScale = 0.0f;
            GameWinPanel.SetActive(true);
            GameWinPanel.GetComponentInChildren<Button>().Select();
            _gameManager.SaveLevelCompleted();
            Audio.clip = steam[1];
            Audio.Play();
            Audio.loop = false;
        }
    }

    public void LoadNextLevel()
    {
        _gameManager.LoadNextLevel();
        PauseMenu.SetActive(false);
    }
    public void RestartLevel()
    {
        _gameManager.Restart();
    }

    public void ReturnToMainMenu()
    {
        _gameManager.ReturnToMainMenu();
        PauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        _gameManager.QuitGame();
    }

    public void PauseGame()
    {
        _gameManager.PauseGame();
        PauseMenu.SetActive(true);
        PauseMenu.GetComponentInChildren<Button>().Select();
    }

    public void ContinueGame()
    {
        _gameManager.ContinueGame();
        PauseMenu.SetActive(false);
    }

    //Private members
    private GameManager _gameManager;
    private bool timerIsRunning = false;

    private void Awake()
    {
        Instance = this;
        _gameManager = GameManager.Instance;

        TimeRemaining = TimeLimit;

        Train train = FindObjectOfType<Train>();
        if (train)
            train.OnGameOver += GameOver;

        OnLevelLoad.Invoke();
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (TimeRemaining > 0f)
            {
                TimeRemaining -= Time.deltaTime;
            }
            else
            {
                timerIsRunning = false;
                GameWin();
            }
        }
    }

}