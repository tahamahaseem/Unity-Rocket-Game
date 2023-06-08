using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour// planet gravitional pull
{

    Transform player, enemy;
    public ParticleSystem destroyed;
    GameObject[] myObjects;
    public ParticleSystem gravityEffect;
    Rigidbody2D playerBody, enemyBody;
    public float influenceRange;
    public float intensity;
    public float distanceToPlayer, distanceToEnemy;
    public Vector2 pullForce, enemyPullForce;
    // Start is called before the first frame update
    
    public static gravity instance = null;
    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {//if collision occured


        if (other.gameObject.tag == "Mob" || other.gameObject.tag == "PlayerBullet" || other.gameObject.tag == "PassiveBullet" || other.gameObject.tag == "AgressiveBullet" )//collision with mobs or pullets
        {
            destroyed.transform.position = other.gameObject.transform.position;
            Instantiate(destroyed, destroyed.transform.position, Quaternion.identity);//plays shot-at particles
        }

    }
    
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myObjects = GameObject.FindGameObjectsWithTag("Mob");
        gravityEffect.Stop();
        
    }


    // Update is called once per frame
    void Update()
    {
    
     if(distanceToPlayer <= influenceRange){
        gravityEffect.Play();//tells player if being pulled in by planet if in range of its influence range
     }else{
        gravityEffect.Stop();
     }
       
        
    }
    void FixedUpdate(){

        distanceToPlayer = Vector2.Distance(player.position, transform.position);//distance from the player to the planet

        if(distanceToPlayer <= influenceRange){//checks if player is in range of planet gravitational pull
            
            pullForce = (transform.position - player.position).normalized / distanceToPlayer * intensity;//calculating pull vector
            playerBody.AddForce(pullForce, ForceMode2D.Force);//pulls in player
            

        }

        //same code as above, but for enemies to be inflected by gravitional pull
        myObjects = GameObject.FindGameObjectsWithTag("Mob");//array holding all enemies

        for(int i = 0; i < myObjects.Length; i++){//goes through array of enemies
        distanceToEnemy = Vector2.Distance(myObjects[i].GetComponent<Transform>().position, transform.position);
        if(distanceToEnemy <= influenceRange){
            enemyPullForce = (transform.position - myObjects[i].GetComponent<Transform>().position).normalized / distanceToEnemy * intensity;
            enemyBody = myObjects[i].GetComponent<Rigidbody2D>();
            enemyBody.AddForce(pullForce, ForceMode2D.Force);
            
        }
        
        }
    }
}
