using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using State = StateMachine<GameManager>.State;

public class GameManager : AbstractSingleton<GameManager>
{
    private StateMachine<GameManager> _stateMachine;
    private ScoreManager _scoreManager= new ScoreManager();
    private TimerManager _timerManager= new TimerManager();
    private UIManager _uiManager;
    private void Start()
    {
        _stateMachine = new StateMachine<GameManager>(this);
        _uiManager = UIManager.Instance;
        // ステートの追加
        var titleState = _stateMachine.Add<TitleState>();
        var gameStartState = _stateMachine.Add<GameStartState>();
        var gameClearState = _stateMachine.Add<GameClearState>();
        var gameOverState = _stateMachine.Add<GameOverState>();
        var resultState = _stateMachine.Add<ResultState>();
        var explanationState = _stateMachine.Add<ExplanationState>();
        var gachaState = _stateMachine.Add<GachaState>();

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

    public void AddScore(int points)
    {
        _scoreManager.AddScore(points);
    }

    public void ResetScore()
    {
        _scoreManager.ResetScore();
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

        protected override void OnExit(State nextState)
        {
            // タイトルステートから出た時の処理
            
        }
    }

    private class GameStartState : State
    {
        protected override void OnEnter(State prevState)
        {
            // ゲーム開始ステートに入った時の処理
            
        }

        protected override void OnUpdate()
        {
            // ゲーム開始ステートの更新処理
        }

        protected override void OnExit(State nextState)
        {
            // ゲーム開始ステートから出た時の処理
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