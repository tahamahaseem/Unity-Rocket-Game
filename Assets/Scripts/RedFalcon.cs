using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFalcon : MonoBehaviour
{
    public AudioSource deathSound;
    public SpriteRenderer color;
    GameObject[] myObjects;
    public ParticleSystem death;
    public int health = 10;
    public float speed = 10;
    public float range = 10;
    private Transform target;
    public float offset = 270;
    float angle;
    Vector3 direction;

    bool shooting = false;
    bool escaping = false;
    float change = 0.5f;
    public Transform firePoint;
    public float fireRate = 0.8f;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    // Start is called before the first frame update

    public static RedFalcon instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {//if collision occured


        if (other.gameObject.tag == "Planet" || other.gameObject.tag == "Player" )//if collision with player or planet
        {
            health = 0;//die
        }
         if (other.gameObject.tag == "Mob")//if collision with its own type
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<PolygonCollider2D>(), other.gameObject.GetComponent<PolygonCollider2D>());//ignore collision
        }
        if (other.gameObject.tag == "PlayerBullet" || other.gameObject.tag == "AgressiveBullet")//if collision with any type of bullets
        {
            health--;//deplete health by one
            StartCoroutine(Hurt());//play hurt animation
        }
        if(health <=0){
            Instantiate(death, transform.position, Quaternion.identity);//die
            deathSound.Play(0);//play death sound
        }

    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = Random.Range(speed*0.2f, speed);//pick a random speed of enemy
        range = Random.Range(range*0.5f, range*1.5f);//pick a random range of enemy
        myObjects = GameObject.FindGameObjectsWithTag("Planet");//find objects of planet type
        
    }
    // Update is called once per frame
    void Update(){
        if(health <= 0){//if no health
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<TrailRenderer>().emitting = false;
            Destroy(this.gameObject.transform.GetChild(1).gameObject);
            Destroy(this.gameObject,1f);//die
        }
    }

 
    void FixedUpdate()
    {

        
        for(int i = 0; i < myObjects.Length; i++){//for all mobs of this type in array
            if(Vector2.Distance(myObjects[i].GetComponent<Transform>().position, transform.position) > myObjects[i].GetComponent<gravity>().influenceRange*2/3 && !escaping){//checks if in range of planet
                direction = target.position - transform.position;
                
                if(Vector3.Distance(transform.position,target.position) >= range){//checks if in range of player
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);//moves towards player
                }else{
                     if(!shooting){//shoots player if in range
                        StartCoroutine(Shoot());
                    } 
                }
              
    
            }else{//if in range of planet, run from planet gravitational pull
       
                escaping = true;
                transform.position = Vector2.MoveTowards(transform.position, myObjects[i].GetComponent<Transform>().position * -1, speed * Time.fixedDeltaTime);
                direction = transform.position - myObjects[i].GetComponent<Transform>().position;
                if(Vector2.Distance(myObjects[i].GetComponent<Transform>().position, transform.position) > myObjects[i].GetComponent<gravity>().influenceRange*3f && escaping){
                    escaping = false;
                }
            }
        }
            
            
        
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//calculates angle to look at player
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+offset));//looks at player
    }


     IEnumerator Shoot(){//shoot process
        shooting = true;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 10f);
        yield return new WaitForSeconds(fireRate);
        shooting = false;
      }

        IEnumerator Hurt(){//hurt animation
       color.color = new Color(150f, 0f, 0f, 50f);
       yield return new WaitForSeconds(0.2f);
       color.color = new Color(255f, 255f, 255f, 1f);
      }

}
