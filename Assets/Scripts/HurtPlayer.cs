using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HurtPlayer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Destroy Player");
            other.gameObject.SetActive(false);
            SceneManager.LoadScene("main");

        }
    }
}
