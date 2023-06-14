using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;
enum Event : int
{
    Idle,
    Walk,
    Jump,
}

public partial class Player : MonoBehaviour
{
    public StateMachine<Player> _stateMachine;


    [SerializeField] private float _speed = 5;
    [SerializeField] private Transform[] _lanesPos = new Transform[3];
    private int _laneIndex = 1;
    public Rigidbody _rigidbody;


    [SerializeField] private PlayerStatusInputController _inputController = new();
    [SerializeField] PlayerStatusXP _playerStatusXp = new();
    public PlayerStatusInputController InputController => _inputController;
    public PlayerStatusXP PlayerStatusXp => _playerStatusXp;




    private void Start()
    {
        TryGetComponent(out _rigidbody);
        _inputController.Start();
        Initialize();
        Walk();
    }

    private void Initialize()
    {
        //ステートマシンの設定
        _stateMachine = new StateMachine<Player>(this);
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
        _inputController.Update();
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