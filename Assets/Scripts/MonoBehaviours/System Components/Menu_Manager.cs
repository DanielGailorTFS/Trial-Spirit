using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private Game_Manager gameManager;
    [Header("Main Menu")]
    [SerializeField] private Button newGame;
    [SerializeField] private Button loadGame;
    [SerializeField] private Button settings;
    [SerializeField] private Button quitGame;

    private void Start()
    {
        newGame.onClick.AddListener(NewGame);
        loadGame.onClick.AddListener(LoadGame);
        settings.onClick.AddListener(Settings);
        quitGame.onClick.AddListener(QuitGame);
    }

    private void NewGame()
    {
        Debug.Log("Starting New Game");
        gameManager.StartGame();
    }

    private void LoadGame()
    {
        Debug.Log("Loading Game");
        //gameManager.LoadGame();
    }

    private void Settings()
    {
        Debug.Log("Opening Settings");
    }

    private void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void PauseGame()
    {
        Debug.Log("Pausing Game");
        gameManager.PauseGame();
    }

    public void QuitToMenu()
    { 
        Debug.Log("Quitting to Main Menu");
        gameManager.QuitToMenu();
    }
}
