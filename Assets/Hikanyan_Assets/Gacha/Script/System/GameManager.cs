using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using State = StateMachine<GameManager>.State;

public class GameManager : AbstractSingleton<GameManager>
{
    private StateMachine<GameManager> stateMachine;
    private ScoreManager scoreManager;
    private TimerManager timerManager;

    private void Start()
    {
        stateMachine = new StateMachine<GameManager>(this);
        scoreManager = new ScoreManager();
        timerManager = new TimerManager();

        // ステートの追加
        var titleState = stateMachine.Add<TitleState>();
        var gameStartState = stateMachine.Add<GameStartState>();
        var gameClearState = stateMachine.Add<GameClearState>();
        var gameOverState = stateMachine.Add<GameOverState>();
        var resultState = stateMachine.Add<ResultState>();
        var explanationState = stateMachine.Add<ExplanationState>();

        // 遷移の定義
        stateMachine.AddTransition<TitleState, GameStartState>((int)GameState.GameStart);
        stateMachine.AddTransition<GameStartState, GameClearState>((int)GameState.GameClear);
        stateMachine.AddTransition<GameStartState, GameOverState>((int)GameState.GameOver);
        stateMachine.AddTransition<GameClearState, ResultState>((int)GameState.Result);
        stateMachine.AddTransition<GameOverState, ResultState>((int)GameState.Result);
        stateMachine.AddTransition<ResultState, TitleState>((int)GameState.Title);
        stateMachine.AddTransition<TitleState, ExplanationState>((int)GameState.Explanation);
        stateMachine.AddTransition<ExplanationState, TitleState>((int)GameState.Title);

        // ステートマシンの実行を開始
        stateMachine.Start<TitleState>();
    }

    public void AddScore(int points)
    {
        scoreManager.AddScore(points);
    }

    public void ResetScore()
    {
        scoreManager.ResetScore();
    }

    public async UniTask StartTimer(float duration)
    {
        await timerManager.StartTimer(duration);
    }

    public void StopTimer()
    {
        timerManager.StopTimer();
    }

    public void ResetTimer()
    {
        timerManager.ResetTimer();
    }

    private class TitleState : StateMachine<GameManager>.State
    {
        protected override void OnEnter(State prevState)
        {
            // タイトルステートに入った時の処理
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

    private class GameStartState : StateMachine<GameManager>.State
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

    private class GameClearState : StateMachine<GameManager>.State
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

    private class GameOverState : StateMachine<GameManager>.State
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

    private class ResultState : StateMachine<GameManager>.State
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

    private class ExplanationState : StateMachine<GameManager>.State
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
}