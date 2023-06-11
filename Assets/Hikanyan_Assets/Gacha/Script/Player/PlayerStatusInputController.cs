using System;
using UnityEngine;
[Serializable]
public class PlayerStatusInputController
{
    RunGameControllerinputactions _playerInput;
    //List<InputEvent> _inputBuffer = new List<InputEvent>();
    
    [SerializeField] bool _fire;
    [SerializeField] bool _isInputEnabled = true;
    [SerializeField] float _inputDisableTime = 0.05f;
    
    
    Vector2 _move, _look;

    public bool Fire{get => _fire;set => _fire = value; }
    public bool IsInputEnabled{get => _isInputEnabled;set => _isInputEnabled = value; }
    public float InputDisableTime{get =>_inputDisableTime; set => _inputDisableTime =value;}
    
    public Vector2 Move{get => _move;set => _move = value; }
    public Vector2 Look{get => _look;set => _look = value; }
    public void Start()
    {
        _playerInput = new RunGameControllerinputactions();
        _playerInput.Enable();
    }

    public void Update()
    {
        //アクションからコントローラの入力値を取得
        _move = _playerInput.Player.Move.ReadValue<Vector2>();
        _look = _playerInput.Player.Look.ReadValue<Vector2>();
        _fire = _playerInput.Player.Fire.triggered;

        Debug.Log(_move);
        // バッファの内容を処理
        //ProcessInputBuffer();
    }
    
    // private void ProcessInputBuffer()
    // {
    //     // バッファが空でない場合、各イベントを処理する
    //     while (_inputBuffer.Count > 0)
    //     {
    //         var inputEvent = _inputBuffer[0];
    //         _inputBuffer.RemoveAt(0);
    //
    //         // イベントに対応するアクションを実行
    //         _stateMachine.Dispatch((int)inputEvent.EventType);
    //     }
    // }
}