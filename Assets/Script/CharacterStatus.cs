using UnityEngine;
using System.Collections;

public class CharacterStatus : MonoBehaviour
{
	// 体力.
	public int HP = 100;
	public int MaxHP = 100;

	// 攻撃力.
	public int Power = 10;

	// 最後に攻撃した対象.
	public GameObject lastAttackTarget = null;

	// プレイヤー名.
	public string characterName = "Player";

	//状態.
	public bool attacking = false;
	public bool died = false;

	// アイテム取得
	public void GetItem(DropItem.ItemKind itemKind){
		switch (itemKind){
		case DropItem.ItemKind.Heal:
			// MaxHPの半分回復
			HP = Mathf.Min(HP + MaxHP / 2, MaxHP);
			break;
		}
	}

	void Start(){
	}

	void Update(){
		if (gameObject.tag != "Player"){
			return;
		}
	}
}
