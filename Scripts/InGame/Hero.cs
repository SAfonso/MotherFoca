
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class Hero : MonoBehaviour
{

	public LayerMask raycastLayerMask;
	public float m_MoveSpeed = 1f;
	public float distanceError = 0.5f;

	Vector3 tryMove = Vector3.zero;
	Rigidbody2D rb;

	int layerMask = 1;

	public Transform target;

	bool isMoving;

    float dashCooldown = 0f;

	string myDirection = "";

	Animator myAnim;

	void Start() {
        rb = GetComponent<Rigidbody2D>();
		myAnim = this.gameObject.GetComponent<Animator>();
		GameManager.instance.gmCanMove = true;
    }

	public void MovePlayer(IreneSwipe.Direccion direction){
		float distance = 0f;
		Debug.Log("Entramos");
		Debug.Log(direction);
		/*if(rb.velocity == Vector2.zero){*/
			if (direction == IreneSwipe.Direccion.Este){
				distance = GetDistance(this.transform, Vector2Int.left);
				if (distance != 0){
					myAnim.SetBool("MoveR", true);
					GameManager.instance.gmCanMove = false;
					this.transform.DOMoveX((this.transform.position.x+distance), distance/m_MoveSpeed).OnComplete(()=>FinishMove("MoveR")).SetEase(Ease.Linear);
					myDirection = "Izquierda";
				}
//				Debug.Log(this.transform.position);
			}
			else if (direction == IreneSwipe.Direccion.Oeste){
				distance = GetDistance(this.transform, Vector2Int.right);
				if (distance != 0){
					myAnim.SetBool("MoveL", true);
					GameManager.instance.gmCanMove = false;
					this.transform.DOMoveX((this.transform.position.x-distance), distance/m_MoveSpeed).OnComplete(()=>FinishMove("MoveL")).SetEase(Ease.Linear);
					myDirection = "Derecha";
				}
//				Debug.Log(this.transform.position);
			}
			else if (direction == IreneSwipe.Direccion.Sur){
				distance = GetDistance(this.transform, Vector2Int.up);
				if (distance != 0){
					myAnim.SetBool("MoveD", true);
					GameManager.instance.gmCanMove = false;
					this.transform.DOMoveY((this.transform.position.y-distance), distance/m_MoveSpeed).OnComplete(()=>FinishMove("MoveD")).SetEase(Ease.Linear);
					myDirection = "Arriba";
				}

//				Debug.Log(this.transform.position);
			}
			else if (direction == IreneSwipe.Direccion.Norte){
				distance = GetDistance(this.transform, Vector2Int.down);
				if (distance != 0){
					myAnim.SetBool("MoveU", true);
					GameManager.instance.gmCanMove = false;
					this.transform.DOMoveY((this.transform.position.y+distance), distance/m_MoveSpeed).OnComplete(()=>FinishMove("MoveU")).SetEase(Ease.Linear);
					myDirection = "Abajo";
				}
//				Debug.Log(this.transform.position);
/*			}*/

			//rb.velocity = Vector3.ClampMagnitude(tryMove, 1f) * m_MoveSpeed;
			//dashCooldown = Mathf.MoveTowards(dashCooldown, 0f, Time.deltaTime);
		}
	}

	public void FinishMove(string value){
		GameManager.instance.gmCanMove = true;
		myAnim.SetBool(value, false);
	}

	public float GetDistance(Transform origin, Vector2Int direction){
		float value = 0f;



		RaycastHit2D hit = Physics2D.Raycast(origin.position, direction,raycastLayerMask);
//		Debug.Log(hit.distance);

		if (hit.collider != null)
        {
			if(hit.transform.tag == "Rock"){
				//Debug.Log("Roca");
				if (hit.distance > (distanceError*2)){
					Debug.Log("Roca");
					value = hit.distance - distanceError;
					
				}
				this.target = hit.collider.transform; // Draw the gizmos
			}else if((hit.transform.tag == "Teleport") || (hit.transform.tag == "Hole")){
				Debug.Log("Parada");
				value = hit.distance;
				this.target = hit.collider.transform; // Draw the gizmos
			}else if ((hit.transform.tag == "Breakeable") || (hit.transform.tag == "Fish") || (hit.transform.tag == "Coin")){
/*				if (hit.distance > (distanceError*2)){
					value = hit.distance - distanceError;
					this.target = hit.transform; // Draw the gizmos
				}*/
				//Debug.Log("Pasable");
				Debug.Log("Estáentrando");
				value = hit.distance - distanceError;
				//Debug.Log(hit.collider.name);

			}else if((hit.transform.tag == "Exit") || (hit.transform.tag == "Start")){
				//Debug.Log("Roca");
				if (hit.distance > (distanceError)){
					value = hit.distance+0.5f;
					
				}
			}else if(hit.transform.tag == "Snow"){
				value = hit.distance+0.5f;
				this.target = hit.collider.transform;
			}

        }

		return value;
	}

	void OnDrawGizmos() {
        if (this.target != null) {
			//Debug.Log(this.target + " <<<< posicion");
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, this.target.position);
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Hole")){
			Debug.Log("TasMuerto");
			DOTween.Kill(this.transform);
			switch(myDirection){
				case "Arriba":
					this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-0.8f,0);
					break;
				case "Abajo":
					this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+0.8f,0);
					break;
				case "Izquierda":
					this.transform.position = new Vector3(this.transform.position.x+0.7f, this.transform.position.y,0);
					break;
				case "Derecha":
					this.transform.position = new Vector3(this.transform.position.x-0.7f, this.transform.position.y+1,0);
					break;
			}
			
		}

		if(other.gameObject.CompareTag("Exit")){
			GameManager.instance.gmCanMove = false;
			myAnim.SetBool("Victory", true);
			GameManager.instance.actualLevel.beforeWasPlayed = true;
			StartCoroutine("WhaitToWin");
		}
		if(other.gameObject.CompareTag("Hole")){
			myAnim.SetBool("Fall", true);
			StartCoroutine("WhaitToReload");

		}
	}

	IEnumerator WhaitToWin() {
        yield return new WaitForSeconds(1.5f);
		GameManager.instance.FinishLevel();
		Loader.instance.LoadWin();
    }
	IEnumerator WhaitToReload() {
        yield return new WaitForSeconds(1f);
		Loader.instance.LoadLevel();
    }


	public void CleanTarget(){
		this.target = null;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
/*		Debug.Log(this.transform.position);
		Debug.Log(DOTween.IsTweening(this.transform));*/
	}
	
}
