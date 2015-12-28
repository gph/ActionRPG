using UnityEngine;
using System.Collections;

public class DestroyItSelf : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
            Debug.Log("Hit " + coll.gameObject.name);
            coll.gameObject.SetActive(false);
        }
    }
}


