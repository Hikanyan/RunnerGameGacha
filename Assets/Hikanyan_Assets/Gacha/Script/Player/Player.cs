using System;
using UnityEngine;

namespace Hikanyan.Runner.Player
{
    public partial class Player :MonoBehaviour
    {
        private PlayerStateBase _currentState;
        void Start()
        {
            _currentState.OnEnter(this,null);
        }

        void Update()
        {
            _currentState.OnUpdate(this);
        }

        void ChangeState(PlayerStateBase nextState)
        {
            _currentState.OnExit(this,nextState);
            nextState.OnExit(this,_currentState);
            _currentState = nextState;
        }
        void OnCollisionEnter(Collision collision)
        {
            //ChangeState(stateStanding);
        }
    }
}