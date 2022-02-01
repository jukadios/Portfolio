using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_Trail : MonoBehaviour {

    [SerializeField]
    ParticleSystem snowParticle;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground") {
            this.GetComponent<AudioSource>().Play();
            snowParticle.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Ground") {
            this.GetComponent<AudioSource>().Stop();
            snowParticle.Stop();
        }
    }
}
