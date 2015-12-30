using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Text texture;
    private Slider healthBar;


    // Use this for initialization
    void Start()
    {
        texture = GetComponentInChildren<Text>();
        //texture = (Text)FindObjectOfType(typeof(Text));
        //if (texture)
        //    Debug.Log("Text object found: " + texture.name);
        //else
        //    Debug.Log("No Text object could be found");
        
        healthBar = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (texture.gameObject == true)
        //{
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            texture.transform.position = new Vector3(screenPos.x, screenPos.y + 20, screenPos.z);
        //}
        if (healthBar.value == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {            
            //other.gameObject.SetActive(false);
            SceneManager.LoadScene("main");
            Debug.Log("Game was restarted");
        }
        if (other.gameObject.tag == "Magic")
        {
            //gameObject.SetActive(false);
            Debug.Log("Enemy was hit");
            Damage((int)(Random.value * 10) + 20);
        }
    }

    void Damage(float damage)
    {

        texture.text = damage.ToString();
        healthBar.value -= damage;
        // Start coroutine to show damage for 1 second and clean the text field
        StartCoroutine(WaitAndPrint(1.0F));


    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        //texture.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        //texture.gameObject.SetActive(false);
        texture.text = "";
    }
}
