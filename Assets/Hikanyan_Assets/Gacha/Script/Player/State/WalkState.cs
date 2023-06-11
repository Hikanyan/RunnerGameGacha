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
            if (player._isInputEnabled)
            {
                player._laneIndex += (int)player._move.x;

                if (player._laneIndex < 0) { player._laneIndex = 0; }
                if (player._laneIndex > 2) { player._laneIndex = 2; }

                DisableInputForDelay().Forget();

                player.transform.DOMoveX(player._lanesPos[player._laneIndex].position.x, 0.2f).SetEase(Ease.Linear).SetAutoKill();

                Debug.Log($"index{player._laneIndex}");
            }
            Debug.Log("WalkState: Update");
        }
        private async UniTaskVoid DisableInputForDelay()
        {
            Player player = StateMachine.Owner;
            player._isInputEnabled = false;
            await UniTask.Delay((int)(player._inputDisableTime * 1000));
            player._isInputEnabled = true;
        }
    }
}