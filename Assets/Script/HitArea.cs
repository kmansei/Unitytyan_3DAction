﻿using UnityEngine;
using System.Collections;

public class HitArea : MonoBehaviour {

	void Damage(AttackArea.AttackInfo attackInfo){
		transform.parent.SendMessage ("Damage",attackInfo);
	}
}
