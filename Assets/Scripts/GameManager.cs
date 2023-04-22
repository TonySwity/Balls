using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winWindows;
    [SerializeField] private GameObject _loseWindows;

    public UnityEvent OnWin;
    
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Win()
    {
        _winWindows.SetActive(true);
        OnWin.Invoke();
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        ProgressGame.Instance.SetLevel(currentLevelIndex+1);
        ProgressGame.Instance.AddCoins(50);
    }

    public void Lose()
    {
        _loseWindows.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(ProgressGame.Instance.Level);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
