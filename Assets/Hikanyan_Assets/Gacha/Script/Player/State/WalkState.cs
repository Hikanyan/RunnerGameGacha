using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
            if (player.InputController.IsInputEnabled)
            {
                player._laneIndex += (int)player.InputController.Move.x;

                if (player._laneIndex < 0) { player._laneIndex = 0; }
                if (player._laneIndex > 2) { player._laneIndex = 2; }

                DisableInputForDelay().Forget();

                player.transform.DOMoveX(player._lanesPos[player._laneIndex].position.x, 0.2f).SetEase(Ease.Linear).SetAutoKill();

                Debug.Log($"index{player._laneIndex}");
            }
            if(player._rigidbody.velocity.magnitude<100 )
                player._rigidbody.AddForce(new Vector3(0,0,player._speed));
            Debug.Log("WalkState: Update");
        }
        private async UniTaskVoid DisableInputForDelay()
        {
            Player player = StateMachine.Owner;
            player.InputController.IsInputEnabled = false;
            await UniTask.Delay((int)(player.InputController.InputDisableTime * 1000));
            player.InputController.IsInputEnabled = true;
        }
    }
}