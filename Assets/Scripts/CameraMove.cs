using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {



    int offset = 100;
    int speed = 100;
    public int MinXPosition, MaxXPosition, MinZPosition, MaxZPosition;

    void Update()
    {


        if (transform.position.x < MaxXPosition)
            if ((Input.mousePosition.x >= Screen.currentResolution.width - offset))
                transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x > MinXPosition)
            if ((Input.mousePosition.x <= offset))
                transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.z < MaxZPosition)
            if ((Input.mousePosition.y >= Screen.currentResolution.height - offset))
                transform.Translate(0, 0, speed * Time.deltaTime);
        if (transform.position.z > MinZPosition)
            if ((Input.mousePosition.y <= offset))
                transform.Translate(0, 0, -speed * Time.deltaTime);
    }
}
