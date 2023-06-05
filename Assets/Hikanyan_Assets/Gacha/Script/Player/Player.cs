using UnityEngine;
using System.Collections.Generic;

public partial class Player : MonoBehaviour
{
    private StateMachine<Player> _stateMachine;

    enum Event : int
    {
        Idle,
        Walk,
        Jump,
    }
    private void Start()
    {
        _stateMachine = new StateMachine<Player>(this);
        Initialize();
    }

    private void Initialize()
    {
        // ステートの追加
        var idleState = _stateMachine.Add<IdleState>();
        var walkState = _stateMachine.Add<WalkState>();
        var jumpState = _stateMachine.Add<JumpState>();

        // 遷移の定義
        _stateMachine.AddTransition<IdleState, WalkState>((int)Event.Walk);
        _stateMachine.AddTransition<IdleState, JumpState>((int)Event.Jump);
        _stateMachine.AddTransition<WalkState, IdleState>((int)Event.Idle);
        _stateMachine.AddTransition<JumpState, IdleState>((int)Event.Idle);

        // 初期ステートの設定
        _stateMachine.Start(idleState);
    }

    private void Update()
    {
        // ステートの更新
        _stateMachine.Update();
    }

    public void Walk()
    {
        // 歩くイベントの発行
        _stateMachine.Dispatch((int)Event.Walk);
    }

    public void Jump()
    {
        // ジャンプイベントの発行
        _stateMachine.Dispatch((int)Event.Jump);
    }
}
