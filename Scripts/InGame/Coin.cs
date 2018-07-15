using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int cointID;
	SpriteRenderer thisSprite;
	BoxCollider2D thisCollider;

	public bool wasCatched;
	
	// Use this for initialization
	void Start () {
		thisSprite = this.transform.GetComponent<SpriteRenderer>();
		thisCollider = this.transform.GetComponent<BoxCollider2D>();
	}

	public void SetId(int id){
		cointID = id;
	}

	public Vector2 SayPosition(){
		return this.transform.position;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player"){
			GetCoin();
		}
	}

	public void GetCoin(){
		// Add cointID to levelManager
		GameManager.instance.actualLevel.GotCha(this.cointID);
		// Summ Points
		thisSprite.gameObject.SetActive(false);
		thisCollider.gameObject.SetActive(false);
	}
	
}
