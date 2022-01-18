using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

    private Player _player;

    private void OnTriggerEnter(Collider other) {


        if (other.tag == "Player") {
            
            _player = other.GetComponent<Player>();

            if(_player != null) {
                _player.CollectCoins();
                Destroy(this.gameObject);
            }
        }

    }
}
