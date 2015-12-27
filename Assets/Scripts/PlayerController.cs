using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour{
    
    private Vector3 targetPosition, velocity = Vector3.zero;
    public float maxSpeed = 5f;
    public Camera mainCamera;
    public float jumpHeight = 5f;

    private Rigidbody2D rigidbody2d;

    public Transform groundCheck;
    private bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool doubleJumped;


    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update() {
        // camera follows player position
        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        // check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
       
        walk();

        if (isGrounded)
        {
            doubleJumped = false;
        }

    }

    public void jump() {
        if (isGrounded)
        {
            rigidbody2d.velocity = new Vector2(0, jumpHeight);
        }
        else
        {
            if(doubleJumped == false)
            {
                rigidbody2d.velocity = new Vector2(0, jumpHeight);
                doubleJumped = true;
            }            
        }
    }
    private void walk() {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (ray.origin.y > -1) {
                targetPosition = new Vector3(ray.origin.x, transform.position.y, transform.position.z);
            }
            Debug.Log("Target position\n X: " + ray.origin.x + " Y: " + ray.origin.y);
        }
        if (transform.position.x != targetPosition.x) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1F, maxSpeed);
        }
    }
}
