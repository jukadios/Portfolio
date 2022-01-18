using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {
            Destroy(this.gameObject);
        }

    }
}
