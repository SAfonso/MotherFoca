using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

	public Sprite hole;

	SpriteRenderer thisSprite;
	public bool walkable = true;

	// Use this for initialization
	void Start () {
		thisSprite = this.GetComponent<SpriteRenderer>();
		this.transform.tag = "Breakeable";	
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player"){
			ChangeSprite();
		}
	}

	public void ChangeSprite(){
		thisSprite.sprite = hole;
		this.transform.tag = "Hole";
		walkable = false;
	}

	public bool GetWalkState(){
		return walkable;
	}
	
}
