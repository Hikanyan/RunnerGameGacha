using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceManager : AbstractSingleton<SequenceManager>
{
    [SerializeField]
    private GameObject[] _preloadedAssets;

    private SceneController _sceneController;

    public void Initialize()
    {
        _sceneController = new SceneController(SceneManager.GetActiveScene());

        InstantiatePreloadedAssets();
    }

    private void InstantiatePreloadedAssets()
    {
        foreach (var asset in _preloadedAssets)
        {
            Instantiate(asset);
        }
    }

    public async UniTask LoadScene(string scene)
    {
        await _sceneController.LoadScene(scene);
    }

    public async UniTask LoadNewScene(string scene)
    {
        await _sceneController.LoadNewScene(scene);
    }

    public async UniTask UnloadLastScene()
    {
        await _sceneController.UnloadLastScene();
    }
}
