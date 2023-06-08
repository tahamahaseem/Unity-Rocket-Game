using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SlowDownTime : MonoBehaviour
{
    public AudioSource into;
    public GameObject player;
    public AudioSource outOf;
    public float duration = 2f;
    public float timeSpeed = 1.0f;
    public float lowestSpeed = 0.5f;
    bool pressed, timeWasChanged;
    float chance, multipler;


    
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        timeWasChanged = false;
        multipler = lowestSpeed/timeSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    if(Input.GetMouseButtonDown(0)){
        chance = Mathf.Floor(Random.Range(0,5));
        pressed = true;
    }else if (Input.GetMouseButtonUp(0)){
        pressed = false;
    }
    
        if(pressed && chance == 0){
            chance = 1;
            if(!timeWasChanged){
                StartCoroutine(slowDown());
            }      
        }
    }

    IEnumerator slowDown(){
        timeWasChanged = true;
        into.Play(0);
        for(float i = timeSpeed; i>=lowestSpeed;i = i - 0.00001f){
        Time.timeScale = i;
        }
        player.GetComponent<PlayerShooting>().fireRate = player.GetComponent<PlayerShooting>().fireRate*(multipler);
        player.GetComponent<PlayerShooting>().bulletForce = player.GetComponent<PlayerShooting>().bulletForce/(multipler)*0.75f;
        player.GetComponent<playerMovement>().moveSpeed = player.GetComponent<playerMovement>().moveSpeed/(multipler);
        yield return new WaitForSeconds(duration);
        into.Stop();
        outOf.Play(0);
        player.GetComponent<PlayerShooting>().fireRate = player.GetComponent<PlayerShooting>().fireRate/(multipler);
        player.GetComponent<PlayerShooting>().bulletForce = player.GetComponent<PlayerShooting>().bulletForce*(multipler)/0.75f;
        player.GetComponent<playerMovement>().moveSpeed = player.GetComponent<playerMovement>().moveSpeed*(multipler);
        yield return new WaitForSeconds(0.2f);
        for(float i = lowestSpeed; i<timeSpeed;i = i + 0.00001f){
        Time.timeScale = i;
        }
        
        timeWasChanged = false;

    }
}
