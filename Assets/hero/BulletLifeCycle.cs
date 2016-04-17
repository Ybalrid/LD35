using UnityEngine;
using System.Collections;

public class BulletLifeCycle : MonoBehaviour {

    public float lifeSpan;
    private float spawnTime;

	// Use this for initialization
	void Start () {
        spawnTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.realtimeSinceStartup - spawnTime > lifeSpan)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "ShootTarget")
        {
            Destroy(gameObject);
        }
    }
}

