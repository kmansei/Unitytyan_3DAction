using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetHp : MonoBehaviour {

	private UnityEngine.UI.Slider slider;
	private CharacterStatus status;
	private int count;
	private int countNow;

	void Start () {
		status = GetComponentInParent<CharacterStatus> ();
		slider = GetComponent<UnityEngine.UI.Slider>();
		slider.value = slider.maxValue;
		count = status.MaxHP;
	}
	
	void Update () {
		countNow = status.HP;

		if(countNow != count) {
			slider.value = (countNow * slider.maxValue) / status.MaxHP;
			count = countNow;
		}
  }
}