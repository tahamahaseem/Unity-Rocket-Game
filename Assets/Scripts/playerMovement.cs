using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
public ParticleSystem boost;
public ParticleSystem destroyedEnemy;
Vector2 mousePos;
public float moveSpeed = 0.1f;
public float offset = 270;
Rigidbody2D rb;
public Vector3 position = new Vector3(0f,0f,1f);
Vector3 inertia = new Vector3(0f,0f,1f);
Vector3 oldInputLocation = new Vector3(0f,0f,1f);
Vector3 oldLocation = new Vector3(0f,0f,1f);
float angle;
Vector3 direction;
bool pressed, pressedShoot;

public static playerMovement instance = null;
    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {//if collision occured


        if (other.gameObject.tag == "Mob")//if collides with an enemy
        {
            destroyedEnemy.transform.position = other.gameObject.transform.position;//
            Instantiate(destroyedEnemy, destroyedEnemy.transform.position, Quaternion.identity);//play debris particles
        }

        if ((other.gameObject.tag == "PassiveBullet")){//if collides with a bullet that does not hurt the player
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<PolygonCollider2D>(), other.gameObject.GetComponent<PolygonCollider2D>());//ignore the bullet

        }

    }
 
 void Start()
 {
    rb = GetComponent<Rigidbody2D>();
    pressed = false;
    pressedShoot = false;
    boost.Stop();
 }
 
 void Update ()
 {
    if(Input.GetMouseButtonDown(1)){//if right mouse button clicked
        pressed = true;
        boost.Play();//play rocket boosters
    }else if (Input.GetMouseButtonUp(1)){//if right mouse button released
        pressed = false;
        boost.Stop();//stop playing rocket boosters
    }

    mousePos= Input.mousePosition;
    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

    if(pressed){//checks if mouse is held
    position = Vector3.Lerp(transform.position, new Vector3(mousePos.x,mousePos.y,transform.position.z), moveSpeed);//moves slowly to mouse position
    oldInputLocation =  new Vector3(mousePos.x,mousePos.y,0);//holds mouse position before mouse was let go
    oldLocation = transform.position;//holds players position before mouse was let go
    inertia = oldInputLocation - oldLocation;
    }
    else{//if mouse is let go
        position = transform.position + (inertia)*moveSpeed;//moves in the direction and speed when rocket stopped boosting
    }
    
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;//direction where player should look (towards mouse)
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//calculate angle
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+offset));//changes player angle to look towards the mouse cursor
 
 }


 private void FixedUpdate(){

    
    
    rb.MovePosition(position);//move player
  
   
  

 }

}
