using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPosition : MonoBehaviour {

	void Start () {
		GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = this.transform.position;
	}

	public Vector2 SayPosition(){
		return this.transform.position;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player"){
			this.gameObject.layer = 2;
			//DOTween.Kill(other.gameObject.transform);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player"){
			this.gameObject.layer = 0;
			//DOTween.Kill(other.gameObject.transform);
		}
	}
}
