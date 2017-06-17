using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour {
	public enum ItemKind{
		Attack,
		Heal,
	};
	public ItemKind kind;

	public AudioClip itemSeClip;

	void OnTriggerEnter(Collider other){	
		if( other.tag == "Player" ){
			// アイテム取得
			CharacterStatus aStatus = other.GetComponent<CharacterStatus>();
			aStatus.GetItem(kind);
			Destroy(gameObject);

			// オーディオ再生
			AudioSource.PlayClipAtPoint(itemSeClip, transform.position);
		}
	}
}
