using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject entity;
    public Transform home;
    private float randomX = 0;
    private float randomY = 0;
    private Vector3 spawnLocation = new Vector3(0f,0f,1f);
    public float spawnRate = 2.0f;
    private float nextSpawn = 0.0f;
    float numberOfSpawns =  Mathf.Infinity;//set to spawn forever
    private int spawnsOccured = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn){//compares game time with nextspawn which increments every spawn

            randomX = Random.Range(40f,150f)*direction();//picks randomx coordinate in range
            randomY = Random.Range(40f,150f)*direction();//picks randomx coordinate in range
            spawnLocation = new Vector3(randomX,randomY,transform.position.z);//makes spawn location using random coordinates

            if (spawnsOccured != numberOfSpawns){//checks if spawn limit is reached
                spawnsOccured++;//increments spawn occured
                nextSpawn = Time.time + spawnRate;//picks when next spawn will occur
                Instantiate(entity, spawnLocation, Quaternion.identity);//spawns entity
            }  

        }

        
    }

    public float direction(){//picks if spawn should occur either on the negative axis or positive axis
        float value = Mathf.Floor(Random.Range(0.01f,1.99f));
        if(value == 0){
            return -1f;
        }else{
            return 1f;
        }
    }
}
