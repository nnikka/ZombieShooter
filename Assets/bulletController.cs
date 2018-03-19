using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour {

     Rigidbody rgb;
     float speed;
     public static float damage = 50f;
     public AudioClip shootSound;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(shootSound, this.transform.position);
		Destroy(this.gameObject, 7f);
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
