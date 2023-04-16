using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Box : PassiveItem
{
    [Range(0,2)]
    public int Health = 2;

    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _breakEffectPrefab;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        SetHealth(Health);
    }

    [ContextMenu("OnAffect")]
    public override void OnAffect()
    {
        base.OnAffect();
        Health--;
        Instantiate(_breakEffectPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        _animator.SetTrigger("Shake");
        if (Health < 0)
        {
            Die();
        }
        else
        {
            SetHealth(Health);
        }
            
    }


    private void SetHealth(int value)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i <= value);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
