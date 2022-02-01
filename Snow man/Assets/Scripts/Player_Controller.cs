using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    [SerializeField]
    private float torque, speed = 35;
    private Rigidbody2D rb2d;
    public bool alive = true;

    private SurfaceEffector2D se2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        se2d = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update() {
        RotatePlayer();
        SpeedBoost();
    }

    void RotatePlayer() {
        if (Input.GetKey(KeyCode.D) && alive) {
            rb2d.AddTorque(-torque);
        }
        else if (Input.GetKey(KeyCode.A) && alive) {
            rb2d.AddTorque(torque);
        }
    }

    void SpeedBoost () {
        if (Input.GetKey(KeyCode.Space) && alive) {
            speed = 50;
            se2d.speed = speed;
        }
        else {
            speed = 35;
            se2d.speed = speed;
        }
    }
}
