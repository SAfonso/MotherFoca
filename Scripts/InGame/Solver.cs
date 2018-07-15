using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver : MonoBehaviour {

	public int index;

	public Vector2 SayPosition(){
		return this.transform.position;
	}

	public int SayIndex(){
		return index;
	}
}
