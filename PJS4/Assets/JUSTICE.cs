using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JUSTICE : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		int count = ((PlayerController)GameObject.Find ("player").GetComponent<PlayerController> ()).collectableCount;
		Debug.Log (count);
		SceneManager.LoadScene (count >= 17 ? 5 : 4);

	}
}
