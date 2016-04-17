using UnityEngine;
using System.Collections;

public class DieOnBeingShot : MonoBehaviour {

    public int life;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "HeroBullet") ;
        if (--life >= 0)
            Destroy(gameObject);
    }
}
