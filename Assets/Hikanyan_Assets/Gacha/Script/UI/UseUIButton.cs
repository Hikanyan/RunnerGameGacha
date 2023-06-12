using UnityEngine;
namespace Hikanyan.UI
{
    public class UseUIButton : MonoBehaviour
    {
        InputUIButton button;

        private void Start()
        {
            button = GetComponent<InputUIButton>(); // InputUIButtonのインスタンスを取得
            button.onClickCallback = () =>
            {
                Debug.Log("タップした時の処理");
                
            };
        }
    }
}