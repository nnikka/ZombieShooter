using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerM : MonoBehaviour {

  public static PlayerControllerM warriorInstance;
  private Vector3 currPos;
  public float velocity;
  public static float hp = 100;

  private bool clickUp; 
  private bool clickDown;
  private bool clickLeft;
  private bool clickRight;

  public Rigidbody2D bulletPrefab;
  public Transform bulletSpawn;
  public static float bulletVelocity = 4f;
  public static float fireRate = 0.5f;
  private float lastShot = 0f;

  public float offset = 0.0f;

	private void Awake() {
	    if (warriorInstance == null)
	    {
	        warriorInstance = this;
	    }
	}

  void Start () {
     
  }

  void Update () {
	
    Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    difference.Normalize();
    float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
	if (Input.GetKey(KeyCode.W)) clickUp = true;
    else clickUp = false;
    if (Input.GetKey(KeyCode.S)) clickDown = true;
    else clickDown = false;
    if (Input.GetKey(KeyCode.A)) clickLeft = true;
    else clickLeft = false;
    if (Input.GetKey(KeyCode.D)) clickRight = true;
    else clickRight = false;
	if (Input.GetKey(KeyCode.Space)) Fire();
  }

	void FixedUpdate() {
	    if (clickUp &&  hp > 0)
	    {
	        moveUp();
	    }
	    if (clickDown && hp > 0)
	    {
	        moveDown();
	    }
	    if (clickLeft && hp > 0)
	    {
	        moveLeft();
	    }
	    if (clickRight && hp > 0)
	    {
	        moveRight();
	    }
    }

	void moveUp()
    {
        currPos = warriorInstance.transform.position;
        currPos.y += velocity;
        warriorInstance.transform.position = currPos;
    }

    void moveDown()
	{   
        currPos = warriorInstance.transform.position;
        currPos.y -= velocity;
        warriorInstance.transform.position = currPos;
    }

    void moveLeft()
    {
        currPos = warriorInstance.transform.position;
        currPos.x -= velocity;
        warriorInstance.transform.position = currPos;
    }

    void moveRight()
    {
        currPos = warriorInstance.transform.position;
        currPos.x += velocity;
        warriorInstance.transform.position = currPos;
    }

	void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {
			Rigidbody2D bullet = Instantiate(bulletPrefab,
                                         bulletSpawn.position,
				bulletSpawn.rotation) as Rigidbody2D;
             
			bullet.velocity = bullet.transform.right * bulletVelocity;
            lastShot = Time.time;
        }
    }


}