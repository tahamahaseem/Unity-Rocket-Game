using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistruptorBullet : MonoBehaviour
{
    public ParticleSystem bullestExplosion;
    public AudioSource audioData;

    public static DistruptorBullet instance = null;
    private void Awake()
    {
        instance = this;
        audioData.Play(0);
    }

     private void OnCollisionEnter2D(Collision2D other)
    {//if collision occured


        if (other.gameObject.tag == "Player"){//if collision with player
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());//ignores player
            Instantiate(bullestExplosion, this.transform.position, Quaternion.identity);//blowing up particles play
            Destroy(this.gameObject);//die
        }else{//if any other thing collided with
            Instantiate(bullestExplosion, this.transform.position, Quaternion.identity);//blowing up particles play
            Destroy(this.gameObject);//die
        }

       
    }
}
