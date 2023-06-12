using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
public class UIManager : AbstractSingleton<UIManager>
{
    public GameObject uiPrefab;
    private Transform uiParent;
    private Dictionary<string, GameObject> uiDictionary;

    protected override void Awake()
    {
        base.Awake();
        uiParent = GameObject.Find("UIParent").transform; // UIを配置する親オブジェクトの名前を設定してください
        uiDictionary = new Dictionary<string, GameObject>();
    }

    public async UniTask OpenUI<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;
        if (!uiDictionary.ContainsKey(uiName))
        {
            GameObject uiObject = await InstantiateUI(uiName);
            uiDictionary.Add(uiName, uiObject);
        }
        else
        {
            GameObject uiObject = uiDictionary[uiName];
            uiObject.SetActive(true);
        }
    }

    public void CloseUI<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;
        if (uiDictionary.ContainsKey(uiName))
        {
            GameObject uiObject = uiDictionary[uiName];
            uiObject.SetActive(false);
        }
    }

    private async UniTask<GameObject> InstantiateUI(string uiName)
    {
        GameObject uiObject = (GameObject)await Resources.LoadAsync(uiName).ToUniTask();
        uiObject = Instantiate(uiObject, uiParent);
        uiObject.name = uiName;
        await uiObject.transform.DOScale(Vector3.zero, 0f).From().SetEase(Ease.OutBack).AsyncWaitForCompletion();
        return uiObject;
    }
}
