#pragma strict

public var rotateCamera : Camera;
function Start () {
	rotateCamera = Camera.main;
}

function Update () {
	transform.rotation = rotateCamera.transform.rotation;
}

function Disable() {
		this.gameObject.SetActive(false);
	}