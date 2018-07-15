using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement; 
using UnityEngine.Tilemaps; 
 
public class HeroAux : MonoBehaviour 
{ 
  public float m_MoveSpeed; 
 
 
  Rigidbody2D rb; 
 
    float dashCooldown = 0f; 
 
  void Start() { 
        rb = GetComponent<Rigidbody2D>(); 
    } 
  void Update ()  
  {
    if(Input.GetKey(KeyCode.A)){
      Debug.Log("Position >>> " + this.transform.position);
    } 
    Vector3 tryMove = Vector3.zero; 
     
    if (Input.GetKey(KeyCode.LeftArrow)) 
      tryMove += Vector3Int.left; 
    if (Input.GetKey(KeyCode.RightArrow)) 
      tryMove += Vector3Int.right; 
    if (Input.GetKey(KeyCode.UpArrow)) 
      tryMove += Vector3Int.up; 
    if (Input.GetKey(KeyCode.DownArrow)) 
      tryMove += Vector3Int.down; 
 
        rb.velocity = Vector3.ClampMagnitude(tryMove, 1f) * m_MoveSpeed; 
 
        dashCooldown = Mathf.MoveTowards(dashCooldown, 0f, Time.deltaTime); 
 
  } 
 
 
} 
