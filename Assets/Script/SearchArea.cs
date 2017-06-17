using UnityEngine;
using System.Collections;

public class SearchArea : MonoBehaviour {
	EnemyCtrl enemyCtrl;
	void Start(){
		enemyCtrl = transform.parent.GetComponentInChildren<EnemyCtrl>();
	}
	
	void OnTriggerStay( Collider other ){
        // Playerタグをターゲットにする
		if( other.tag == "Player" )
			enemyCtrl.SetAttackTarget( other.transform );
	}
}
