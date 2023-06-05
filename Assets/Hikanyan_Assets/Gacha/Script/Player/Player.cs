using System;
using UnityEngine;

namespace Hikanyan.Runner.Player
{
    public partial class Player :MonoBehaviour
    {
        private static readonly StateStanding _stateStanding = new StateStanding();
        private static readonly StateJumping _stateJumping = new StateJumping();
        /// <summary>
        /// 現在のステート
        /// </summary>
        private PlayerStateBase _currentState =_stateStanding;

        
        
        void OnStart()
        {
            _currentState.OnEnter(this,null);
        }

        void OnUpdate()
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
            ChangeState(_stateStanding);
        }
    }
}