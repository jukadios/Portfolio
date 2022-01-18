using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Player _player2;
    private Animator _animEnemyDeath;
    private AudioSource _audioExplotion;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    // Start is called before the first frame update
    void Start() {
        _player = GameObject.Find("Player_1").GetComponent<Player>();
        _player2 = GameObject.Find("Player_2").GetComponent<Player>();
        _audioExplotion = GameObject.Find("Audio_Explotion").GetComponent<AudioSource>();

        float _randx = Random.Range(-9.6f, 9.6f);
        transform.position = new Vector3(_randx, 8f, 0);

        _animEnemyDeath = GetComponent<Animator>();

        if(_animEnemyDeath == null) {
            Debug.Log("Animation is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire) {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject _enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = _enemyLaser.GetComponentsInChildren<Laser>();
            for(int i = 0; i < lasers.Length; i++) {
                lasers[i].AssaignEnemy();
            }
        }
    }

    private void CalculateMovement() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8) {
            float _randx = Random.Range(-9.1f, 9.1f);
            float _randy = Random.Range(8f, 10f);
            transform.position = new Vector3(_randx, _randy, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("Hit: " + other.transform.name);

        if (other.gameObject.tag == "Player_1") {

            //getcomponent<Player> es para poder accesar al script de player y traer la funcion para el daño
            Player player = other.transform.GetComponent<Player>();

            if ( player != null)
            {
                player.damage();
            }

            StartCoroutine(Delay());

        } else if(other.gameObject.tag != "Player_1" && other.gameObject.tag != "Asteroid") {

            if (_player) {
                _player.Score(Random.Range(1,10));
            }

            Destroy(other.gameObject);

            StartCoroutine(Delay());
            //Destroy(other.gameObject);
        }
    }

    private IEnumerator Delay() {
        _animEnemyDeath.SetTrigger("EnemyDeath");
        _audioExplotion.Play();
        _speed = 0;
        Destroy(GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(2.8f);
        Destroy(this.gameObject);
    }
}
