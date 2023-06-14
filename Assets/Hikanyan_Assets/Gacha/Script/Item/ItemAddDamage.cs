public class ItemAddDamage:ItemBase
{
    protected override void Activate()
    {
        GameManager.Instance._stateMachine.Dispatch((int)GameState.Result);
    }
}