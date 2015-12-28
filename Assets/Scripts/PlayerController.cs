using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour{

    private Vector3 targetPosition, velocity = Vector3.zero;
    public float maxSpeed = 5f;
    public Camera mainCamera;
    public float jumpHeight = 8f;

    //private Rigidbody2D rigidbody2d;

    //public Transform groundCheck;
    //private bool isGrounded;
    //public float groundCheckRadius;
    //public LayerMask whatIsGround;

    public Transform targetClicked;
    public GameObject iceSpike;

    private bool magicRunning = false;

    // Use this for initialization
    void Start () {
//        rigidbody2d = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update() {
        // camera follows player position
        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        // check if player is grounded
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetAxisRaw("Fire1") != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (ray.origin.y > -1)
            {
                if (magicRunning)
                {
                    targetClicked.position = new Vector3(ray.origin.x, transform.position.y, transform.position.z);
                    targetPosition = transform.position;
                }
                else
                {
                    targetPosition = new Vector3(ray.origin.x, transform.position.y, transform.position.z);
                    targetClicked.position = targetPosition;
                }
                
                targetClicked.position = targetPosition;
                //Debug.Log("targetClicked.position.y : " + targetClicked.position.y);
            }
            //Debug.Log("Target position\n X: " + ray.origin.x + " Y: " + ray.origin.y);
        }
        if (transform.position.x != targetPosition.x)
        {
            if (targetPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1F, maxSpeed);
        }

        if (magicRunning)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                iceSpike.transform.position = new Vector3(targetClicked.position.x, 5, targetClicked.position.z);
                iceSpike.gameObject.SetActive(true);
                targetPosition = transform.position;
                magicRunning = !magicRunning;
            }
        }

        //if(Input.GetAxisRaw("Jump") != 0)
        //{
        //    jump();
        //

    }
    //public void jump() {
    //    if (isGrounded)
    //    {
    //        rigidbody2d.velocity = new Vector2(0, jumpHeight);
    //    }
    //}
    
    public void magic()
    {
        magicRunning = !magicRunning;
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
    }
}
