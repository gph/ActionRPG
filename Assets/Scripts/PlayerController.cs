using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour{

    private Vector3 targetPosition, velocity = Vector3.zero;
    public float maxSpeed = 5f;
    public Camera mainCamera;

    public GameObject iceSpike;

    private bool magicActivated = false;
    private bool spellCasted = false;

    Animator playerAnimator;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        targetPosition = transform.position;
        
    }
	
	// Update is called once per frame
	void Update() {
        // camera follows player position
        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);

        // convert mouse position to screen pos
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (magicActivated == false)
        {
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

                // instatiate iceSpike from prefab
                //iceSpike = (GameObject)Instantiate(iceSpike, new Vector3(targetClicked.position.x, 5, targetClicked.position.z),transform.rotation);

                if (!spellCasted)
                {
                    StartCoroutine(SpellCast(0.5F, targetPosition));
                }                
                spellCasted = true;
                playerAnimator.SetBool("casting", true);
                Debug.Log("WOW");
            }
        }
    }    
    public void magic()
    {
        magicActivated = !magicActivated;
    }

    IEnumerator SpellCast(float waitTime, Vector3 position)
    {
        yield return new WaitForSeconds(waitTime);
        iceSpike.transform.position = new Vector3(position.x, 5, position.z);
        iceSpike.gameObject.SetActive(true);
        magicActivated = !magicActivated;
        spellCasted = false;
        playerAnimator.SetBool("casting", false);
    }
}
