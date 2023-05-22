using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    bool _isStop = false;
    Vector3 force = new Vector3(30.0f, 0.0f, 0.0f);
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //TODO
        //ˆÚ“®
        if (!_isStop)
        {
            if (_rigidBody.velocity.magnitude < 10.0f)
            {
                _rigidBody.AddForce(force); // —Í‚ð‰Á‚¦‚é
            }
        }
    }
}
