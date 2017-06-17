using UnityEngine; 
using System.Collections;

public class PlayerMove : MonoBehaviour {

	Vector3 direction; 
	public float move_speed = 5.0f; 
	public float rotate_speed = 180f;  
	private float gravity = 9.8f;  
	private Animator anim; 
	CharacterController chara;
	public CharaAnimation CharaAni;
	CharacterStatus status;

	Transform cam_trans; 
	void Start () { 
		chara = GetComponent<CharacterController>(); 
		anim = GetComponentInChildren<Animator>(); 
		cam_trans = GameObject.Find("Main Camera").GetComponent<Transform>(); 
		CharaAni = GetComponent<CharaAnimation>();
		status = GetComponent<CharacterStatus>();

	}
		
	void Update () {
		
		if (chara.isGrounded) {
			direction = (cam_trans.transform.right * Input.GetAxis("Horizontal")) + (cam_trans.transform.forward * Input.GetAxis("Vertical")); 

			if (direction.sqrMagnitude > 0.1f) { 
				if (status.attacking == false) {
					Vector3 forward = Vector3.Slerp (transform.forward, direction, rotate_speed * Time.deltaTime / Vector3.Angle (transform.forward, direction)); 
					transform.LookAt (transform.position + forward);
				}
			}
				 
			if (Input.GetButton("Fire1")) { 
				anim.SetBool ("Attacking", true);
				CharaAni.attacked = true;
			} else {
				anim.SetBool ("Attacking", false);
				CharaAni.attacked = false;
			}

		}
		direction.y -= gravity * Time.deltaTime;
	
		if(status.attacking == false){
			chara.Move(direction * Time.deltaTime * move_speed); 
			anim.SetFloat("Speed", chara.velocity.magnitude);
			if(status.died){
				chara.Move(direction * Time.deltaTime * move_speed * 0); 
			}
		}

	}
		
} 