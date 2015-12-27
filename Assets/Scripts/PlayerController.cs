using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray.origin.x + " " + ray.origin.y);
            Vector2 new_pos = new Vector2(ray.origin.x, ray.origin.y);
            transform.position = new_pos;

        }

    }
}
