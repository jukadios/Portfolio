using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioWin;

    private void OnTriggerStay(Collider other) {
        
        if (other.tag == "Player") {
            if (Input.GetKeyDown(KeyCode.E)) {
                Player player = other.GetComponent<Player>();

                if (player.coins > 0) {
                    player.coins--;
                    player.EnableWeapons();
                    AudioSource.PlayClipAtPoint(audioWin, transform.position, 1);
                }
                else
                    Debug.Log("No money, no weapons");
            }
        }

    }
}
