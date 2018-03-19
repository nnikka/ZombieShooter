using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public GameObject hpAdd;
    public float speedZ;
    private Transform target;
    private bool OnDoor = false;
    public Transform door;
    private float currentHp;
    public int zombieMoney;
    public int zombieScore;

    private float lastAttack;
    private float attackRate = 1.5f;
    public AudioClip death;
    public GameObject bloody;

	// Use this for initialization
	void Start () {
		lastAttack = Time.time;
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		currentHp = GameControllerM.zombieHp;
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentHp <= 0) {
			AudioSource.PlayClipAtPoint(death, this.transform.position);
			GameControllerM.playerMoney += zombieMoney;
			GameControllerM.playerScore += zombieScore;
			Instantiate(bloody, this.gameObject.transform.position, this.gameObject.transform.rotation);
			if (Random.Range(0, 9) == 7) {
				Instantiate(hpAdd, this.gameObject.transform.position, this.gameObject.transform.rotation);
			}
	        Destroy(this.gameObject);
	    } 
		if (OnDoor) {
		    chase();
		} else {
			MoveToDoor();
		}
	}

	public void MoveToDoor() {
		Vector3 difference = door.position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 0f);

		if (Vector2.Distance(transform.position, door.position) > 0) {
			transform.position = Vector2.MoveTowards(transform.position, door.position, speedZ * Time.deltaTime);
		} else {
			OnDoor = true;
		}
	}

	private void chase() {
		Vector3 difference = target.position - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + 0f);

		if (Vector2.Distance(transform.position, target.position) > 0.5f) {
			transform.position = Vector2.MoveTowards(transform.position, target.position, speedZ * Time.deltaTime);
		} else {
			zombieAttack();
		}
	}

	private void zombieAttack() {
	    if (Time.time > lastAttack + attackRate) {
			PlayerControllerM.hp -= 5f;
			lastAttack = Time.time;
			GetComponent<AudioSource>().Play();
	    }
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.tag == "bullet") {
			currentHp -= bulletController.damage;
			Destroy(collision.gameObject);
	    }

		if (collision.tag == "machineBullet") {
			currentHp -= machineBulletController.damage;
			Destroy(collision.gameObject);
	    }

	}
}
