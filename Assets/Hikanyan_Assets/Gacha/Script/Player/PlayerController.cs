using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Hikanyan.Runner.Player
{
    public class PlayerController:MonoBehaviour
    {
        Player _player;
        PlayerInput _input;
        IMovable _move;

        [SerializeField]
        private float _playerSpeed = 5;
        private void Awake()
        {
            TryGetComponent(out _player);
            TryGetComponent(out _input);
            TryGetComponent(out _move);
        }

        private void Start()
        {
        }

        private void Update()
        {
        }

        private void OnEnable()
        {
            // _input.actions["Move"].performed += OnMove;
            // _input.actions["Move"].canceled += OnMoveStop;
            // _input.actions["Fire"].started += OnFire;
        }

        private void OnDisable()
        {
            // _input.actions["Move"].performed -= OnMove;
            // _input.actions["Move"].canceled -= OnMoveStop;
            // _input.actions["Fire"].started -= OnFire;
        }

        void OnMove(InputAction.CallbackContext obj)
        {
            var value = obj.ReadValue<Vector2>();
            var direction = new Vector3(value.x, 0, value.y);
            _move.Move(direction,_playerSpeed);
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
}