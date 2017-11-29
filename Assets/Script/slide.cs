using UnityEngine;
using System.Collections;

public class slide : MonoBehaviour {

	private Vector3 slide_direction;

	public float slide_magnitude = 0.5f;

	const float gravity = 9.8f;
	bool isSliding = false;

	RaycastHit slideHit;
	CharacterController chara;

	void Start () {
		chara = GetComponent<CharacterController>(); 
	}

	void Update () {
		if (Physics.Raycast(transform.position, Vector3.down, out slideHit, LayerMask.NameToLayer("Ground"))) {
			if (Vector3.Angle(slideHit.normal, Vector3.up) > 20){
				isSliding = true;
			}else{
				isSliding = false;
			}
		}

		if (Physics.Raycast(transform.position, Vector3.down, out slideHit, LayerMask.NameToLayer("Defaul"))) {
			if (Vector3.Angle(slideHit.normal, Vector3.up) > 0){
				isSliding = true;
			}else{
				isSliding = false;
			}
		}

		if(isSliding){//滑るフラグが立ってたら
			Vector3 hitNormal = slideHit.normal;
			slide_direction.x = hitNormal.x * slide_magnitude;
			slide_direction.y -= gravity * Time.deltaTime;
			slide_direction.z = hitNormal.z * slide_magnitude;
			chara.Move(slide_direction); 
		}
	
	}
}
