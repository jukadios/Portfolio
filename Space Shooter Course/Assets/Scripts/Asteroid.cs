using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed = 3f;

    private Player _player;
    [SerializeField]
    private GameObject _asteroidExplotion;

    private Spawn_M _spawnManager;
    private AudioSource _audioExplotion;

    void Start() {

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_M>();
        _audioExplotion = GameObject.Find("Audio_Explotion").GetComponent<AudioSource>();

        if (_asteroidExplotion == null)
            Debug.Log("There's no animation");
    }

    void Update() {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    //chack for laser collission (Trigger)
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Laser") {

            Instantiate(_asteroidExplotion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.SpawningNest();
            Destroy(this.gameObject, 0.1f);
            _audioExplotion.Play();

        }
        else if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player_1" || other.gameObject.tag == "Player_2") {

            Player player = other.transform.GetComponent<Player>();
            Instantiate(_asteroidExplotion, transform.position, Quaternion.identity);
            _spawnManager.SpawningNest();
            Destroy(this.gameObject, 0.1f);
            _audioExplotion.Play();

            if (player)
                player.damage();
        }

    }
}
