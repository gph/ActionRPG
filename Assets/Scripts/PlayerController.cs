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

    // deny player movement below this position.y
    private float UI_y;


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
        UI_y = transform.position.y - 3;

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (grounded)
        {
            doubleJump = false;
        }


        // convert mouse position to screen pos
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
        {
                Jump();            
        }

        if (magicActivated == false)
        {
            targetPosition = transform.position;
            if (Input.GetAxisRaw("Fire1") != 0)
            {
                if (ray.origin.y > UI_y)
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
            if (Input.GetMouseButtonDown(0) && ray.origin.y > UI_y)
            {
                
                targetPosition = new Vector3(ray.origin.x, transform.position.y, transform.position.z);

                if (!spellCasted)
                {
                    StartCoroutine(SpellCast(0.5F, targetPosition));
                }                
                spellCasted = true;
                playerAnimator.SetBool("casting", true);
            }
        }

        if (transform.position.y < -5)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
        }
    }    
    public void magic(GameObject spell)
    {
        magicActivated = true;
        this.spell = spell;
    }

    IEnumerator SpellCast(float waitTime, Vector3 position)
    {
        yield return new WaitForSeconds(waitTime);
        spell.transform.position = new Vector3(position.x, 5, position.z);
        spell.gameObject.SetActive(true);
        spellCasted = false;
        playerAnimator.SetBool("casting", false);
        magicActivated = false;
    }
    void Jump()
    {

        if (grounded)
        {
            doubleJump = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
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
