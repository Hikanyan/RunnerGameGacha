using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using State = StateMachine<InGameManager>.State;

[Serializable]
public class InGameManager
{
    [SerializeField] private GameObject[] _loadObject;
    [SerializeField] private GameObject[] _pointObject;
    [SerializeField] private int _loadLength = 10; // オブジェクトの個数
    [SerializeField] float _loadSpacing = 2f; // オブジェクト間の間隔
    [SerializeField] private int _addPoint;
    [SerializeField] private float _timer;

    private Vector3 _position;
    private List<GameObject> _generatObjects = new List<GameObject>();
    private List<GameObject> _spawnedObject = new List<GameObject>();
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
            _spawnedObject.Add(UnityEngine.Object.Instantiate(selectedObjects[i], loadPos, Quaternion.Euler(loadRot)));
        }
    }
}