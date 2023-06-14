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
    [SerializeField] private GameObject[] _special;
    [SerializeField] private int _loadLength = 10;
    [SerializeField] private float _loadSpacing = 2f;
    [SerializeField] private int _itemLength = 30;
    [SerializeField] private float _itemMinSpacing = 5f;
    [SerializeField] private float _itemMaxSpacing = 20f;


    public void InGameRunStart()
    {
        List<GameObject> selectedObjects = new List<GameObject>();
        for (int i = 0; i < _loadLength; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _loadObject.Length);
            selectedObjects.Add(_loadObject[randomIndex]);
        }
        selectedObjects.Insert(0, _special[0]);
        selectedObjects.Add(_special[_special.Length - 1]);

        for (int i = 0; i < _loadLength; i++)
        {
            Vector3 loadRot = new Vector3(-90f, 0f, 90);

            if (i == 0 && _special.Length > 0 && selectedObjects[0] == _special[0])
            {
                // 最初の要素がスペシャルのオブジェクトの場合、別の位置を指定
                Vector3 specialLoadPos = new Vector3(0, 10f, i * _loadSpacing);
                UnityEngine.Object.Instantiate(selectedObjects[0], specialLoadPos, Quaternion.Euler(loadRot));
            }
            if (i == _loadLength - 1 && _special.Length > 1)
            {
                // 最後の要素がスペシャルのオブジェクトの場合、別の位置を指定
                Vector3 specialPos = new Vector3(0, 8f, i * _loadSpacing);
                Vector3 specialRot = new Vector3(0, 180f, 0f);
                UnityEngine.Object.Instantiate(selectedObjects[_loadLength - 1], specialPos, Quaternion.Euler(specialRot));
                Debug.Log(selectedObjects[_loadLength-1]);
            }
            else
            {
                // 通常の位置を指定
                Vector3 normalLoadPos = new Vector3(0f, 9f, i * _loadSpacing);
                UnityEngine.Object.Instantiate(selectedObjects[i], normalLoadPos, Quaternion.Euler(loadRot));
            }

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

    public void Result()
    {
        
    }
}