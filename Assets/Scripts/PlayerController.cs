using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

    private Vector3 targetPosition, velocity = Vector3.zero;
    public float maxSpeed = 5f;
    public Camera mainCamera;

    private GameObject spell;

    private bool magicActivated = false;
    private bool spellCasted = false;

    Animator playerAnimator;

    // JUMP
    public float jumpHeight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool doubleJump;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        targetPosition = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        // camera follows player position
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // convert mouse position to screen pos
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (magicActivated == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2")) {
                Jump();
            }
            targetPosition = transform.position;
            if (Input.GetAxisRaw("Fire1") != 0)
            {
                if (ray.origin.y > -1)
                {
                    targetPosition = new Vector3(ray.origin.x, transform.position.y, transform.position.z);
                }
            }
            if (transform.position.x != targetPosition.x)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1F, maxSpeed);
            }
            if (targetPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if(targetPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        { 
            if (Input.GetMouseButtonDown(0) && ray.origin.y > -1)
            {
                
                if (ray.origin.y > -1)
                {
                    targetPosition = new Vector3(ray.origin.x, transform.position.y, transform.position.z);
                }
                if (!spellCasted)
                {
                    StartCoroutine(SpellCast(0.5F, targetPosition));
                }                
                spellCasted = true;
                playerAnimator.SetBool("casting", true);
            }
        }
    }    
    public void magic(GameObject spell)
    {
        magicActivated = !magicActivated;
        this.spell = spell;
    }

    IEnumerator SpellCast(float waitTime, Vector3 position)
    {
        yield return new WaitForSeconds(waitTime);
        spell.transform.position = new Vector3(position.x, 5, position.z);
        spell.gameObject.SetActive(true);
        magicActivated = !magicActivated;
        spellCasted = false;
        playerAnimator.SetBool("casting", false);
    }
    public void Jump()
    {
        if (grounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            doubleJump = false;
        }
        else
        {
            if (!doubleJump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                doubleJump = true;
            }
        }        
    }
}
