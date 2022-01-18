using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            StartCoroutine(Continue());            
        }

    }

    IEnumerator Continue() {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        Time.timeScale = 0f;
    }
}
