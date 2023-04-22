using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _levelsText;
    [SerializeField] private Image _backgroundImage;

    [SerializeField] private Button _startButton;
    private void Start()
    {
        _coinsText.text = ProgressGame.Instance.Coins.ToString();
        _levelsText.text ="Levels " + ProgressGame.Instance.Level.ToString();
        _backgroundImage.color = ProgressGame.Instance.BackgroundColor;
        _startButton.onClick.AddListener(StartLevel);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(ProgressGame.Instance.Level);
    }

}
