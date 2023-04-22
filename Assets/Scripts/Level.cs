using System;
using UnityEngine;

[System.Serializable]
public struct Task
{
    public ItemType ItemType;
    public int Number;
    public int Level;
}

public class Level : MonoBehaviour
{
    public int NumberOfBalls = 50;
    public int MaxCreatedBallLevel = 1;
    public Task[] Tasks;

    public static Level Instance;

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
}
