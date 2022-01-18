using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField]
    private Transform targetA, targetB, targetC;
    [SerializeField]
    private bool platform2;
    [SerializeField]
    private float speed = 1, speed2 = 3;

    private bool change = false, pB = false, pA = false, pC = false;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (!platform2) {
            BasicPlatform();
        }
        else {
            ComplexPlatform();
        }
    }

    void BasicPlatform() {
        if (!change) {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, Time.deltaTime * speed);
            if (transform.position == targetB.position) {
                change = true;
            }
        }
        else if (change) {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, Time.deltaTime * speed);
            if (transform.position == targetA.position) {
                change = false;
            }
        }
    }

    void ComplexPlatform() {
        if (!pA && !pB && !pC) {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, Time.deltaTime * speed2);
            if (transform.position == targetB.position) {
                pB = true;
            }
        }
        else if (!pA && pB && !pC) {
            transform.position = Vector3.MoveTowards(transform.position, targetC.position, Time.deltaTime * speed2);
            if (transform.position == targetC.position) {
                pC = true;
                pB = false;
            }
        }
        else if (!pA && !pB && pC) {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, Time.deltaTime * speed2);
            if (transform.position == targetB.position) {
                pC = false;
                pA = true;
            }
        }
        else if (pA && !pB && !pC) {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, Time.deltaTime * speed2);
            if (transform.position == targetA.position) {
                pA = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Player") {
            other.transform.SetParent(this.gameObject.transform);
        }
    }

    void OnTriggerExit(Collider other) {
        
        if(other.tag == "Player") {
            other.transform.SetParent(null);
        }

    }

}
