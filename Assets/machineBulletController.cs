using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineBulletController : MonoBehaviour {

    public static float damage = 50f;
	public AudioClip shootSound;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 7f);
		AudioSource src = GetComponent<AudioSource>();
		src.clip = shootSound;
		src.time = 0.3f;
		src.Play();
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.tag == "wall") {
			Destroy(this.gameObject);
	    }

	}
}
