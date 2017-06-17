using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	CharacterStatus status;
	private GameObject attacker;
	public AudioClip hitSeClip;
	AudioSource hitSeAudio;

	void Start(){
		status = transform.root.GetComponentInChildren<CharacterStatus>();

		hitSeAudio = gameObject.AddComponent<AudioSource>();
		hitSeAudio.clip = hitSeClip;
		hitSeAudio.loop = false;
	}


	public class AttackInfo{
		public int attackPower; 
		public Transform attacker; 
    }
	// 攻撃情報を取得する.
	AttackInfo GetAttackInfo(){			
		AttackInfo attackInfo = new AttackInfo();
		attackInfo.attackPower = status.Power;
		attackInfo.attacker = transform.parent;
		return attackInfo;
	}

	void OnTriggerEnter(Collider other){
		other.SendMessage("Damage",GetAttackInfo());
       	hitSeAudio.Play();
	}

	// 攻撃判定を有効にする.
	void OnAttack(){
		GetComponent<Collider>().enabled = true;
	}

	// 攻撃判定を無効にする.
	void OnAttackTermination(){
		GetComponent<Collider>().enabled = false;
	}
}
