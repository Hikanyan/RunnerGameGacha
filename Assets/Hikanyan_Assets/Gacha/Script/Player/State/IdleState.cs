﻿using UnityEngine;
using State = StateMachine<Player>.State;

public partial class Player
{
    public class IdleState : State
    {
        protected override void OnEnter(State prevState)
        {
            Player player =StateMachine.Owner;
            player._rigidbody.velocity = Vector3.zero;
            Debug.Log("IdleState: Enter");
        }

        protected override void OnExit(State nextState)
        {
            Debug.Log("IdleState: Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("IdleState: Update");
        }
    }
}