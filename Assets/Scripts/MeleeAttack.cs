using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour {

    private bool isOnTrigger;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActive(true);
            }   
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
