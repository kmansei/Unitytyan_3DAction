using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour
{
	const float RayCastMaxDistance = 100.0f;
	CharacterStatus status;
	CharaAnimation charaAnimation;
	Transform attackTarget;
	InputManager inputManager;
	public float attackRange = 1.5f;
	GameRuleCtrl gameRuleCtrl;
	public GameObject hitEffect;

	enum State{
		Walking,
		Attacking,
		Died,
	};

	State state = State.Walking;	
	State nextState = State.Walking;

	public AudioClip deathSeClip;
	AudioSource deathSeAudio;

	void Start(){
		status = GetComponent<CharacterStatus>();
		charaAnimation = GetComponent<CharaAnimation>();
		inputManager = FindObjectOfType<InputManager>();
		gameRuleCtrl = FindObjectOfType<GameRuleCtrl>();

		deathSeAudio = gameObject.AddComponent<AudioSource>();
		deathSeAudio.loop = false;
		deathSeAudio.clip = deathSeClip;
	}

	void Update(){
		switch (state){
		case State.Walking:
			Walking();
			break;
		case State.Attacking:
			Attacking();
			break;
		}

		if (state != nextState){
			state = nextState;
			switch (state){
			case State.Walking:
				WalkStart();
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
	}

	void AttackStart(){
	}

	void Attacking(){
		if (charaAnimation.IsAttacked())
			ChangeState(State.Walking);
	}

	void Died(){
		status.died = true;
		gameRuleCtrl.GameOver();

		deathSeAudio.Play ();
	}

	void Damage(AttackArea.AttackInfo attackInfo){
		GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
		effect.transform.localPosition = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
		Destroy(effect, 0.3f);

		status.HP -= attackInfo.attackPower;
		if (status.HP <= 0){
			status.HP = 0;
			ChangeState(State.Died);
		}
	}

	void StateStartCommon(){
		status.attacking = false;
		status.died = false;
	}
}

