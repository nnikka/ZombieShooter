using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpController : MonoBehaviour {

    void Start() {
		Destroy(this.gameObject, 10f);
    }

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.tag == "Player") {
			float newHp = PlayerControllerM.hp += 10f;
			if (newHp >= 100f) {
				PlayerControllerM.hp = 100f;
			} else {
			    PlayerControllerM.hp = newHp;
			}
			Destroy(this.gameObject);
	    }

	}
}
