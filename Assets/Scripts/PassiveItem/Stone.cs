using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Stone : PassiveItem
{
    [SerializeField] private GameObject _dieEffect;
    [Range(0, 2f)]
    [SerializeField] private int _level;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private Stone _stonePrefab;

    [ContextMenu("OnAffect")]
    public override void OnAffect()
    {
        base.OnAffect();
        
        if (_level > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateChildRock(_level - 1);
            }
        }
        else
        {
            ScoreManager.Instance.AddScore(ItemType, transform.position);
        }

        Die();
    }

    public void CreateChildRock(int level)
    {
        Stone newRock = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
        newRock.SetLevel(level);
    }

    public void SetLevel(int level)
    {
        _level = level;
        float scale = 1f;
        
        if (level == 2)
        {
            scale = 1f;
        }
        else if(level == 1)
        {
            scale = 0.7f;
        }
        else if (level == 0)
        {
            scale = 0.45f;
        }

        _visualTransform.localScale = Vector3.one * scale; 
    }


    private void Die()
    {
        Instantiate(_dieEffect, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}
