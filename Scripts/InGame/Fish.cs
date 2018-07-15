using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

	SpriteRenderer thisSprite;
	BoxCollider2D thisCollider;
	
	// Use this for initialization
	void Start () {
		thisSprite = this.transform.GetComponent<SpriteRenderer>();
		thisCollider = this.transform.GetComponent<BoxCollider2D>();
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Walrus"){
			EatFish();
		}
	}

	public void EatFish(){
		// Add cointID to levelManager
		thisSprite.gameObject.SetActive(false);
		thisCollider.gameObject.SetActive(false);
	}
}
