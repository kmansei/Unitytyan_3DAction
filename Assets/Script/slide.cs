using UnityEngine;
using System.Collections;

public class slide : MonoBehaviour {


	public PlayerMove Move;

	RaycastHit slideHit;

	bool isSliding = false;

	public Vector3 down;

	public float slideSpeed = 1.0f;

	const float gravity = 9.8f;

	CharacterController chara;

	// Use this for initialization
	void Start () {
		Move = GetComponent<PlayerMove>();
		chara = GetComponent<CharacterController>(); 
	}
	
	// Update is called once per frame
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
			down.x = hitNormal.x*slideSpeed;
			down.y -= gravity * Time.deltaTime;//重力落下
			down.z = hitNormal.z*slideSpeed;
			chara.Move(down); 
		}
	
	}
}
