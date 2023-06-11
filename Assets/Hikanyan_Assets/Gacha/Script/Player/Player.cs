using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;


public partial class Player : MonoBehaviour
{
    private StateMachine<Player> _stateMachine;
    private RunGameControllerinputactions _playerInput;
    //private List<InputEvent> _inputBuffer = new List<InputEvent>();

    [SerializeField] private Transform[] _lanesPos = new Transform[3];
    private int _laneIndex = 1;
    private Rigidbody _rigidbody;
    private Vector2 _move, _look;
    private Vector2 _moveStop, _lookStop;
    
    private bool _fire;
    private bool _isInputEnabled = true;
    [SerializeField] float _inputDisableTime = 0.5f;

    private int _level;
    private int _experience;
    public int Level { get { return _level; } set { _level = value; } }
    public int Experience { get { return _experience; } set { _experience = value; } }
    enum Event : int
    {
        Idle,
        Walk,
        Jump,
    }


    private void Start()
    {
        _playerInput = new RunGameControllerinputactions();
        _playerInput.Enable();
        TryGetComponent(out _rigidbody);
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
        _move = _playerInput.Player.Move.ReadValue<Vector2>();
        _look = _playerInput.Player.Look.ReadValue<Vector2>();
        _fire = _playerInput.Player.Fire.triggered;

        Debug.Log(_move);
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
}