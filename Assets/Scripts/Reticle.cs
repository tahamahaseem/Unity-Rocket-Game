using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public Transform crosshair;
    public ParticleSystem particles;
    Vector2 mousePos;
    // Start is called before the first frame update
    
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame

    void FixedUpdate(){
        crosshair.Rotate(0,0,50*Time.deltaTime);
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if(Vector3.Distance(crosshair.transform.position, mousePos) > 2.5f){
            particles.Play();
        }else if (Vector3.Distance(crosshair.transform.position, mousePos) < 1.2f){
            particles.Stop();
        }
        crosshair.transform.position = new Vector3(mousePos.x,mousePos.y,1);

    }


}
