using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler
, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler 
, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
{
	//　マウスが侵入した時
	public void OnPointerEnter(PointerEventData eventData) {
		Debug.Log ("マウスが侵入");
	}

	//　マウスが出て行った時
	public void OnPointerExit(PointerEventData eventData) {
		Debug.Log ("マウスが出て行った");
	}

	//　マウスが押された時
	public void OnPointerDown(PointerEventData eventData) {
		Debug.Log ("マウスボタンが押された");
	}

	//　マウスが離された時
	public void OnPointerUp(PointerEventData eventData) {
		Debug.Log ("マウスボタンが離された");
	}

	//　マウスがクリックされた時
	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log ("マウスがクリックされた");
	}

	//　マウスドラッグ対象が見つかった時
	public void OnInitializePotentialDrag(PointerEventData eventData) {
		Debug.Log ("マウスドラッグ開始前");
	}

	//　マウスドラッグが開始された
	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("マウスドラッグが開始された");
	}

	//　マウスがドラッグされている
	public void OnDrag(PointerEventData eventData) {
		Debug.Log ("マウスドラッグされている");
	}

	//　マウスドラッグが終了
	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("マウスドラッグが終了");
	}

	//　オブジェクトがドロップされた
	public void OnDrop(PointerEventData eventData) {
		Debug.Log ("ドロップされた");
	}

	//　マウスホイールをスクロールしている時
	public void OnScroll(PointerEventData eventData) {
		Debug.Log ("マウスホイールスクロールしている");
	}

	//　オブジェクトが選択されている時（毎フレーム）
	public void OnUpdateSelected(BaseEventData eventData) {
		Debug.Log ("オブジェクトが選択されている");
	}

	//　オブジェクトが選択されている時（選択された瞬間）
	public void OnSelect(BaseEventData eventData) {
		Debug.Log ("オブジェクトが選択された");
	}

	//　オブジェクトが選択解除された
	public void OnDeselect(BaseEventData eventData) {
		Debug.Log ("オブジェクトが選択解除された");
	}

	//　キー入力による移動
	public void OnMove(AxisEventData eventData) {
		Debug.Log ("キー入力による移動");
	}

	//　Submitボタンが押された
	public void OnSubmit(BaseEventData eventData) {
		Debug.Log ("サブミットボタンが押された");
	}

	//　Cancelボタンが押された
	public void OnCancel(BaseEventData eventData) {
		Debug.Log ("キャンセルボタンが押された");
	}
}