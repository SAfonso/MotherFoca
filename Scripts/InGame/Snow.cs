using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Snow : MonoBehaviour {

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
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
