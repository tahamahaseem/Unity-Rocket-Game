using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homeHealth : MonoBehaviour
{
    public float health = 25;//player health
    GameObject playerBody;
    GameObject[] myObjects;
    public ParticleSystem death;
    public SpriteRenderer color;
    public SpriteRenderer indicatorColor;
    public static homeHealth instance = null;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {//if collision occured



        if (other.gameObject.tag == "PassiveBullet")//passive bullets are bullets that don't hurt the player, but only hurt the planet
        {
            health--;//hurts planet
            StartCoroutine(Hurt());
        }
        if (other.gameObject.tag == "AgressiveBullet")
        {
            health = health - 0.1f;//hurts planet
            StartCoroutine(Hurt());
        }
        if (other.gameObject.tag == "Mob")//if enemy crashes into planet
        {
            health = health - 5;//this hurt more than bullets
            StartCoroutine(Hurt());
        }
        if (other.gameObject.tag == "Planet")//if a planet collides with another planet
        {
            health = 0;//instantly die
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player");
        myObjects = GameObject.FindGameObjectsWithTag("Mob");
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0){
            Instantiate(death, transform.position, Quaternion.identity);//play death particles
            Destroy(playerBody);//destroy player
            for(int i = 0; i < myObjects.Length; i++){
                Destroy(myObjects[i]);//destory all mobs types when planet dies
            }
            Destroy(this.gameObject);//destroy planet
        }
        
    }

    IEnumerator Hurt(){//player hurt animation
       color.color = new Color(150f, 0f, 0f, 50f);
       indicatorColor.color = new Color(150f, 0f, 0f, 50f);
       yield return new WaitForSeconds(0.2f);
       color.color = new Color(255f, 255f, 255f, 1f);
       indicatorColor.color = new Color(255f, 255f, 255f, 1f);
      }
}
