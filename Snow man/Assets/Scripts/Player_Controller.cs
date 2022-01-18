using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    [SerializeField]
    private float torque;
    private Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {

        if (Input.GetKey(KeyCode.D)) {
            rb2d.AddTorque(-torque);
        }
        else if (Input.GetKey(KeyCode.A)) {
            rb2d.AddTorque(torque);
        }
    }
}
