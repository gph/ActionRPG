using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Text texture;
    private Slider healthBar;
    public float maxHealth;
    private float health;

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
        maxHealth = 100;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if (texture.gameObject == true)
        //{
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            texture.transform.position = new Vector3(screenPos.x, screenPos.y + 20, screenPos.z);
        //}
        if (health <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.name == "Player")
        //{            
        //    //other.gameObject.SetActive(false);
        //    SceneManager.LoadScene("main");
        //    Debug.Log("Game was restarted");
        //}
        if (other.gameObject.tag == "Magic")
        {
            //gameObject.SetActive(false);
            Debug.Log("Enemy was hit");
            Damage((int)(Random.value * 10) + 20);
        }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Foot Hit!");
            Damage((int)(Random.value * 10) + 30);
        }
    }

    void Damage(float damage)
    {

        texture.text = damage.ToString();
        health -= damage;
        healthBar.value -= maxHealth * damage / 100;
        
        // Start coroutine to show damage for 1 second and clean the text field
        StartCoroutine(WaitAndPrint(0.5F));
    }
    public void EnemyPreRespawn()
    {
        health = 100;
        healthBar.value = 100;
        texture.text = "";
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        texture.text = "";
    }
}
