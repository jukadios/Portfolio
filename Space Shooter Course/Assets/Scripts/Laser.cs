using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    //Always use "_" before the name of private
    private float _speed = 8f;
    private bool _isEnemyLaser = false;

    void Start()
    {
        transform.position =  new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_isEnemyLaser == false) {
            MoveUp();
        }
        else {
            MoveDown();
        }
    }

    void MoveUp() {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8) {
            if (transform.parent) {
                Destroy(transform.parent.gameObject);
            }

            //se le pone this.gameobject si no solo destruye el script y  no el objeto 
            Destroy(this.gameObject, .1f);
        }
    }

    void MoveDown() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8) {
            if (transform.parent) {
                Destroy(transform.parent.gameObject);
            }

            //se le pone this.gameobject si no solo destruye el script y  no el objeto 
            Destroy(this.gameObject, .1f);
        }
    }


    public void AssaignEnemy() {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player" && _isEnemyLaser == true) {
            Player player = other.GetComponent<Player>();

            if (player != null) {
                player.damage();
                Destroy(this.gameObject);
            }
        }
    }
}
