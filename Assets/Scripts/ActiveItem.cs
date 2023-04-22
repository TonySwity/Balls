using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ActiveItem : Item
{
     public Projection Projection;
     public int Level;
     public float Radius;
     public Rigidbody RigidbodyItem;
     public bool IsDead;
     
     [SerializeField] protected TextMeshProUGUI LevelText;
     [SerializeField] protected SphereCollider Collider;
     [SerializeField] protected SphereCollider Trigger;
     [SerializeField] protected Animator ItemAnimator;

     protected virtual void Start()
     {
          Projection.Hide();
     }

     [ContextMenu("IncreaseLevel")]
     public virtual void IncreaseLevel()
     {
          Level++;
          SetLevel(Level);
          Trigger.enabled = false;
          StartCoroutine(EnableTrigger());
     }
     
     public virtual void SetLevel(int level)
     {
          Level = level;
          int number = (int)Mathf.Pow(2, level + 1);
          string numberString = number.ToString();
          LevelText.text = numberString;
          
     }

     public void SetupToTube()
     {
          Trigger.enabled = false;
          Collider.enabled = false;
          RigidbodyItem.isKinematic = true;
          RigidbodyItem.interpolation = RigidbodyInterpolation.None;
     }

     public void Drop()
     {
          Trigger.enabled = true;
          Collider.enabled = true;
          RigidbodyItem.isKinematic = false;
          RigidbodyItem.interpolation = RigidbodyInterpolation.Interpolate;
          transform.parent = null;
          RigidbodyItem.velocity = Vector3.down * 0.8f;
     }

     private void OnTriggerEnter(Collider other)
     {
          if(IsDead) return;
          
          if (other.attachedRigidbody)
          {
               ActiveItem otherItem = other.attachedRigidbody.GetComponent<ActiveItem>();
               
               if (otherItem && !otherItem.IsDead && Level == otherItem.Level)
               {
                    CollapseManager.Instance.Collapse(this,otherItem);
               }
          }
     }

     public void Disable()
     {
          Trigger.enabled = false;
          RigidbodyItem.isKinematic = true;
          Collider.enabled = false;
          IsDead = true;
     }

     public void Die()
     {
          Destroy(gameObject);
     }
     
     private IEnumerator EnableTrigger()
     {
          yield return new WaitForSecondsRealtime(0.08f);
          Trigger.enabled = true;
     }

     public virtual void DoEffect()
     {
          
     }
}
