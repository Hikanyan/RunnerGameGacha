using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;


public partial class Player : MonoBehaviour
{
    private StateMachine<Player> _stateMachine;
    private PlayerInput _playerInput = default;
    private InputAction _moveAction, _lookAction, _fireAction,_jumpAction;
    //private List<InputEvent> _inputBuffer = new List<InputEvent>();

    [SerializeField] private Transform[] _lanesPos = new Transform[3];
    private int _laneIndex = 1;
    private Rigidbody _rigidbody;
    enum Event : int
    {
        Idle,
        Walk,
        Jump,
    }


    private void Start()
    {
        TryGetComponent(out _rigidbody);
        TryGetComponent(out _playerInput);
        
        Initialize();
    }

    private void Initialize()
    {
        // InputSystemの設定
        var actionMap = _playerInput.currentActionMap;
        //アクションマップからアクションを取得
        _moveAction = actionMap["Move"];
        _lookAction = actionMap["Look"];
        _fireAction = actionMap["Fire"];
        _jumpAction = actionMap["Jump"];

        _moveAction.started += OnMovementPerformed;
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
    //
    // private void OnEnable()
    // {
    //     // InputSystemのイベントハンドラの登録
    //     _playerInput.actions["Move"].performed += OnMovementPerformed;
    // }
    //
    // private void OnDisable()
    // {
    //     // InputSystemのイベントハンドラの解除
    //     _playerInput.actions["Move"].performed -= OnMovementPerformed;
    // }

    private void Update()
    {
        // ステートの更新
        _stateMachine.Update();
    
        //アクションからコントローラの入力値を取得
        Vector2 move = _moveAction.ReadValue<Vector2>();
        Vector2 look = _lookAction.ReadValue<Vector2>();
        bool fire = _fireAction.triggered;

        // バッファの内容を処理
        //ProcessInputBuffer();
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
    // private void ProcessInputBuffer()
    // {
    //     // バッファが空でない場合、各イベントを処理する
    //     while (_inputBuffer.Count > 0)
    //     {
    //         var inputEvent = _inputBuffer[0];
    //         _inputBuffer.RemoveAt(0);
    //
    //         // イベントに対応するアクションを実行
    //         _stateMachine.Dispatch((int)inputEvent.EventType);
    //     }
    // }
    
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        // 移動アクションの処理
        Walk();
    }
}