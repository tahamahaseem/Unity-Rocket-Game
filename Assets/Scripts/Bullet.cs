using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem bullestExplosion;
    public GameObject bullet;

    public static Bullet instance = null;
    private void Awake()
    {
        instance = this;
    }

     private void OnCollisionEnter2D(Collision2D other)
    {//checks if collision occured


        if (other.gameObject.tag == "Mob")//if collision is with a mob
        {
            transform.position = other.gameObject.transform.position;
            Instantiate(bullestExplosion, transform.position, Quaternion.identity);//play particles
            Destroy(this.gameObject);//die
        }
        if (other.gameObject.tag == "Planet"){//if collision is with a planet
            Instantiate(bullestExplosion, transform.position, Quaternion.identity);//play particles
            Destroy(this.gameObject);//die

        }

       


    }
}
