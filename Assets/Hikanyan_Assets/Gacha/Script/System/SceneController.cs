using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneController
{
    private Scene _lastScene;
    private readonly Scene _neverUnloadScene;

    public SceneController(Scene neverUnloadScene)
    {
        _neverUnloadScene = neverUnloadScene;
        _lastScene = _neverUnloadScene;
    }

    // 指定したシーンを非同期でロードします。
    public async UniTask LoadScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
            throw new ArgumentException($"{nameof(scene)} は無効です!");

        await UnloadLastScene();

        await LoadSceneAdditive(scene);
    }

    // 新しいシーンを非同期でロードします。
    public async UniTask LoadNewScene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
            throw new ArgumentException($"{nameof(scene)} は無効です!");

        await UnloadLastScene();

        await LoadNewSceneAdditive(scene);
    }

    // シーンを非同期でアンロードします。
    private async UniTask UnloadScene(Scene scene)
    {
        if (!_lastScene.IsValid())
            return;

        var asyncUnload = SceneManager.UnloadSceneAsync(scene);
        await asyncUnload;

        await UniTask.Yield();
    }

    // シーンを非同期で追加ロードします。
    private async UniTask LoadSceneAdditive(string scenePath)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        await asyncLoad;

        _lastScene = SceneManager.GetSceneByPath(scenePath);
        SceneManager.SetActiveScene(_lastScene);
    }

    // 新しいシーンを追加ロードします。
    private async UniTask LoadNewSceneAdditive(string sceneName)
    {
        var asyncUnload = SceneManager.UnloadSceneAsync(_lastScene);
        await asyncUnload;

        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        await asyncLoad;

        _lastScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(_lastScene);
    }

    // 最後にロードされたシーンを非同期でアンロードします。
    public async UniTask UnloadLastScene()
    {
        if (_lastScene != _neverUnloadScene)
            await UnloadScene(_lastScene);
    }
}
