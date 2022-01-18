using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Zone : MonoBehaviour
{

    private Player _player;
    [SerializeField]
    private GameObject _respawn;

    void OnTriggerEnter(Collider other) {

        _player = other.GetComponent<Player>();

        if(other.tag == "Player") {
            _player.LivesCounter();

            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null) {
                cc.enabled = false;
            }
            other.transform.position = new Vector3(-10, 1.5f, 0);
            StartCoroutine(Respwan(cc));
            //other.transform.position = _respawn.transform.position;
        }
    }

    IEnumerator Respwan(CharacterController cc) {
        yield return new WaitForSeconds(.5f);
        cc.enabled = true;
    }
}
