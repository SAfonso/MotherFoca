using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldSwipe : MonoBehaviour {

	public Hero player;
//	public GameObject fish;
	private Vector3 m_vSwipeDirection;

	string myDirection = "";

	private float m_fHeightSensibility 	= 0.1f;
	private float m_fWidthSensibility  	= 0.1f;

//	private float tapsensivility = 0.5f;
//	float touchTime = 0f;
	bool canTouch = true;




	// Update is called once per frame
	void Update () {
/*		if ( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary) {
			touchTime += Time.deltaTime;
			//Debug.Log(touchTime);
		}*/

/*		if ( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && canTouch ) {

				canTouch = false;

				m_vSwipeDirection = Input.GetTouch(0).deltaPosition;
			
				if ((touchTime >= tapsensivility)  ){ //Cambiar por getTouch
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray) != null){
						Vector3 thisPosition = GetNewPos(ray.origin);
						Instantiate(fish, thisPosition, Quaternion.identity);
					}
					canTouch = true;

				}else if (touchTime <= 0.5f){
					CheckAndComputeDirection4Direction();
				}
				touchTime = 0f;
		}
	}*/

	if ( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && canTouch && GameManager.instance.gmCanMove) {

				canTouch = false;

				m_vSwipeDirection = Input.GetTouch(0).deltaPosition;
			
				CheckAndComputeDirection4Direction();

		}
	}

	// Center the player prefab in the tile
/*	public Vector3 GetNewPos(Vector3 oldPos){
		float xDec = oldPos.x;
		float yDec = oldPos.y;

		xDec = Mathf.Repeat(xDec, 1.0f);
		yDec = Mathf.Repeat(yDec, 1.0f);

		float newXpos = oldPos.x - xDec;
		float newYpos = oldPos.y - yDec;

		Vector3 newPos = new Vector3(newXpos+0.5f, newYpos+0.5f, 0);

		return newPos;		
	}*/

	private void CheckAndComputeDirection4Direction() {

		string direction = "";

		if ( m_vSwipeDirection.x > 2f && m_vSwipeDirection.x > m_fWidthSensibility ) {

			if ( m_vSwipeDirection.x > 2f ) {
				if ( m_vSwipeDirection.y >= 10f && m_vSwipeDirection.y > m_fHeightSensibility ) {
					direction = "Abajo";
					
				}
				else if ( m_vSwipeDirection.y <= -10f && m_vSwipeDirection.y < -m_fHeightSensibility ) {
						direction = "Arriba";
				}
				else {
					direction = "Izquierda";
				}
			}
		} else if ( m_vSwipeDirection.x < -2f && m_vSwipeDirection.x < -m_fWidthSensibility ) {

			if ( m_vSwipeDirection.y >= 10f && m_vSwipeDirection.y > m_fHeightSensibility ) {
						direction = "Abajo";
			}
			else if ( m_vSwipeDirection.y <= -10f && m_vSwipeDirection.y < -m_fHeightSensibility ) {
						direction = "Arriba";
			}
			else {
				direction = "Derecha";
			}
		} else if ( m_vSwipeDirection.y >= 2f ) {
			direction = "Abajo";
		} else if ( m_vSwipeDirection.y <= -2f ){
			direction = "Arriba";
		}
		//player.MovePlayer(direction);
		canTouch = true;
	
	}
}
