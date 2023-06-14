using System;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player._stateMachine.Dispatch((int)Event.Idle);
            GameManager.Instance._stateMachine.Dispatch((int)GameState.Result);
        }
    }
}