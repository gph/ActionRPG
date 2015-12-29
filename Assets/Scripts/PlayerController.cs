﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour{

    private Vector3 targetPosition, velocity = Vector3.zero;
    public float maxSpeed = 5f;
    public Camera mainCamera;
    public float jumpHeight = 8f;

    public Transform targetClicked;
    public GameObject iceSpike;

    private bool magicRunning = false;

    // Use this for initialization
    void Start () {
        targetPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update() {
        // camera follows player position
        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!magicRunning)
        {
            targetPosition = transform.position;
            if (Input.GetAxisRaw("Fire1") != 0)
            {
                if (ray.origin.y > -1)
                {
                    targetPosition = new Vector3(ray.origin.x, transform.position.y, transform.position.z);
                }
                //Debug.Log("Target position\n X: " + ray.origin.x + " Y: " + ray.origin.y);
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
                    targetClicked.position = new Vector3(ray.origin.x, transform.position.y, transform.position.z);
                }
                // instatiate iceSpike from prefab
                //iceSpike = (GameObject)Instantiate(iceSpike, new Vector3(targetClicked.position.x, 5, targetClicked.position.z),transform.rotation);

                iceSpike.transform.position = new Vector3(targetClicked.position.x, 5, targetClicked.position.z);
                iceSpike.gameObject.SetActive(true);
                magicRunning = !magicRunning;
            }
        }
    }    
    public void magic()
    {
        magicRunning = !magicRunning;
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
    }
}
