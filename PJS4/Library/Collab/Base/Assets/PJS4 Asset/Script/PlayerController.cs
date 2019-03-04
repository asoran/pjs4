using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
	public GameObject cp; //Checkpoint

	public float jumpForce = 0.5f;

	private bool canJump;
	private bool canMove;
	public float vitesse;
	private Rigidbody2D rb;
	public GameObject circle;

    // Use this for initialization
    void Start () {
		canJump = true;
		canMove = true;
		vitesse = 30;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";

		rb = GetComponent<Rigidbody2D> ();
    }

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log (col.gameObject.name);
		switch (col.gameObject.tag) {
		case "Fire":
			die ();
				break;
		case "JumpReset":
			canJump = true;
			break;
		case "cp":
			cp = col.gameObject;
			Debug.Log ("LE CP : " + col.gameObject);
			break;
		case "stop":
			canJump = true;
			canMove = false;
			rb.velocity = new Vector2 (0f, 0f);
			break;
		case "TP":
			Transform t = col.gameObject.GetComponent<TP> ().cible.transform;
			float offsetX = 2;
			float offsetY = 2;

			if (t.position.x < this.transform.position.x) {
				// On est à droite
				offsetX = -offsetX;
			}
			if (t.position.y < this.transform.position.y){
				// On est en haut
				offsetY = -offsetY;
			}

			this.transform.position = new Vector2 (t.position.x + offsetX, t.position.y + offsetY);
			break;
		}
	}

	public void die(){
		//gameObject.SetActive(false);
		this.transform.position = cp.transform.position;
		if(--this.gameObject.GetComponent<Timer>().vie <= 0)
			SceneManager.LoadScene (0); // Pierre bad

	}

    // Update is called once per frame
    void Update()
    {
			
		if (canJump && canMove) {
			//Version AddForce
			/*
			//float v = Input.GetAxis ("Vertical");
			float h = Input.GetAxis ("Horizontal");
			rb.AddForce (new Vector3 (h, 0.0f, v) * vitesse)
			*/

			//Version Velocity
			float h = Input.GetAxis ("Horizontal");
			rb.velocity = new Vector2 (h * vitesse, rb.velocity.y);
			//Version translate position
			/*
			var x = Input.GetAxis("Horizontal") * Time.deltaTime * vitesse;
			transform.Translate (x, 0, 0);
			*/
		}
    }

    private void FixedUpdate()
    {
		bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (grounded)
        {
			canJump = true;
        }

		if (canJump  && Input.GetMouseButtonDown(0))
        {
			canJump = false;
            JumpMouse();
        }
    }

    public void JumpMouse()
    {
 
		Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouse.z = 0;

		if (Vector3.Distance (mouse, transform.position) <= circle.GetComponent<DrawCircle>().xradius * gameObject.transform.localScale.x) {

			Vector3 velocity = mouse - transform.position;
			float distance = Vector3.Distance (mouse, transform.position);

			rb.velocity = CalcVelocity (velocity, distance);
        
		}
		

    }
		
    public Vector3 CalcVelocity(Vector3 diff, float distance)
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = diff.x * jumpForce * distance ;
        velocity.y = diff.y * jumpForce * distance ;

        return velocity;
    }

    public void OnThrown()
    {
        GetComponent<TrailRenderer>().enabled = true;
        
    }
	public float v = 10.0f;

	public void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.tag.Equals("Sticky")){
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / v);
			canJump = true;
		}else if(col.gameObject.tag.Equals("stop")){
			//rb.velocity = new Vector2(0, 0);

		
		}
	}

	public void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.tag.Equals("stop")){
			canMove = true;
		}
	}

}