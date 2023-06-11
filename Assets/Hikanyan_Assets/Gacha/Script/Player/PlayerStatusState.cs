using System;

[Serializable]
public class PlayerStatusState
{
    private StateMachine<Player> _stateMachine;

    public StateMachine<Player> PlayerStateMachine { get => _stateMachine; set => _stateMachine = value; }

    public enum Event : int
    {
        Idle,
        Walk,
        Jump,
    }

    void Start()
    {
        Player player = new Player();
        //ステートマシンの設定
        _stateMachine = new StateMachine<Player>(player);
        // ステートの追加
        var idleState = _stateMachine.Add<Player.IdleState>();
        var walkState = _stateMachine.Add<Player.WalkState>();
        var jumpState = _stateMachine.Add<Player.JumpState>();

        // 遷移の定義
        _stateMachine.AddTransition<Player.IdleState, Player.WalkState>((int)Event.Walk);
        _stateMachine.AddTransition<Player.IdleState, Player.JumpState>((int)Event.Jump);
        _stateMachine.AddTransition<Player.WalkState, Player.IdleState>((int)Event.Idle);
        _stateMachine.AddTransition<Player.JumpState, Player.IdleState>((int)Event.Idle);

        // 初期ステートの設定
        _stateMachine.Start(idleState);
    }

    void Update()
    {
        // ステートの更新
        _stateMachine.Update();
    }
}