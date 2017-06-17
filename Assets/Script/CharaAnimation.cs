using UnityEngine;

public class CharaAnimation : MonoBehaviour {
	
	Animator animator;
	CharacterStatus status;
	Vector3 prePosition;
	public bool isDown = false;
	public bool attacked = false;
	public BoxCollider box_col;
	
	public bool IsAttacked(){
		return attacked;
	}
	
	void Start (){
		animator = GetComponent<Animator>();
		status = GetComponent<CharacterStatus>();
		prePosition = transform.position;
	}
	
	void Update (){
		Vector3 delta_position = transform.position - prePosition;
		animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);
		
		if(attacked && status.attacking == true){
			attacked = false;
		}

		animator.SetBool("Attacking", (!attacked && status.attacking));

		if(!isDown && status.died){
			isDown = true;
			animator.SetTrigger("Down");
		}

		prePosition = transform.position;
	}

	void StartAttackHit(){
	}

	void EndAttackHit(){
	}

	void EndAttack(){
		attacked = true;
		status.attacking = false;
	}
}