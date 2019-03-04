using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


    public Transform groundCheck;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public Vector2 groundRadius = new Vector2(3.35f , 0.31f);
    public LayerMask whatIsGround;
	public GameObject cp; //Checkpoint
	public int collectableCount;

	public float jumpForce = 0.5f;

	private bool canJump;
	private bool canMove;
	public float vitesse;
	private Rigidbody2D rb;
	public GameObject circle;
    public bool facingLeft;

    public SpriteRenderer sprite;

    // Use this for initialization
    void Start () {
		collectableCount = 0;
		canJump = true;
		canMove = true;
		vitesse = 30;
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";
        sprite = GetComponent<SpriteRenderer>();

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
		}
	}

	public void OnTriggerEnter2D(Collider2D col){
		switch (col.gameObject.tag) {
		case "pickup":
			Destroy (col.gameObject);
			++collectableCount;
			break;
		case "TP":
			Transform t = col.gameObject.GetComponent<TP> ().cible.transform;
			float offsetX = 2;
			float offsetY = 2;

			if (t.position.x < this.transform.position.x) {
				// On est àroite
				offsetX = -offsetX;
			}
			if (t.position.y > this.transform.position.y){
				// On est en bas
				offsetY = -offsetY;
			}

			this.transform.position = new Vector2 (t.position.x + offsetX, t.position.y + offsetY);
			break;
		}
	}

	public void die(){
		//gameObject.SetActive(false);
		this.transform.position = new Vector3 (cp.transform.position.x,cp.transform.position.y +5,cp.transform.position.z);
		if(--this.gameObject.GetComponent<Timer>().vie <= 0)
			SceneManager.LoadScene (3); // Pierre bad

	}

    // Update is called once per frame
    void Update()
    {
		
		if (Input.GetKeyDown (KeyCode.R))
			die ();

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
            if (h < 0 && !facingLeft)
            {

                Flip(true);
            }
            else if (h > 0 && facingLeft)
            {
                Flip(false);
            }
        }
    }

     void Flip(bool b){

        facingLeft = !facingLeft;
        sprite.flipX = b;
        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/
        

    }

private void FixedUpdate()
    {
        bool grounded = Physics2D.OverlapBox(groundCheck.position, groundRadius,0, whatIsGround,0,0);
        bool grounded2 = Physics2D.OverlapBox(groundCheck1.position, groundRadius, 0, whatIsGround, 0, 0);
        bool grounded3 = Physics2D.OverlapBox(groundCheck2.position, groundRadius, 0, whatIsGround, 0, 0);
        bool ground = grounded | grounded2 | grounded3;
        if (ground)
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

        if (Vector3.Distance(mouse, transform.position) <= circle.GetComponent<DrawCircle>().xradius * gameObject.transform.localScale.x)
        {


            Vector3 velocity = mouse - transform.position;
            float distance = Vector3.Distance(mouse, transform.position);

            rb.velocity = CalcVelocity(velocity, distance);

        }

        else if (Vector3.Distance(mouse, transform.position) > circle.GetComponent<DrawCircle>().xradius * gameObject.transform.localScale.x)
        {
            Debug.Log("test");
            /*Vector3 velocity = mouse - transform.position;
            float distance = Vector3.Distance(mouse, transform.position);

            Vector3 maxPosition = (transform.position - mouse).normalized * circle.GetComponent<DrawCircle>().xradius * gameObject.transform.localScale.x + mouse;
            Vector3 velocityMax = maxPosition - transform.position;
            float distanceMax = Vector3.Distance(maxPosition , transform.position);
            rb.velocity = CalcVelocity(velocityMax, distanceMax);*/

            float radius = circle.GetComponent<DrawCircle>().xradius * gameObject.transform.localScale.x;
            //float radius = circle.GetComponent<DrawCircle>().xradius * gameObject.transform.localScale.x;
            Vector3 dir = mouse - transform.position;
            Vector3 dirMax = dir.normalized * radius;

            Vector3 maxPosition = transform.position + dirMax;
            Vector3 velocity = maxPosition - transform.position;
            float distance = Vector3.Distance(maxPosition, transform.position);

            rb.velocity = CalcVelocity(velocity, distance);

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