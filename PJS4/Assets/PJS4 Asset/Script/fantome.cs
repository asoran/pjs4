using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fantome : MonoBehaviour {

	BoxCollider2D b;
	SpriteRenderer s;

	// Use this for initialization
	void Start () {
		b = GetComponent<BoxCollider2D> ();
		s = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter2D(Collision2D other){
		StartCoroutine (disp ());
	}

	IEnumerator disp()
	{
		yield return new WaitForSeconds(2);
		s.enabled = false; //NANI ???
		b.enabled = false;
		StartCoroutine(apparait());
	}

	IEnumerator apparait(){
		yield return new WaitForSeconds(10);
		s.enabled = true; //NANI ???
		b.enabled = true;
	}

}
