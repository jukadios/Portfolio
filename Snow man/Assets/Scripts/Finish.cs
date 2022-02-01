using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    [SerializeField]
    ParticleSystem finishParticle;

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            Time.timeScale = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            StartCoroutine(Continue());            
        }

    }

    IEnumerator Continue() {
        Time.timeScale = 1f;
        finishParticle.Play();
        this.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        Time.timeScale = 0f;
    }
}
