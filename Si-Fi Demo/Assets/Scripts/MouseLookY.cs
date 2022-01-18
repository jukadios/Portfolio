using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookY : MonoBehaviour
{

    private float sensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousey = Input.GetAxis("Mouse Y");
        Vector3 rotationx = transform.localEulerAngles;
        rotationx.x -= (mousey * sensitivity);
        transform.localEulerAngles = rotationx;
    }
}
