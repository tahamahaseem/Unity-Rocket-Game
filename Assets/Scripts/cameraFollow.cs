using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Transform target;
    bool pressed;
    Vector3 desiredPostion, smoothPosition, orignalPosition;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {

      
            desiredPostion = target.position + offset;//where the player is currently
            smoothPosition = Vector3.Lerp(transform.position, desiredPostion, smoothSpeed);//slowly takes an interval to move toward player
            transform.position = smoothPosition;//follows player
        
        
        
    }


}
