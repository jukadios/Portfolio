using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinAudio;

    private void OnTriggerStay(Collider other) {
        
        if (other.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E)) {

                Player player = other.GetComponent<Player>();
                if(player != null) {
                    player.coins ++;
                    AudioSource.PlayClipAtPoint(coinAudio, transform.position, 1);
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
