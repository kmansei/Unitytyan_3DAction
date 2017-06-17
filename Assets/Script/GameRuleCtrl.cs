using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameRuleCtrl : MonoBehaviour {
	// ゲームオーバーフラグ
	public bool gameOver = false;
	// ゲームクリア
	public bool gameClear = false;
	// シーン移行時間
	public float sceneChangeTime = 3.0f;
	//オーディオ設定
	public AudioClip clearSeClip;
	AudioSource clearSeAudio;

	void Start(){
		//オーディオの初期化
		clearSeAudio = gameObject.AddComponent<AudioSource>();
		clearSeAudio.loop = false;
		clearSeAudio.clip = clearSeClip;
	}

	void Update()
	{
		// ゲーム終了条件成立後、シーン遷移
		if( gameOver || gameClear ){
			sceneChangeTime -= Time.deltaTime;
			if( sceneChangeTime <= 0.0f ){
				SceneManager.LoadScene("TitleScene");
			}
			return;
		}
	}

	public void GameOver(){
		gameOver = true;
	}
	public void GameClear(){
		gameClear = true;
		clearSeAudio.Play ();
	}
}
