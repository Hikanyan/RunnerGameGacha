using UnityEngine;
using UnityEngine.InputSystem;

public partial class Player : MonoBehaviour
{
    private StateMachine<Player> _stateMachine;
    private InputAction _movementAction;

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
        
        // InputSystemの設定
        _movementAction = new InputAction("Move", InputActionType.Button, "<Keyboard>/space");
        _movementAction.performed += OnMovementPerformed;
        _movementAction.Enable();
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
    
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 移動アクションの処理
            Walk();
        }
    }

    private void OnDestroy()
    {
        // InputSystemのイベントハンドラの解除
        _movementAction.performed -= OnMovementPerformed;
        _movementAction.Disable();
    }
}