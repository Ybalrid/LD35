using UnityEngine;
using System.Collections;

public class EnemyBulletLifeCycle : MonoBehaviour {

    // Use this for initialization
    public float lifeSpan;
    private float spawnTime;
	void Start () {
        spawnTime = Time.realtimeSinceStartup;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.realtimeSinceStartup - spawnTime > lifeSpan)
            Destroy(gameObject);
	
	}
}
