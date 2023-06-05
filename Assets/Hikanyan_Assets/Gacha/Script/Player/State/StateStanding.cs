using UnityEngine;

namespace Hikanyan.Runner.Player
{
    public partial class Player
    {
        /// <summary>
        /// 通常状態
        /// </summary>
        public class StateStanding:PlayerStateBase
        {
            public override void OnUpdate(Player owner)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    owner.ChangeState(_stateJumping);
                }
            }
        }
    }
}