using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;
using State = StateMachine<InGameManager>.State;

[Serializable]
public class InGameManager
{
    [SerializeField] private GameObject _lanePostion;
    [SerializeField] private GameObject[] _loadObject;
    [SerializeField] private GameObject[] _itemObject;
    [SerializeField] private int _loadLength = 10;
    [SerializeField] private float _loadSpacing = 2f;
    [SerializeField] private int _itemLength = 30;
    [SerializeField] private float _itemMinSpacing = 5f;
    [SerializeField] private float _itemMaxSpacing = 20f;
    [SerializeField] private int _addPoint;
    [SerializeField] private float _timer;


    public void Start()
    {
        List<GameObject> selectedObjects = new List<GameObject>();
        for (int i = 0; i < _loadLength; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _loadObject.Length);
            selectedObjects.Add(_loadObject[randomIndex]);
        }

        for (int i = 0; i < _loadLength; i++)
        {
            Vector3 loadPos = new Vector3(0f, 0f, i * _loadSpacing);
            Vector3 loadRot = new Vector3(-90f, 0f, 90);
            UnityEngine.Object.Instantiate(selectedObjects[i], loadPos, Quaternion.Euler(loadRot));
        }

        List<Transform> itemSpawnPoint = new List<Transform>();
        // スポーンポイントの取得
        foreach (Transform child in _lanePostion.transform)
        {
            itemSpawnPoint.Add(child);
        }

        List<GameObject> itemObjects = new List<GameObject>();
        for (int i = 0; i < _itemLength; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _itemObject.Length);
            itemObjects.Add(_itemObject[randomIndex]);
        }
        // スポーンポイントごとにアイテムを生成
        for (int i = 0; i < _itemLength; i++)
        {
            float randomSpacing =UnityEngine.Random.Range(_itemMinSpacing, _itemMaxSpacing);
            int randomIndex = UnityEngine.Random.Range(0, _itemObject.Length);
            Vector3 loadPos = new Vector3(itemSpawnPoint[randomIndex].position.x, 0f, i * randomSpacing);
            Vector3 loadRot = new Vector3(0f, 0f, 0f);
            UnityEngine.Object.Instantiate(itemObjects[i], loadPos, Quaternion.Euler(loadRot));
        }
    }
}