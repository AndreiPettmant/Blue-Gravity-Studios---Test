using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.CurrentGameStatus != GameStatus.Dialogue)
        { 
            pausePanel.SetActive(true);
            GameManager.SetGameStatus(GameStatus.Dialogue);
        }
            
    }

    public void ResumeGame()
    {
        GameManager.SetGameStatus(GameStatus.Playing);
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
