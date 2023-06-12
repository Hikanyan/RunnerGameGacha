using UnityEngine;
namespace Hikanyan.UI
{
    public class UseUIButton : MonoBehaviour
    {
        [SerializeField] private GameState _gameState = GameState.None;
        InputUIButton button;

        private void Start()
        {
            button = GetComponent<InputUIButton>(); // InputUIButtonのインスタンスを取得
            button.onClickCallback = () =>
            {
                Debug.Log("タップした時の処理");
                GameManager.Instance._stateMachine.Dispatch((int)_gameState);
            };
        }
    }
}