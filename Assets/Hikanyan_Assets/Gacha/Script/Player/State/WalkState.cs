using UnityEngine;
using State = StateMachine<Player>.State;

public partial class Player
{
    public class WalkState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("WalkState: Enter");
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("WalkState: Exit");
        }

        protected override void OnUpdate()
        {
            Player player = StateMachine.Owner;
            player._rigidbody.AddForce(new Vector3(10, 0, 0));
            Debug.Log("WalkState: Update");
        }
    }
}