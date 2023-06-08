using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFalconBullet : MonoBehaviour
{
   public ParticleSystem bullestExplosion;
   public AudioSource audioData;


    public static RedFalconBullet instance = null;
    private void Awake()
    {
        instance = this;
        audioData.Play(0);//play bullet sound on awake
    }

     private void OnCollisionEnter2D(Collision2D other)
    {

            Instantiate(bullestExplosion, this.transform.position, Quaternion.identity);//play bullet explosion if collided with anything
            Destroy(this.gameObject);//destroy the bullet
    
    }
}
