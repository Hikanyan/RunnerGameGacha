using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public GachaUI gachaUI;

    public async UniTask PullGachaAsync()
    {
        // ガチャの結果を非同期に待ち受け
        // GachaItem item = await UniTask.Delay(TimeSpan.FromSeconds(2)).ContinueWith(() => PullGacha());

        // 結果を表示する
        // gachaUI.ShowResult(item);
    }

    // private GachaItem PullGacha()
    // {
    //     // ガチャの結果を計算してアイテムを返す処理を実装する
    //     return item;
    // }
}