using UnityEngine;
using System.Collections;

public class EnemyCtrl : MonoBehaviour {
	CharacterStatus status;
	CharaAnimation charaAnimation;
	CharacterMove characterMove;
	Transform attackTarget;
	GameRuleCtrl gameRuleCtrl;
	public GameObject hitEffect;

	// 待機時間は２秒
	public float waitBaseTime = 2.0f;
	// 残り待機時間
	float waitTime;
	// 移動範囲５メートル
	public float walkRange = 5.0f;
	// 初期位置を保存しておく変数
	public Vector3 basePosition;
	// 複数のアイテム
	public GameObject[] dropItemPrefab;

	// ステートの種類.
	enum State {
		Walking,	
		Chasing,	
		Attacking,	
		Died,       
	};

	State state = State.Walking;		// 現在のステート.
	State nextState = State.Walking;	// 次のステート.

	public AudioClip deathSeClip;
	AudioSource deathSeAudio;

	void Start () {
		status = GetComponent<CharacterStatus>();
		charaAnimation = GetComponent<CharaAnimation>();
		characterMove = GetComponent<CharacterMove>();
		gameRuleCtrl = FindObjectOfType<GameRuleCtrl>();
		basePosition = transform.position;
		waitTime = waitBaseTime;
	}

	void Update () {
		switch (state) {
		case State.Walking:
			Walking();
			break;
		case State.Chasing:
			Chasing();
			break;
		case State.Attacking:
			Attacking();
			break;
		}

		if (state != nextState){
			state = nextState;
			switch (state) {
			case State.Walking:
				WalkStart();
				break;
			case State.Chasing:
				ChaseStart();
				break;
			case State.Attacking:
				AttackStart();
				break;
			case State.Died:
				Died();
				break;
			}
		}
	}

	// ステートを変更する.
	void ChangeState(State nextState){
		this.nextState = nextState;
	}

	void WalkStart(){
		StateStartCommon();
	}

	void Walking(){
		// 待機時間がまだあったら
		if (waitTime > 0.0f){
			// 待機時間を減らす
			waitTime -= Time.deltaTime;
			// 待機時間が無くなったら
			if (waitTime <= 0.0f){
				// 範囲内の何処か
				Vector2 randomValue = Random.insideUnitCircle * walkRange;
				// 移動先の設定
				Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
				//　目的地の指定.
				SendMessage("SetDestination", destinationPosition);
			}
		}
		else
		{
			// 目的地へ到着
			if (characterMove.Arrived()){
				// 待機状態へ
				waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
			}
			// ターゲットを発見したら追跡
			if (attackTarget){
				ChangeState(State.Chasing);
			}
		}
	}
	// 追跡開始
	void ChaseStart(){
		StateStartCommon();
	}
	// 追跡中
	void Chasing(){
		// 移動先をプレイヤーに設定
		SendMessage("SetDestination", attackTarget.position);
		// 2m以内に近づいたら攻撃
		if (Vector3.Distance( attackTarget.position, transform.position ) <= 2.0f){
			ChangeState(State.Attacking);
		}
	}

	// 攻撃ステートが始まる前に呼び出される.
	void AttackStart(){
		StateStartCommon();
		status.attacking = true;

		// 敵の方向に振り向かせる.
		Vector3 targetDirection = (attackTarget.position-transform.position).normalized;
		SendMessage("SetDirection",targetDirection);

		// 移動を止める.
		SendMessage("StopMove");
	}

	// 攻撃中の処理.
	void Attacking()
	{
		if (charaAnimation.IsAttacked())
			ChangeState(State.Walking);
		// 待機時間を再設定
		waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
		// ターゲットをリセットする
		attackTarget = null;
	}

	void dropItem(){
		if (dropItemPrefab.Length == 0) { return; }
		GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
		Instantiate(dropItem, transform.position, Quaternion.identity);
	}

	void Died(){
		status.died = true;
		dropItem();
		Destroy(gameObject);
		if (gameObject.tag == "Boss"){
			gameRuleCtrl.GameClear();
		}

		// オーディオ再生
		AudioSource.PlayClipAtPoint(deathSeClip, transform.position);
	}

	void Damage(AttackArea.AttackInfo attackInfo){
		GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
		effect.transform.localPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
		Destroy(effect, 0.3f);

		status.HP -= attackInfo.attackPower;
		if (status.HP <= 0) {
			status.HP = 0;
			// 体力０なので死亡
			ChangeState(State.Died);
		}
	}

	// ステートが始まる前にステータスを初期化する.
	void StateStartCommon(){
		status.attacking = false;
		status.died = false;
	}
	// 攻撃対象を設定する
	public void SetAttackTarget(Transform target){
		attackTarget = target;
	}
}
