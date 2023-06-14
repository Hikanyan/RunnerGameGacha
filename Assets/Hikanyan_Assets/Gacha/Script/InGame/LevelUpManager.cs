using System;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using UniRx;

[Serializable]
public class LevelUpManager
{
    [SerializeField] int _maxLevel = 10;
    [SerializeField] float _levelUpDuration = 1.0f;
    [SerializeField] int _initialExperienceNeeded = 100;
    [SerializeField] int _experienceIncreasePerLevel = 50;
    [SerializeField] GameObject _levelUpEffectPrefab;
    [SerializeField] Player _player;
    [SerializeField] UnityEvent<int> _onLevelUp;

    private IntReactiveProperty _experience = new IntReactiveProperty(0);
    private IntReactiveProperty _currentLevel = new IntReactiveProperty(0);
    private int _experienceNeeded;

    public IReadOnlyReactiveProperty<int> Experience => _experience;
    public IReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;

    private async UniTask  LevelUpRoutine()
    {
        int currentLevel = _currentLevel.Value;

        if (currentLevel >= _maxLevel)
        {
            Debug.Log($"最大レベルに達しました！{_currentLevel.Value}");
            return;
        }

        // レベルアップエフェクトのプレハブを生成
        GameObject levelUpEffect = Object.Instantiate(_levelUpEffectPrefab, _player.transform.position, Quaternion.identity);
        await UniTask.Delay(TimeSpan.FromSeconds(_levelUpDuration));

        // プレイヤーレベルを上げる
        _currentLevel.Value++;
        _onLevelUp.Invoke(_currentLevel.Value);

        // レベルアップエフェクトを削除
        Object.Destroy(levelUpEffect);

        // DoTweenを使用してレベルアップアニメーションを再生
        _player.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), _levelUpDuration, 1, 0.5f);

        // 次のレベルアップまでの必要経験値を増やす
        _experienceNeeded += _experienceIncreasePerLevel;

        // 経験値を0にリセット
        _experience.Value = 0;
        _player.PlayerStatusXp.Experience = 0;
    }

     public async UniTask LevelUp()
    {
        await LevelUpRoutine();
    }

    public void GainExperience(int amount)
    {
        _player.PlayerStatusXp.Experience += amount;
        _experience.Value = _player.PlayerStatusXp.Experience;

        if (_experience.Value >= _experienceNeeded)
        {
            _experience.Value -= _experienceNeeded;
            LevelUp().Forget();
        }
    }

    public void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _experienceNeeded = _initialExperienceNeeded;
        _experience.Value = _player.PlayerStatusXp.Experience;
        _currentLevel.Value = _player.PlayerStatusXp.Level;
    }
}
