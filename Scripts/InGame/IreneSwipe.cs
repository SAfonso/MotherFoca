using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class IreneSwipe : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

	public Hero player;

	Vector2 startPosition, endPosition;

	public enum Direccion {Norte, Sur, Este, Oeste, None};

	Direccion direccion;

	public void OnBeginDrag(PointerEventData data){
		startPosition = data.position;
	}

	public void OnDrag(PointerEventData data){

	}

	public void OnEndDrag(PointerEventData data){
		endPosition = data.position;

		direccion = Direccion.None;

		if(GameManager.instance.gmCanMove){
			GetDirection();
			player.MovePlayer(direccion);
		}
		
	}

	public void GetDirection(){
		
		float distanceX, distanceY;

		distanceX = endPosition.x - startPosition.x;
		distanceY = endPosition.y - startPosition.y;

		Debug.Log("("+ distanceX +", "+ distanceY +")");

		if(Mathf.Abs(distanceX)-Mathf.Abs(distanceY) < 0){
			if(Mathf.Abs(distanceY) > 50f){
				if(distanceY < 0){
					direccion = Direccion.Sur;
				}else
					direccion = Direccion.Norte;
			}else
				direccion = Direccion.None;
		}else{
			if(Mathf.Abs(distanceX) > 50f){
				if(distanceX < 0){
					direccion = Direccion.Oeste;
				}else
					direccion = Direccion.Este;
			}else
				direccion = Direccion.None;
		}
	}

}
