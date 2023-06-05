using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Player _player;
    PlayerInput _input;

    [SerializeField] private float _playerSpeed = 5;

    private void Awake()
    {
        TryGetComponent(out _player);
        TryGetComponent(out _input);
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
        _input.actions["Fire"].started += OnFire;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
        _input.actions["Fire"].started -= OnFire;
    }

    void OnMove(InputAction.CallbackContext obj)
    {
        _player.Walk();
        // var value = obj.ReadValue<Vector2>();
        // var direction = new Vector3(value.x, 0, value.y);
        // Move(direction,_playerSpeed);
    }

    void OnMoveStop(InputAction.CallbackContext obj)
    {
    }

    void OnFire(InputAction.CallbackContext obj)
    {
        Debug.Log("Fire");
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}