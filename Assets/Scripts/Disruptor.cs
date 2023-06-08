using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disruptor : MonoBehaviour
{
    public AudioSource deathSound;
    public SpriteRenderer color;
    public GameObject home;
    public ParticleSystem death;
    public int health = 5;
    GameObject[] myObjects;
    public float speed = 10;
    public float range = 10;
    private Transform target;
    public float offset = 270;
    float angle;
    Vector3 direction;
    
    bool shooting = false;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public GameObject bulletPrefab;
    public float bulletForce = 50f;

    public static Disruptor instance = null;

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
            health--;//subtract health by one
            StartCoroutine(Hurt());//play hurt animation
        }
         if (other.gameObject.tag == "PassiveBullet")//if collision own bullet type
        {
           health = 0;//die
        }

        if(health <=0){
            Instantiate(death, transform.position, Quaternion.identity);//die
            deathSound.Play(0);//play death sound
        }

    }


    void Start()
    {
        target = home.GetComponent<Transform>();
        speed = Random.Range(speed*0.5f, speed);//pick a random speed of enemy
        range = Random.Range(range, range+5);//pick a random range of enemy
        myObjects = GameObject.FindGameObjectsWithTag("Planet");//find objects of planet type
        
    }

    void Update(){
        if(health <= 0){//if no health
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<TrailRenderer>().emitting = false;
            Destroy(this.gameObject.transform.GetChild(1).gameObject);
            Destroy(this.gameObject,1f);//die
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position,target.position) >= range){// checks if in range of planet
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        }else{
            if(!shooting){//shoots if close to planet
                StartCoroutine(Shoot());
                 Debug.Log("Start");
            }
        }
        direction = target.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle+offset));
        
        
    }

    IEnumerator Shoot(){//shoot process
        Debug.Log("Shooting");
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
