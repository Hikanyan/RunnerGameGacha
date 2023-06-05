using UnityEngine;
using State = StateMachine<Player>.State;

public partial class Player
{
    public class JumpState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("JumpState: Enter");
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("JumpState: Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("JumpState: Update");
        }
    }
}