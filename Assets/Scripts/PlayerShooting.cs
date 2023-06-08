using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioData;
    public Transform firePoint;
    public float fireRate = 0.2f;
    public Transform firePoint2;
    public GameObject bulletPrefab;
    public float bulletForce = 50f;
    bool pressed;
    bool shooting = false;
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetMouseButtonDown(0)){//if left mouse button clicked
        pressed = true;
    }else if (Input.GetMouseButtonUp(0)){//if left mouse button released
        pressed = false;
    }
    if(pressed && !shooting){
        StartCoroutine(Shoot());//play shooting procces if allowed to and if mouse is pressed
    }
       
    }

      IEnumerator Shoot(){
        audioData.Play(0);//play bullet 1 sound
        shooting = true;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);//spawn bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);//add impluse force to bullet so that it launches from firing point
        Destroy(bullet, 3f);
        yield return new WaitForSeconds(fireRate);//wait to shoot next bullet
        audioData.Play(0);//play bullet 2 sound
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);//spawn bullet
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);//add impluse force to bullet so that it launches from firing point
        Destroy(bullet2, 3f);
        yield return new WaitForSeconds(fireRate);//wait to shoot next bullet
        shooting = false;
 

        

      }
}

