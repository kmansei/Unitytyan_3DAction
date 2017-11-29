using UnityEngine; 
using System.Collections;

public class PlayerMove : MonoBehaviour {

	Vector3 input_direction;
	Vector3 direction;
    Vector3 old_direction = new Vector3(0, 0, 0);
	Vector3 camera_forward;

	public float move_speed = 5.0f; 
	public float rotate_speed = 180f;
    public CharaAnimation CharaAni;

	private float gravity = 9.8f;  
	private Animator anim; 

	CharacterController chara;
	CharacterStatus status;

	void Start () { 
		chara = GetComponent<CharacterController>(); 
		anim = GetComponentInChildren<Animator>(); 
		CharaAni = GetComponent<CharaAnimation>();
		status = GetComponent<CharacterStatus>();

	}
		
	void Update () {
		
		if (chara.isGrounded) {
			camera_forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
			direction = camera_forward * Input.GetAxis ("Vertical") + Camera.main.transform.right * Input.GetAxis ("Horizontal");

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

		direction = direction.normalized;
		direction += Vector3.down * gravity * Time.deltaTime;
	
		if(status.attacking == false){
            direction = Vector3.Lerp(old_direction, direction, Mathf.Min(Time.deltaTime * 5.0f, 1.0f));
			chara.Move(direction * Time.deltaTime * move_speed); 
			anim.SetFloat("Speed", chara.velocity.magnitude);
			if(status.died){
				chara.Move(direction * Time.deltaTime * move_speed * 0); 
			}
            old_direction = direction;
		}

	}
		
} 