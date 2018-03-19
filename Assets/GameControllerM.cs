using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerM : MonoBehaviour {

    public GameObject PauseUI;
    private bool paused = false;

	public GameObject bossPRF;
	private int bossMoney = 100;
	private int bossScore = 100;
	public static float bossHp = 1000;

    public GameObject zombiePRF;
    public Transform[] zombieSpawnsLeft;
    public Transform leftDoor;
	public Transform[] zombieSpawnsRight;
    public Transform rightDoor;
	public Transform[] zombieSpawnsTop;
    public Transform toptDoor;
	public Transform[] zombieSpawnsBottom;
    public Transform bottomDoor;

    private int zombieCount;
	private float spawnTime = 1.5f;
	private float delayBetweenWaves = 10f;
	private int zombieMoney = 5;
	private int zombieScore = 30;
    public static float zombieHp = 100f;

    private float startWave;

    public static int playerScore = 0;
    public static int playerMoney = 0;
    public GameObject playerHpBar;
	public GameObject playerScoreBar;
	public GameObject playerMoneyBar;
	public GameObject machinebulletPrefab;

	private bool enableGlock = true;
	private bool enableM4 = true;
	private bool enableGutling = true;
	private bool enableMachine = true;
	public Button buyGlockB;
	public Button buyMachineB;
	public Button buyM4B;
	public Button buyGutlingB;
	public Image currentGun;
	public Sprite m4sprt;
	public Sprite glocksprt;
	public Sprite gutlingsprt;

	private int numberOfMachines = 0;
	public Text machineNumberText;
	private int waveNumber = 0;
	private bool bossCreated = false; 

	// Use this for initialization
	void Start () {
		zombieCount = 10;
		StartCoroutine("CreateWave");
		PauseUI.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		handleMachineSetup();
		handleGunsDisable();
		handePauseUI();
		handleBars();
		if (PlayerControllerM.hp <= 0) {
			SceneManager.LoadScene("_EndGame");
		}
	}

	private void makeBoss() {
		bossCreated = true;
		int poInt = Random.Range(0, 4);
		if (poInt == 1) {
			createBoss(zombieSpawnsLeft, leftDoor);
		} else if (poInt == 2) {
			createBoss(zombieSpawnsRight, rightDoor);
		} else if (poInt == 3) {
			createBoss(zombieSpawnsTop, toptDoor);
		} else {
			createBoss(zombieSpawnsBottom, bottomDoor);
		}
	}

	private void createBoss(Transform[] spawns, Transform door) {
		int randomInt = Random.Range(0, spawns.Length);
		float zombieSpeed = Random.Range(0.5f, 1.7f);
		GameObject zombie = Instantiate(bossPRF, spawns[randomInt].position, spawns[randomInt].rotation);
		zombie.GetComponent<bossController>().door = door;
		zombie.GetComponent<bossController>().speedZ = zombieSpeed;
		zombie.GetComponent<bossController>().zombieMoney = bossMoney;
		zombie.GetComponent<bossController>().zombieScore = bossScore;
	}

	private void makeZombie() {
		int poInt = Random.Range(0, 4);
		if (poInt == 1) {
			createZombie(zombieSpawnsLeft, leftDoor);
		} else if (poInt == 2) {
			createZombie(zombieSpawnsRight, rightDoor);
		} else if (poInt == 3) {
			createZombie(zombieSpawnsTop, toptDoor);
		} else {
			createZombie(zombieSpawnsBottom, bottomDoor);
		}
	}

	private void createZombie(Transform[] spawns, Transform door) {
		int randomInt = Random.Range(0, spawns.Length);
		float zombieSpeed = Random.Range(0.5f, 1.7f);
		GameObject zombie = Instantiate(zombiePRF, spawns[randomInt].position, spawns[randomInt].rotation);
		zombie.GetComponent<ZombieController>().door = door;
		zombie.GetComponent<ZombieController>().speedZ = zombieSpeed;
		zombie.GetComponent<ZombieController>().zombieMoney = zombieMoney;
		zombie.GetComponent<ZombieController>().zombieScore = zombieScore;
	}

	IEnumerator CreateWave() {
	    while (true) {
	         startWave = Time.time;
			 for (int i = 0; i < zombieCount; i++) {
				makeZombie();
				if (waveNumber % 3 == 0 && !bossCreated) {
				    makeBoss();
				}
				yield return new WaitForSeconds(spawnTime);
	         }
			 yield return new WaitForSeconds(delayBetweenWaves);
			 upgradeZombies();
			 waveNumber++;
			 bossCreated = false;
	    }
	}

	private void upgradeZombies() {
		zombieCount += 4;
		zombieHp += 30f;
		bossHp += 300;
		if (spawnTime >= 0.2f)
        {
            spawnTime -= 0.2f;
        }
		delayBetweenWaves += 0.2f;
		zombieMoney += 2;
		zombieScore += 4;
	}

	private void handleBars() {
		playerHpBar.GetComponent<Image>().fillAmount = PlayerControllerM.hp / 100f;
		playerScoreBar.GetComponent<Text>().text = "Score: " + playerScore;
		playerMoneyBar.GetComponent<Text>().text = "Score: " + playerMoney + " $";
		machineNumberText.text = "" + numberOfMachines;
	}

	void handePauseUI() {
	  if (Input.GetKeyDown(KeyCode.P)) {
          paused = !paused;
      }
	  if (paused) {
		  PauseUI.SetActive(true);
		  Time.timeScale = 0;
      }
	  if (!paused) {
		  PauseUI.SetActive(false);
		  Time.timeScale = 1;
      }
    }

    public void Resume() {
		paused = false;
    }

    public void Restart() {
		PlayerControllerM.hp = 100f;
		playerMoney = 0;
		playerScore = 0;
		zombieHp = 100f;
		bossHp = 1000f;
		bulletController.damage = 50f;
		Application.LoadLevel(Application.loadedLevel);
    }

    public void Quit() {
		PlayerControllerM.hp = 100f;
		playerMoney = 0;
		playerScore = 0;
		zombieHp = 100f;
		bossHp = 1000f;
		bulletController.damage = 50f;
		SceneManager.LoadScene("_StartMenu");
    }

    public void buyGlock() {
       if (playerMoney > 50 && playerScore > 200) {
          playerMoney -= 50;
          PlayerControllerM.bulletVelocity = 5f;
		  PlayerControllerM.fireRate = 0.4f;
			enableGlock = false;
			enableGutling = true;
			enableM4 = true;
			currentGun.sprite = glocksprt;
			bulletController.damage = 70f;
       }
    }

	public void buyM4() {
       if (playerMoney > 200 && playerScore > 500) {
		  playerMoney -= 200;
          PlayerControllerM.bulletVelocity = 6f;
		  PlayerControllerM.fireRate = 0.2f;
			enableM4 = false;
			enableGutling = true;
			enableGlock = true;
			currentGun.sprite = m4sprt;
			bulletController.damage = 100f;
       }
    }

	public void buyGutling() {
       if (playerMoney > 500 && playerScore > 1000) {
          playerMoney -= 500;
          PlayerControllerM.bulletVelocity = 7f;
		  PlayerControllerM.fireRate = 0.1f;
			enableGutling = false;
			enableM4 = true;
			enableGlock = true;
			currentGun.sprite = gutlingsprt;
			bulletController.damage = 150f;
       }
    }

    public void buyMachine() {
		if (playerMoney > 300 && playerScore > 3000) {
          playerMoney -= 300;
          numberOfMachines++;
		  machineNumberText.text = "" + numberOfMachines;
       }
    }

    private void handleGunsDisable() {
		if (playerMoney > 50 && playerScore > 200 && enableGlock) {
			buyGlockB.interactable = true;
       } else {
			buyGlockB.interactable = false;
       }
		if (playerMoney > 200 && playerScore > 500 && enableM4) {
			buyM4B.interactable = true;
       } else {
			buyM4B.interactable = false;
       }
		if (playerMoney > 500 && playerScore > 1000 && enableGutling) {
			buyGutlingB.interactable = true;
       } else {
			buyGutlingB.interactable = false;
       }
		if (playerMoney > 300 && playerScore > 3000) {
		 buyMachineB.interactable = true;
       } else {
			buyMachineB.interactable = false;
       }
    }

    private void handleMachineSetup() {
		if (Input.GetKeyDown(KeyCode.G)) {
		   if (numberOfMachines > 0) {
				numberOfMachines--;
				GameObject machine = Instantiate(machinebulletPrefab, PlayerControllerM.warriorInstance.bulletSpawn.position,
				 PlayerControllerM.warriorInstance.bulletSpawn.rotation);
				machine.transform.position = new Vector3(machine.transform.position.x, machine.transform.position.y, -15f);
		   }
		}
    }
}
