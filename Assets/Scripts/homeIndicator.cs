using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homeIndicator : MonoBehaviour
{

    public Transform zaaneva;
    public Transform camera;
    public Vector3 distance;
    public Vector3 desiredPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = camera.position - zaaneva.position;
        //desiredPosition = (camera.position + distance.normalized*-25);
        transform.position = new Vector3((camera.position + distance.normalized*-26).x,(camera.position + distance.normalized*-14).y,1);

       
        if (Mathf.Abs(distance.x) >= 31f || Mathf.Abs(distance.y) >= 20f){
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }else{
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
