using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Hikanyan.Runner.Player
{
    public partial class Player : IMovable
    {
        Rigidbody _rigidbody;
        
        PlayerInput _input;
        private void Awake()
        {
            //OnAwake();
            TryGetComponent(out _rigidbody);
            TryGetComponent(out _input);
        }

        private void Start()
        {
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }

        public void Move(Vector3 direction, float speed)
        {
            //_rigidbody.transform.Translate(direction * speed * Time.deltaTime);
            _rigidbody.AddForce(direction * speed * Time.deltaTime,ForceMode.Impulse);
        }

        public void Rotate(Vector3 axis, float angle)
        {
            _rigidbody.transform.Rotate(axis, angle);
        }
    }
}