using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    public float health = 20;
    public AudioSource deathSound;
    public ParticleSystem death;
    public ParticleSystem hurt;
    public SpriteRenderer color;
    public static playerHealth instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {//if collision occured



        if (other.gameObject.tag == "AgressiveBullet")//if hit by enemy bullet
        {
            health = health - 2;//take away two health
            StartCoroutine(Hurt());//play hurt animation
        }
        if (other.gameObject.tag == "Mob")//if hit by enemy
        {
            health = health - 1;//take away one health
            StartCoroutine(Hurt());//play hurt animation
        }
        if (other.gameObject.tag == "Planet")//if hit by a planet
        {
            health = 0;//die instantly
        }
        if(health <=0){
            Instantiate(death, transform.position, Quaternion.identity);// play death particles
            deathSound.Play(0);//play death sound
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){//death process
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<PlayerShooting>().enabled = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
            Destroy(this.gameObject,1f);//die
        }
        
    }

    IEnumerator Hurt(){//hurt animation
       color.color = new Color(150f, 0f, 0f, 50f);
       yield return new WaitForSeconds(0.2f);
       color.color = new Color(255f, 255f, 255f, 1f);
      }
}
