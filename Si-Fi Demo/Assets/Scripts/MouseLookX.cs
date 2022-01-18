using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookX : MonoBehaviour
{

    private float sensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X");
        Vector3 rotationy = transform.localEulerAngles;
        rotationy.y += (mousex * sensitivity);
        transform.localEulerAngles = rotationy;
    }
}
