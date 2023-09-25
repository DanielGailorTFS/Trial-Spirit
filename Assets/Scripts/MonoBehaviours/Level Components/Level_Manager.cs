using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level_Manager : MonoBehaviour
{
    [SerializeField] private Game_Manager gameManager;
    public Level_Manager Instance;
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
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }

    public bool isPaused = false;
    public bool levelComplete = false;
    public bool gameOver = false;

    public void LevelComplete()
    { 
        gameManager.LevelComplete();
    }
}
