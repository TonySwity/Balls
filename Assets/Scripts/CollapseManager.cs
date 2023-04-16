using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{
    public static CollapseManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Collapse(ActiveItem itemA, ActiveItem itemB)
    {
        ActiveItem toItem;
        ActiveItem fromItem;

        if (Mathf.Abs(itemA.transform.position.y - itemB.transform.position.y) > 0.2f)
        {
            if (itemA.transform.position.y > itemB.transform.position.y)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }
        else
        {
            if (itemA.RigidbodyItem.velocity.magnitude > itemB.RigidbodyItem.velocity.magnitude)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }
        
        StartCoroutine(CollapseProcess(fromItem, toItem));
    }

    public IEnumerator CollapseProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        fromItem.Disable();
        
        if (fromItem.ItemType == ItemType.Ball || toItem.ItemType == ItemType.Ball)
        {
            Vector3 startPosition = fromItem.transform.position;

            for (float t = 0; t < 1f; t += Time.deltaTime / 0.1f)
            {
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                yield return null;
            }

            fromItem.transform.position = toItem.transform.position;
        }
        // fromItem.Die();
        // toItem.DoEffect();
        // ExplodeBall(toItem.transform.position, toItem.Radius + 0.15f);

        if (fromItem.ItemType == ItemType.Ball && toItem.ItemType == ItemType.Ball)
        {
            fromItem.Die();
            toItem.DoEffect();
            ExplodeBall(toItem.transform.position, toItem.Radius + 0.1f);
        }
        else
        {
            if (fromItem.ItemType == ItemType.Ball)
            {
                fromItem.Die();
            }
            else
            {
                fromItem.DoEffect();
            }

            if (toItem.ItemType == ItemType.Ball)
            {
                toItem.Die();   
            }
            else
            {
                toItem.DoEffect();
            }
        }
    }

    public void ExplodeBall(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            PassiveItem passiveItem = colliders[i].GetComponent<PassiveItem>();
            
            if (colliders[i].attachedRigidbody)
            {
                passiveItem= colliders[i].attachedRigidbody.GetComponent<PassiveItem>();
            }
            if (passiveItem)
            {
                passiveItem.OnAffect();
            }
        }
    }
}
