 using System;
using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;
using Random = UnityEngine.Random;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private ActiveItem _ballPrefab;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TextMeshProUGUI _numberOfBallsText;
    
    private ActiveItem _itemInTube;
    private ActiveItem _itemInSpawner;
    private int _ballsLeft;
    private Coroutine _waitForLose;

    public void UpdateBallsLeftText()
    {
        _numberOfBallsText.text = _ballsLeft.ToString();
    }

    public void StopWaitForLose()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
        }
    }

    private void Start()
    {
        _ballsLeft = Level.Instance.NumberOfBalls;
        UpdateBallsLeftText();
        
        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    private void LateUpdate()
    {
        if (_itemInSpawner)
        {
            Ray ray = new Ray(_rayTransform.position,Vector3.down);
            
            if (Physics.SphereCast(ray, _itemInSpawner.Radius, out RaycastHit hit, 100f, _layerMask, QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2f, hit.distance, 1f);
                _itemInSpawner.Projection.SetPosition(_spawner.position + Vector3.down * hit.distance);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }

    private void CreateItemInTube()
    {
        if (_ballsLeft == 0)
        {
            Debug.Log("Balls Ended");
            return;
        }
        int itemLevel = Random.Range(0, Level.Instance.MaxCreatedBallLevel);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _itemInTube.SetLevel(itemLevel);
        _itemInTube.SetupToTube();
        _ballsLeft--;
        UpdateBallsLeftText();
    }

    private IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;
        
        for (float t = 0; t < 1f; t+= Time.deltaTime/0.8f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }

        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;
        _rayTransform.gameObject.SetActive(true);
        _itemInSpawner.Projection.Show();
        _itemInTube = null;
        CreateItemInTube();
    }

    private void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner.Projection.Hide();
        _itemInSpawner = null;
        _rayTransform.gameObject.SetActive(false);
        
        if (_itemInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
        else
        {
            _waitForLose = StartCoroutine(WaitForLose());
            CollapseManager.Instance.OnCollapse.AddListener(ResetLoseTimer);
            GameManager.Instance.OnWin.AddListener(StopWaitForLose);
        }
    }

    private IEnumerator WaitForLose()
    {
        for (float i = 0; i < 5f; i += Time.deltaTime)
        {
            yield return null;
        }
        
        Debug.Log("Lose");
        GameManager.Instance.Lose();
    }

    private void ResetLoseTimer()
    {
        if (_waitForLose != null)
        {
            StopCoroutine(_waitForLose);
            _waitForLose = StartCoroutine(WaitForLose());
        }

    }
}
