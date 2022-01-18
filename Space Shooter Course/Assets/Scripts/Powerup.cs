using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour{

    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerupId;

    // Start is called before the first frame update
    void Start() {
       float _randx = Random.Range(-9.6f, 9.6f);
        transform.position = new Vector3(_randx, 8f, 0);
    }

    // Update is called once per frame
    void Update() {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Player") {

            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
 
                switch (_powerupId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Opcion no valida");
                        break;
                }

            }
            Destroy(this.gameObject);

        }

    }

}