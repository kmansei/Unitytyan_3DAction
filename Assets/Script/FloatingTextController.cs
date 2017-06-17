using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour{
	private static FloatingText popupTextPrefab;
	private static GameObject canvas;

	public static void Initialize(){
		canvas = GameObject.Find("Canvas");
		if (!popupTextPrefab){
			popupTextPrefab = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
		}
	}

	public static void CreateFloatingText(string text, Transform textTransform){
		FloatingText instance = Instantiate(popupTextPrefab) as FloatingText;
		bool worldPositionStays = false;

		// ワールド空間の position をスクリーン空間に変換。
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(textTransform.position);

		instance.transform.SetParent(canvas.transform, worldPositionStays);
		instance.transform.position = screenPosition;
		instance.SetText(text);
	}
}