using System.Collections.Generic;
using TMPro;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private List<GachaItem> gachaItems;
    [SerializeField] private TextMeshProUGUI resultText;

    private ReactiveProperty<bool> isSpinning = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> IsSpinning => isSpinning;

    public async UniTask<List<GachaItem>> SpinTenTimes()
    {
        if (isSpinning.Value)
            return null;

        isSpinning.Value = true;

        List<GachaItem> results = new List<GachaItem>();

        for (int i = 0; i < 10; i++)
        {
            GachaItem result = await GetGachaResult();
            results.Add(result);
        }

        isSpinning.Value = false;

        // 結果を表示する
        DisplayResults(results);

        return results;
    }

    public async UniTask<GachaItem> SpinOnce()
    {
        if (isSpinning.Value)
            return null;

        isSpinning.Value = true;

        GachaItem result = await GetGachaResult();

        isSpinning.Value = false;

        // 結果を表示する
        DisplayResult(result);

        return result;
    }

    private async UniTask<GachaItem> GetGachaResult()
    {
        int index = Random.Range(0, gachaItems.Count);
        return gachaItems[index];
    }

    private void DisplayResults(List<GachaItem> results)
    {
        string resultText = "ガチャ結果:\n";

        foreach (GachaItem result in results)
        {
            resultText += result.itemName + "\n";
        }

        this.resultText.text = resultText;
    }

    private void DisplayResult(GachaItem result)
    {
        resultText.text = "ガチャ結果: " + result.itemName;
    }
}

public class GachaItem
{
    public string itemName;
    public int itemRarity;
}