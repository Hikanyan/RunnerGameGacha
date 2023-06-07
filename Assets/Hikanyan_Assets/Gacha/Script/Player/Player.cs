using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


public partial class Player : MonoBehaviour
{
    private StateMachine<Player> _stateMachine;
    private PlayerInput _movementPlayerInputAction;
    
    private List<InputEvent> _inputBuffer = new List<InputEvent>();


    private Rigidbody _rigidbody;
    enum Event : int
    {
        Idle,
        Walk,
        Jump,
    }
    struct InputEvent
    {
        public Event EventType;

        public InputEvent(Event eventType)
        {
            EventType = eventType;
        }
    }


    private void Start()
    {
        TryGetComponent(out _rigidbody);
        // InputSystemの設定
        TryGetComponent(out _movementPlayerInputAction);
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

    private void OnEnable()
    {
        // InputSystemのイベントハンドラの登録
        _movementPlayerInputAction.actions["Move"].performed += OnMovementPerformed;
    }

    private void OnDisable()
    {
        // InputSystemのイベントハンドラの解除
        _movementPlayerInputAction.actions["Move"].performed -= OnMovementPerformed;
    }

    private void Update()
    {
        // ステートの更新
        _stateMachine.Update();
    
        // バッファの内容を処理
        ProcessInputBuffer();
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
    private void ProcessInputBuffer()
    {
        // バッファが空でない場合、各イベントを処理する
        while (_inputBuffer.Count > 0)
        {
            var inputEvent = _inputBuffer[0];
            _inputBuffer.RemoveAt(0);

            // イベントに対応するアクションを実行
            _stateMachine.Dispatch((int)inputEvent.EventType);
        }
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 入力イベントをバッファに追加
            var inputEvent = new InputEvent(Event.Walk);
            _inputBuffer.Add(inputEvent);
            // デバッグログでバッファに追加されたことを確認
            Debug.Log("Input Event Added to Buffer: " + inputEvent.EventType);
            // 移動アクションの処理
            Walk();
        }
    }
}