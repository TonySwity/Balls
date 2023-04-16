using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Stone : PassiveItem
{
    [SerializeField] private GameObject _dieEffect;
    [Range(0, 2f)]
    [SerializeField] private int _level = 2;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private Stone _stonePrefab;

    [ContextMenu("OnAffect")]
    public override void OnAffect()
    {
        base.OnAffect();

        if (_level > 2)
        {
            for (int i = 0; i < 2; i++)
            {
                CreateChildRock(_level - 1);
            }
        }

        Die();
    }

    public void CreateChildRock(int level)
    {
        Stone newRock = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
        newRock.SetLevel(_level);
    }

    public void SetLevel(int level)
    {
        _level = level;

        float scale = _level switch
        {
            2 => 1f,
            1 => 0.7f,
            0 => 0.45f,
            _ => 1f
        };

        _visualTransform.localScale = Vector3.one * scale; 
    }


    private void Die()
    {
        Instantiate(_dieEffect, transform.position, quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }
}
