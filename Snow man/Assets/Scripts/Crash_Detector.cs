using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crash_Detector : MonoBehaviour {

    [SerializeField]
    ParticleSystem headParticle;

    Player_Controller player;
    private bool sound = true;

    private void Start() {
        sound = true;
        player = GameObject.Find("Larry").GetComponent<Player_Controller>();
    }

    void OnTriggerEnter2D (Collider2D other) {

        if(other.tag == "Ground") {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit() {
        headParticle.Play();
        player.alive = false;
        if (sound) {
            this.GetComponent<AudioSource>().Play();
            sound = false;
        }
        yield return new WaitForSeconds(1f);
        player.alive = true;
        SceneManager.LoadScene(0);
    }

}
