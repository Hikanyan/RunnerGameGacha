using System;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Idle();
            GameManager.Instance._stateMachine.Dispatch((int)GameState.Result);
        }
    }
}