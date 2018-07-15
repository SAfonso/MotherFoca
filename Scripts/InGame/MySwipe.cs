using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MySwipe : MonoBehaviour {

	public Hero player;
    private bool tap, swipeleft, swiperight, swipeup, swipedown;
    private bool isDraging = false;
    private Vector2 starttouch, endpos, swipedelta = Vector2.zero;
    [SerializeField] [Range(10f, 125f)] private float swipelength = 75f;

    private void Update() {
        tap = swipeleft = swiperight = swipeup = swipedown = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            starttouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0) {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                starttouch = Input.touches[0].position;
				Debug.Log("Hola empieza" + starttouch);
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
				endpos = Input.touches[0].position;
                Debug.Log("Hola termina" + endpos);
				isDraging = false;
				CalCulateDistance();
            }
        }
        #endregion

        //Calculate the distance





    }


	public void CalCulateDistance(){
		swipedelta = Vector2.zero;
		Debug.Log("Queremos Dibujar " + isDraging);
		Debug.Log("startpos " + starttouch);
        
			Debug.Log("Dibujamos");
/*            if (Input.touches.Length > 0)
            {*/
                swipedelta = endpos - starttouch;
				Debug.Log("Swi`pDelta >> " + swipedelta);
				Debug.Log("Swi`pDelta Mag >> " + swipedelta.magnitude);
/*            }
            else if (Input.GetMouseButton(0)) {
                swipedelta = (Vector2)Input.mousePosition - starttouch;
            }*/

            //Did we cross the deadzone?
            if (swipedelta.magnitude > swipelength) {
                //which direction?
				string direction = "";
				Debug.Log("Estamos aquí");

                float x = swipedelta.x;
                float y = swipedelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //left or riht
                    if (x < 0) { direction = "Izquierda"; } else { direction = "Derecha"; }
                }
                else {
                    //up or down
                    if (y < 0) { direction = "Abajo"; } else { direction = "Arriba"; }
                }
				Debug.Log ("Movemos a " + direction);
				//player.MovePlayer(direction);
                Reset();
            }
	}
    private void Reset() {
        starttouch = swipedelta = Vector2.zero;
        isDraging = false;
    }

    public Vector2 SwipeDelta { get { return swipedelta; } }
    public bool SwipeLeft { get { return swipeleft; } }
    public bool SwipeRight { get { return swiperight; } }
    public bool SwipeUp { get { return swipeup; } }
    public bool SwipeDown { get { return swipedown; } }




}


