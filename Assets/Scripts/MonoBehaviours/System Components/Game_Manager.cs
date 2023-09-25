using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    public Menu_Manager canvasManager;

    private enum GameState
    {
        MainMenu,
        InGame,
        Paused,
        GameOver,
        LevelComplete
    }
    [SerializeField] private GameState currentGameState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // This ensures that the object is not destroyed when a new scene is loaded
        }
        else
        {
            Destroy(gameObject);  // Ensures that you don't have duplicate GameManager objects in your scenes
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_Tutorial");
        currentGameState = GameState.InGame;
    }

    public void PauseGame()
    {
        currentGameState = GameState.Paused;
    }

    public void LevelComplete()
    {
        SceneManager.LoadScene("Win");
        currentGameState = GameState.LevelComplete;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Lose");
        currentGameState = GameState.GameOver;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }




}
