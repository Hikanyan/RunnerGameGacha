using System;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.Serialization;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] int _maxLevel = 10;
    [SerializeField] float _levelUpDuration = 1.0f;
    [SerializeField] int _initialExperienceNeeded = 100;
    [SerializeField] int _experienceIncreasePerLevel = 50;
    [SerializeField] GameObject _levelUpEffectPrefab;
    [SerializeField] Player _player;
    [SerializeField] UnityEvent<int> _onLevelUp;

    [SerializeField] int _experience;
    [SerializeField] int _experienceNeeded;
    private async UniTask LevelUpRoutine()
    {
        int currentLevel = _player.Level;

        if (currentLevel >= _maxLevel)
        {
            Debug.Log("最大レベルに達しました！");
            return;
        }

        // レベルアップエフェクトのプレハブを生成
        GameObject levelUpEffect = Instantiate(_levelUpEffectPrefab, _player.transform.position, Quaternion.identity);
        await UniTask.Delay(TimeSpan.FromSeconds(_levelUpDuration));

        // プレイヤーレベルを上げる
        _player.Level++;
        _onLevelUp.Invoke(_player.Level);

        // レベルアップエフェクトを削除
        Destroy(levelUpEffect);

        // DoTweenを使用してレベルアップアニメーションを再生
        _player.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), _levelUpDuration, 1, 0.5f);
        
        // 次のレベルアップまでの必要経験値を増やす
        _experienceNeeded += _experienceIncreasePerLevel;
    }

    public async UniTask LevelUp()
    {
        await LevelUpRoutine();
    }
    public void GainExperience(int amount)
    {
        _experience += amount;

        if (_experience >= _experienceNeeded)
        {
            _experience -= _experienceNeeded;
            LevelUp().Forget();
        }
    }
    private void Start()
    {
        _experienceNeeded = _initialExperienceNeeded;
    }
    //Debug用
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         GainExperience(10);
    //         
    //         Debug.Log($"レベル{player.Level}");
    //         Debug.Log($"経験値{experience}");
    //     }
    // }
}