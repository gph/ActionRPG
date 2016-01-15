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
        //enemySpawned = (GameObject)Instantiate(enemyPrefab, new Vector3(transform.position.x + Random.value * 10 +1, transform.position.y, transform.position.z), Quaternion.identity);
        //enemySpawned.transform.parent = myCanvas.transform;
    }
}
