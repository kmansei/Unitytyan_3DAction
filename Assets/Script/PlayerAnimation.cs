using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
	private Animator animator;
	private CharacterStatus status;
    bool ConboAttack;

	void Start () {
		animator = GetComponent<Animator> ();
		status = GetComponent<CharacterStatus> ();
        ConboAttack = false;
	}

	void Update () {
		if (Input.GetButtonUp("Fire1") && status.attacking == true){
			ConboAttack = true;
		}

        if (Input.GetButtonUp("Fire1") && status.attacking == false) {
			animator.SetBool ("Attacking", true);
			status.attacking = true;
		}

	}

	void JudgeConboAttack(){
        if (ConboAttack == true){
			animator.SetBool("ConboAttack", true);
        } else {
            EndAttack();
        }
	}

	void EndAttack() {
		animator.SetBool("Attacking", false);
        animator.SetBool("ConboAttack", false);
		status.attacking = false;
        ConboAttack = false;
	}
}
