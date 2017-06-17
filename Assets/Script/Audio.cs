using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	public AudioClip clearSeClip;
	AudioSource clearSeAudio;

	void Start(){
		
		clearSeAudio = gameObject.AddComponent<AudioSource>();
		clearSeAudio.loop = false;
		clearSeAudio.clip = clearSeClip;
	}
}
