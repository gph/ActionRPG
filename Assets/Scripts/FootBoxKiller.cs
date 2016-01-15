using UnityEngine;
using System.Collections;

public class FootBoxKiller : MonoBehaviour {

    private Rigidbody2D rigidbody2d;

    // Use this for initialization
    void Start () {
        rigidbody2d = FindObjectOfType<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 10);
        }        
    }
}
