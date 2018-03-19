using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCOntroller : MonoBehaviour {


    public GameObject ScoreBar;

	void Start () {
		ScoreBar.GetComponent<Text>().text = "Final score: " + GameControllerM.playerScore;
		PlayerControllerM.hp = 100f;
		GameControllerM.playerMoney = 0;
		GameControllerM.playerScore = 0;
		GameControllerM.zombieHp = 100f;
		GameControllerM.bossHp = 1000f;
		bulletController.damage = 50f;
	}
}
