using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cycleAnimtionController : MonoBehaviour {

	public Sprite[] sprites;
	private float dealy = 0.1f;
	private int current = 0;

	void Start() {
		Debug.Log(current);
		StartCoroutine("animateObject");
	}

	IEnumerator animateObject() {
	    while (true) {
	         Debug.Log(current);
			this.GetComponent<SpriteRenderer>().sprite = sprites[current];
	        yield return new WaitForSeconds(dealy);
	        if (current == sprites.Length - 1) {
				current = 0;
	        } else {   
				current++;
	        }
	    }
	}
}
