using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineController : MonoBehaviour {

	public Rigidbody2D machineBullet;
    private Transform spawnBullet;
	public static float bulletVelocity = 6f;
    public static float fireRate = 0.3f;
    private float lastShot = 0f;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject.transform.parent.gameObject, 30f);
		spawnBullet = this.gameObject.transform.GetChild(0).gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void shoot() {
		if (Time.time > fireRate + lastShot)
        {
			Rigidbody2D bullet = Instantiate(machineBullet,
				spawnBullet.position,
				spawnBullet.rotation) as Rigidbody2D;
             
			bullet.velocity = bullet.transform.right * bulletVelocity;
            lastShot = Time.time;
        }
	}

	private void OnTriggerStay2D(Collider2D collision) {

		if (collision.tag == "zombie" || collision.tag == "bossZombie") {
			shoot();
	    }

	}
}
