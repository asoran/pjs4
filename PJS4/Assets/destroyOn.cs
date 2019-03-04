using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOn : MonoBehaviour {

	public int X = 3;
	private PlayerController player;

	void Start(){
		player = GameObject.Find ("player").GetComponent<PlayerController> ();
	}

	// Update is called once per frame
	void Update () {
		if (player.collectableCount >= X)
			Destroy (this.gameObject);
	}
}
