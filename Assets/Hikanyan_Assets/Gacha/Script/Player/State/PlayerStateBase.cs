namespace Hikanyan.Runner.Player
{
    /// <summary>
    /// プレイヤーのステートの抽象クラス
    /// </summary>
    public abstract class PlayerStateBase
    {
        public virtual void OnEnter(Player owner, PlayerStateBase playerStateBase) { }

        public virtual void OnUpdate(Player owner) { }

        public virtual void OnExit(Player owner, PlayerStateBase playerStateBase) { }
    }
}