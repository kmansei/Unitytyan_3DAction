#pragma strict

function Update () {
	transform.rotation = Camera.main.transform.rotation;
}

function Disable() {
	this.gameObject.SetActive(false);
}
