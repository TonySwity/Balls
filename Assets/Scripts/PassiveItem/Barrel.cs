using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : PassiveItem
{
    [SerializeField] private GameObject _barrelExplosion;
    public override void OnAffect()
    {
        base.OnAffect();
        Die();
    }

    [ContextMenu("Die")]
    private void Die()
    {
        Instantiate(_barrelExplosion, transform.position, Quaternion.Euler(-90,0,0));
        ScoreManager.Instance.AddScore(ItemType, transform.position);
        Destroy(gameObject);
    }
}
