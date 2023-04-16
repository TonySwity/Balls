using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamit : ActiveItem
{
    [Header("Dynamit")]
    [SerializeField] private float _affectRadius = 1.5f;
    [SerializeField] private float _forceValue = 1000f;

    [SerializeField] private GameObject _affectArea;
    [SerializeField] private GameObject _effectPrefab;

    protected override void Start()
    {
        base.Start();
        _affectArea.SetActive(false);
    }

    [ContextMenu("Explode")]
    public void Explode()
    {
        StartCoroutine(AffectProcess());
    }

    private IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        ItemAnimator.Play("PrepareOfExplossean");
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody rigidbody = colliders[i].attachedRigidbody;

            if (rigidbody)
            {
                Vector3 fromTo = (rigidbody.transform.position - transform.position).normalized;
                rigidbody.AddForce(fromTo * _forceValue + Vector3.up * (_forceValue * 0.5f));
                
                PassiveItem passiveItem = rigidbody.GetComponent<PassiveItem>();
            
                if (passiveItem)
                {
                    passiveItem.OnAffect();
                }
            }
        }
        
        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        Explode();
    }
}
