using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGameManager : MonoBehaviour {

	public static IGameManager instance;
    DataLevel thisLevel = new DataLevel();
	public GameObject player;

	GameObject level, levelContainer, startPosition, solver;
	public Coin[] estrellas;



	void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {

		levelContainer = GameObject.FindGameObjectWithTag("LevelContainer");
				
		thisLevel = GameManager.instance.GetValues();

		level = GameManager.instance.GetLevel();

		Instantiate(level, levelContainer.transform);

		estrellas = new Coin[3];

		estrellas = GameObject.FindObjectsOfType<Coin>();
		for(int i=0; i<estrellas.Length; i++){
			Coin coin = estrellas[i];
			coin.SetId(i);
			coin.wasCatched = false;
			GameManager.instance.actualLevel.SetStarPos(i);
		}

		solver = GameObject.FindObjectOfType<Particle>().gameObject;
		if (!GameManager.instance.activeCheats){
			solver.SetActive(false);
		}else{
			solver.SetActive(true);
			GameManager.instance.gmPowerUps--;
		}
		
	}

	public void UsePoweUp(){
		if(GameManager.instance.gmPowerUps != 0){
			GameManager.instance.gmPowerUps--;
		}
	}

}
