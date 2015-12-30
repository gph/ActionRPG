using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public GameObject myCanvas;
    public GameObject enemyPrefab;
    GameObject enemySpawned;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            enemySpawned = (GameObject)Instantiate(enemyPrefab, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), Quaternion.identity);
            enemySpawned.transform.parent = myCanvas.transform;
        }
    }
}
