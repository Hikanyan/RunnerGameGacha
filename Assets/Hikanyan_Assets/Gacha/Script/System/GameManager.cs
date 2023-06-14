using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using State = StateMachine<GameManager>.State;
[Serializable]
public class GameManager : AbstractSingleton<GameManager>
{
    public StateMachine<GameManager> _stateMachine;
    [SerializeField] ScoreManager _scoreManager = new ScoreManager();
    [SerializeField] TimerManager _timerManager = new TimerManager();
    [SerializeField] InGameManager _inGameManager = new InGameManager();
    [SerializeField] LevelUpManager _levelUpManager = new LevelUpManager();
    UIManager _uiManager;
    
    public ScoreManager ScoreManager => _scoreManager;
    public LevelUpManager LevelUpManager => _levelUpManager;
    private void Start()
    {
        _stateMachine = new StateMachine<GameManager>(this);
        _uiManager = UIManager.Instance;
        // 遷移の定義
        _stateMachine.AddTransition<TitleState, GameStartState>((int)GameState.GameStart);
        _stateMachine.AddTransition<GameStartState, GachaState>((int)GameState.Gacha);
        _stateMachine.AddTransition<GachaState, GameClearState>((int)GameState.GameClear);
        _stateMachine.AddTransition<GachaState, GameOverState>((int)GameState.GameOver);
        _stateMachine.AddTransition<GameClearState, ResultState>((int)GameState.Result);
        _stateMachine.AddTransition<GameOverState, ResultState>((int)GameState.Result);
        _stateMachine.AddTransition<ResultState, TitleState>((int)GameState.Title);
        _stateMachine.AddTransition<TitleState, ExplanationState>((int)GameState.Explanation);
        _stateMachine.AddTransition<ExplanationState, TitleState>((int)GameState.Title);
        
        // ステートマシンの実行を開始
        _stateMachine.Start<TitleState>();
    }

    private void Update()
    {
        Debug.Log(_stateMachine.CurrentState);
    }

    public void AddExperiencePoint(int amount)
    {
        _levelUpManager.GainExperience(amount);
    }

    public void AddScore(int points)
    {
        _scoreManager.AddScore(points);
    }

    public void ResetScore()
    {
        _scoreManager.ResetScore();
    }

    public void AddCoin(int amount)
    {
        _scoreManager.AddCoin(amount);
    }

    public void ResetCoin()
    {
        _scoreManager.ResetCoin();
    }

    public async UniTask StartTimer(float duration)
    {
        await _timerManager.StartTimer(duration);
    }

    public void StopTimer()
    {
        _timerManager.StopTimer();
    }

    public void ResetTimer()
    {
        _timerManager.ResetTimer();
    }

    private class TitleState : State
    {
        protected override async void OnEnter(State prevState)
        {
            // タイトルステートに入った時の処理
            await SequenceManager.Instance.LoadScene("TitleScene");
            await GameManager.Instance._uiManager.OpenUI<TitleUI>();
        }

        protected override void OnUpdate()
        {
            // タイトルステートの更新処理
        }

        protected override async void OnExit(State nextState)
        {
            // タイトルステートから出た時の処理
            GameManager.Instance._uiManager.CloseUI<TitleUI>();
        }
    }

    private class GameStartState : State
    {
        protected override async void OnEnter(State prevState)
        {
            // ゲーム開始ステートに入った時の処理
            await SequenceManager.Instance.LoadScene("GameScene");
            await GameManager.Instance._uiManager.OpenUI<GameUI>();
            GameManager.Instance._inGameManager.InGameRunStart();
            GameManager.Instance._levelUpManager.Start();
        }

        protected override void OnUpdate()
        {
            // ゲーム開始ステートの更新処理
        }

        protected override void OnExit(State nextState)
        {
            // ゲーム開始ステートから出た時の処理
            GameManager.Instance._uiManager.CloseUI<GameUI>();
        }
    }

    private class GameClearState : State
    {
        protected override void OnEnter(State prevState)
        {
            // ゲームクリアステートに入った時の処理
        }

        protected override void OnUpdate()
        {
            // ゲームクリアステートの更新処理
        }

        protected override void OnExit(State nextState)
        {
            // ゲームクリアステートから出た時の処理
        }
    }

    private class GameOverState : State
    {
        protected override void OnEnter(State prevState)
        {
            // ゲームオーバーステートに入った時の処理
        }

        protected override void OnUpdate()
        {
            // ゲームオーバーステートの更新処理
        }

        protected override void OnExit(State nextState)
        {
            // ゲームオーバーステートから出た時の処理
        }
    }

    private class ResultState : State
    {
        protected override void OnEnter(State prevState)
        {
            // リザルトステートに入った時の処理
        }

        protected override void OnUpdate()
        {
            // リザルトステートの更新処理
        }

        protected override void OnExit(State nextState)
        {
            // リザルトステートから出た時の処理
        }
    }

    private class ExplanationState : State
    {
        protected override void OnEnter(State prevState)
        {
            // 説明ステートに入った時の処理
        }

        protected override void OnUpdate()
        {
            // 説明ステートの更新処理
        }

        protected override void OnExit(State nextState)
        {
            // 説明ステートから出た時の処理
        }
    }
    private class GachaState :State
    {
        protected override void OnEnter(State prevState)
        {
            // ガチャステートに入った時の処理
        }

        protected override void OnUpdate()
        {
            // ガチャステートの更新処理
        }

        protected override void OnExit(State nextState)
        {
            // ガチャステートから出た時の処理
        }
    }

}