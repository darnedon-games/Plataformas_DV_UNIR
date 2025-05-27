﻿using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource music;

    private void Start()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadMenuScene()
    {
        if (Time.timeScale == 0f)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f; // Se reanuda el juego
            music.UnPause();// Se reanuda la música
        }
        SceneManager.LoadScene("MainMenu");
    }
    public void ResumeGame()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f; // Se reanuda el juego
        music.UnPause();// Se reanuda la música
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}