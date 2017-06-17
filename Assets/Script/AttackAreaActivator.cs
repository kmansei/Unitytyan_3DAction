using UnityEngine;
using System.Collections;

public class AttackAreaActivator : MonoBehaviour {
	Collider[] attackAreaColliders; 

	public AudioClip attackSeClip;
	AudioSource attackSeAudio;

	public AudioClip attackVoiceClip;
	AudioSource attackVoiceAudio;

	void Start () {
		// 子供のGameObjectからAttackAreaスクリプトがついているGameObjectを探す。
		AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();
		attackAreaColliders = new Collider[attackAreas.Length];

		for (int attackAreaCnt = 0; attackAreaCnt < attackAreas.Length; attackAreaCnt++) {
			// AttackAreaスクリプトがついているGameObjectのコライダを配列に格納する.
			attackAreaColliders[attackAreaCnt] = attackAreas[attackAreaCnt].GetComponent<Collider>();
			attackAreaColliders[attackAreaCnt].enabled = false;  
		}

		attackSeAudio = gameObject.AddComponent<AudioSource>();
		attackSeAudio.clip = attackSeClip;
		attackSeAudio.loop = false;

		attackVoiceAudio = gameObject.AddComponent<AudioSource>();
		attackVoiceAudio.clip = attackVoiceClip;
		attackVoiceAudio.loop = false;
	}

	// アニメーションイベントのStartAttackHitを受け取ってコライダを有効にする
	void StartAttackHit(){
		foreach (Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = true;

		attackSeAudio.Play();
        attackVoiceAudio.Play();
	}

	// アニメーションイベントのEndAttackHitを受け取ってコライダを無効にする
	void EndAttackHit(){
		foreach (Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = false;
	}
}
