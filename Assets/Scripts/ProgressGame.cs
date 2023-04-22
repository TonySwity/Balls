using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProgressGame : MonoBehaviour
{
    public int Coins = 0;
    public int Level = 1;
    public Color BackgroundColor;

    public static ProgressGame Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Load();
    }

    public void SetLevel(int level)
    {
        Level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        Coins += value;
        Save();
    }

    [ContextMenu("SaveData")]
    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("LoadData")]
    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();

        if (progressData != null)
        {
            Coins = progressData.Coins;
            Level = progressData.Level;
            Color newColor = new Color();
            newColor.r = progressData.BackgroundColor[0];
            newColor.g = progressData.BackgroundColor[1];
            newColor.b = progressData.BackgroundColor[2];
            newColor.a = progressData.BackgroundColor[3];
        }
        else
        {
            Coins = 0;
            Level = 1;
            
            //Почему закомментированный код не работает?????
            //Хотя зайдя внуть Сolor цвет применяется верно
            //BackgroundColor = new Color(96f, 150f, 50f, 255f);
            
            //А этот работает????
            BackgroundColor = Color.green * 0.5f;
        }

    }

    [ContextMenu("DeleteData")]
    public void DeleteFile()
    {
        SaveSystem.Delete();
    }
}
