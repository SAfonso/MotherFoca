using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teleport : MonoBehaviour {

	public Teleport m_Target;
    public Hero player;
    Animator myAnim;

    public Rigidbody2D justArrived = null;
    IreneSwipe.Direccion nextDirection = IreneSwipe.Direccion.None;
    Vector2 teleporPosition;
    Vector2 objVelocity;
    Rigidbody2D objectRecived, auxRB = null;
    bool canTeleport = false;

/*void OnTriggerEnter2D(Collider2D other) {
    Rigidbody2D rb = other.attachedRigidbody;
    objVelocity = rb.velocity;
    other.attachedRigidbody.velocity = new Vector2(0f,0f);

    if (rb == justArrived) {
        justArrived = null;
        Debug.Log(justArrived);
        Debug.Log(this.transform.name);
        canTeleport = false;
        other.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        return;
    }
    
    TeleportObject(other.attachedRigidbody);

}

	void OnTriggerExit2D(Collider2D other)
	{
		justArrived = null;
        Debug.Log("Vamos a salir de >>> " + this.transform.position);
        Debug.Log("JustArrived >> " + justArrived);
        Debug.Log("Ha salido de " + this.transform.name);
        Debug.Log("Posicion del objeto <<<< " + other.transform.position);
   
	}

    void TeleportObject(Rigidbody2D obj) {
        player.CleanTarget();
        m_Target.ReceiveObject(obj, objVelocity);
    }

    void ReceiveObject(Rigidbody2D obj, Vector2 velocity) {
        teleporPosition = new Vector2(transform.position.x, transform.position.y);
        objectRecived = obj;
        canTeleport = true;

        Debug.Log("Posicion delobjeto >>> "  + obj.transform.position);
        Debug.Log("Posicion del teleport >>> "  + transform.position);
                DOTween.Kill(objectRecived.gameObject.transform);

        justArrived = obj;
        objectRecived.velocity = velocity;
        objectRecived.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        objectRecived.gameObject.transform.position = this.transform.position;
        
        Debug.Log("Posicion nueva " +  objectRecived.gameObject.transform.position);

        
    }

    void FixedUpdate() {
        if (canTeleport){
            objectRecived.MovePosition(teleporPosition);  
        }
        
    }*/

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        player = GameObject.FindObjectOfType<Hero>();
        myAnim = player.GetComponent<Animator>();
    }




   void OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D rb = other.attachedRigidbody;
        auxRB = rb;
        rb.velocity = new Vector2(0,0);
/*        Vector3 direction = new Vector3(0,0,0);
        direction.x = other.transform.position.x - transform.position.x;
        direction.y = other.transform.position.y - transform.position.y;
        Debug.Log(direction);*/
        if (this.nextDirection == IreneSwipe.Direccion.None){
            if(other.transform.position.x < transform.position.x){
                Debug.Log("Entra por izq");
                Debug.Log(other.transform.position.x);
                nextDirection = IreneSwipe.Direccion.Oeste;
            }else if(other.transform.position.x > transform.position.x){
                Debug.Log("Entra por dcha");
                Debug.Log(other.transform.position.x);
                nextDirection = IreneSwipe.Direccion.Este;
            }else if(other.transform.position.y < transform.position.y){
                Debug.Log("Entra por abajo");
                Debug.Log(other.transform.position.y);
                nextDirection = IreneSwipe.Direccion.Sur;
            }else if(other.transform.position.y > transform.position.y){
                Debug.Log("Entra por arriba");
                Debug.Log(other.transform.position.y);
                nextDirection = IreneSwipe.Direccion.Norte;
            }
            myAnim.SetBool("SplashIn", true);

        }

        
        if (rb == justArrived) {
            justArrived = null;
            Debug.Log(justArrived);
            Debug.Log(this.transform.name);
            canTeleport = false;

            

            //StartCoroutine("WaitAfterTeleport");

            switch (this.nextDirection){
                case IreneSwipe.Direccion.Norte: 
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-1f, 0); 
                    this.nextDirection = IreneSwipe.Direccion.Sur;
                    break;
                case IreneSwipe.Direccion.Sur: 
                    player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+1f, 0); 
                    this.nextDirection = IreneSwipe.Direccion.Norte;
                    break;
                case IreneSwipe.Direccion.Este: 
                    player.transform.position = new Vector3(player.transform.position.x-1f, player.transform.position.y, 0); 
                    this.nextDirection = IreneSwipe.Direccion.Oeste;
                    break;
                case IreneSwipe.Direccion.Oeste: 
                    player.transform.position = new Vector3(player.transform.position.x+1f, player.transform.position.y, 0); 
                    this.nextDirection = IreneSwipe.Direccion.Este;
                    break;
            }
            
            player.MovePlayer(this.nextDirection);
            Debug.Log("Direction >>> " + this.nextDirection);
            this.nextDirection = IreneSwipe.Direccion.None;
            
            return;
        }
        StartCoroutine("WaitUntilTeleport");
       
    }


	IEnumerator WaitUntilTeleport() {
        yield return new WaitForSeconds(1f);
        myAnim.SetBool("SplashIn", false);
        TeleportObject(auxRB);
    }
	IEnumerator WaitAfterTeleport() {
        yield return new WaitForSeconds(0.5f);
    }


	void OnTriggerExit2D(Collider2D other)
	{
        
		justArrived = null;
        Debug.Log("Vamos a salir de >>> " + this.transform.position);
        Debug.Log(justArrived);
        Debug.Log(this.transform.name);
        this.nextDirection = IreneSwipe.Direccion.None;
        Debug.Log("Ha salido de " + this.transform.name);
   
	}

    void TeleportObject(Rigidbody2D obj) {
        player.CleanTarget();
        m_Target.ReceiveObject(obj, nextDirection);
    }

    void ReceiveObject(Rigidbody2D obj, IreneSwipe.Direccion direction) {
        teleporPosition = new Vector3(transform.position.x, transform.position.y, 0);
        objectRecived = obj;
        DOTween.Kill(objectRecived.gameObject.transform);
        canTeleport = true;
        this.nextDirection = direction;

        objectRecived.gameObject.transform.position = this.transform.position;
        Debug.Log("Posicion delobjeto >>> "  + obj.transform.position);
        Debug.Log("Posicion del teleport >>> "  + transform.position);
        justArrived = obj;
        
    }

/*    void FixedUpdate() {
        if (canTeleport){
            //objectRecived.velocity = new Vector2(0,0);
            objectRecived.MovePosition(teleporPosition);
  
        }        
       
    }*/

}
