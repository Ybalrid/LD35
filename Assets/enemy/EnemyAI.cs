using UnityEngine;
using System.Collections;
using System;

public class EnemyAI : MonoBehaviour {

    public float damping;
    public Rigidbody bullet;
    public float threshold;
    public float shootRate;
    public float bulletSpeed;

    private float lastShootTime;
    public Vector3 aimOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 target = StaticLevel.HeroPosition;
        Vector3 inBetween = target - transform.position + aimOffset;

        Vector3 lookAt = Vector3.Lerp(transform.forward, inBetween, damping);
        transform.LookAt(target);

        if(inBetween.magnitude <= threshold)
        {
            shoot(target);
        }

	
	}

    private void shoot(Vector3 target)
    {
        float currentTime = Time.realtimeSinceStartup;

        if(currentTime - lastShootTime >= shootRate)
        {
            lastShootTime = currentTime;
            Rigidbody newBullet = Instantiate(bullet, transform.position + transform.rotation * new Vector3(0, 0, 1), new Quaternion()) as Rigidbody;
            newBullet.velocity = transform.forward * bulletSpeed;
        }
    }
}
