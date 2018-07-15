using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Particle : MonoBehaviour {

	public float velocity;
	public List<Solver> solverGroup = new List<Solver>();
	public GameObject start;
	Vector2 exit, nextPos;
	int index;
	bool canMove = false;

	SpriteRenderer thisSprite;
	ParticleSystem thisTrail;


	// Use this for initialization
	void Start () {
		exit = start.GetComponent<startPosition>().SayPosition();
		this.transform.position = exit;
		index = 0;
		thisSprite = this.GetComponent<SpriteRenderer>();
		thisTrail = transform.GetChild(0).GetComponent<ParticleSystem>();
		canMove = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove){
			MoveToNext();
			canMove = !canMove;
		}
		
	}

	public void MoveToNext(){
		
		if(index <= (solverGroup.Count-1)){
			nextPos = solverGroup[index].SayPosition();
			index++;
			transform.DOMove(nextPos, velocity).OnComplete(()=>canMove = !canMove);
		}else{
			index = 0;
			thisSprite.enabled = false;
            thisTrail.Pause();
			nextPos = exit;
			transform.DOMove(nextPos, velocity).OnComplete(()=>Restart());
		}
		
	}

	public void Restart(){
		thisSprite.enabled = true;
        thisTrail.Play();
		StartCoroutine("WaitUntiMove");
	}

	IEnumerator WaitUntiMove() {
        yield return new WaitForSeconds(0.2f);
		canMove = !canMove;
    }
}
