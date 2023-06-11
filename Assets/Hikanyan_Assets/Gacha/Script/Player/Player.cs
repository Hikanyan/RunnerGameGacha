using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;


public partial class Player : MonoBehaviour
{
    [SerializeField] private Transform[] _lanesPos = new Transform[3];
    private int _laneIndex = 1;
    private Rigidbody _rigidbody;


    [SerializeField] PlayerStatusState _playerStatusState;
    [SerializeField] PlayerStatusInputController _inputController = new();
    [SerializeField] PlayerStatusXP _playerStatusXp = new();
    public PlayerStatusState PlayerStatusState => _playerStatusState;
    public PlayerStatusInputController InputController => _inputController;
    public PlayerStatusXP PlayerStatusXp => _playerStatusXp;

    


    private void Start()
    {
        TryGetComponent(out _rigidbody);
        Walk();
    }

    public void Walk()
    {
        // 歩くイベントの発行
        PlayerStatusState.PlayerStateMachine.Dispatch((int)PlayerStatusState.Event.Walk);
    }

    public void Jump()
    {
        // ジャンプイベントの発行
        PlayerStatusState.PlayerStateMachine.Dispatch((int)PlayerStatusState.Event.Jump);
    }
}