using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriver : MonoBehaviour {

    [SerializeField]
    private float rot = 1f;

    [SerializeField]
    public float speed = 10f, healt = 30;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject[] items;

    [SerializeField]
    private GameObject spawnManager;

    private SpawnManager spawn;

    public bool speedUp = false, package = false, delivery = false;
    public int deliveries = 0;

    void Start() {
        Delivery();
        Package();
        SpeedUp();
        spawn = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        transform.Rotate(new Vector3(0, 0, 0));
    }

    void Update() {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Tree" || other.tag == "House" || other.tag == "Rock") {
            float lessHealt = Random.Range(1f, 3f);
            healt -= lessHealt;
            Debug.Log(healt);
        }
        if (other.tag == "Delivery" && package) {
            StartCoroutine(NewDelivery());
        }
        else if(other.tag == "Package") {
            spawn.DestItems(1);
            package = true;
        }
        else if(other.tag == "SpeedUp") {
            StartCoroutine(NewSpeedUp());
        }
    }

    void Movement() {
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(new Vector3(0, 0, (rot * -1) * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(new Vector3(0, 0, rot * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    public void Delivery() {
        float randomX1 = Random.Range(-20f, 150f);
        float randomY1 = Random.Range(33.4f, 90f);
        GameObject item = Instantiate(items[0], new Vector3(randomX1, randomY1, 0), Quaternion.identity);

        item.transform.parent = spawnManager.transform;
    }

    public void Package() {
        float randomX1 = Random.Range(-20f, 150f);
        float randomY1 = Random.Range(33.4f, 90f);
        GameObject item = Instantiate(items[1], new Vector3(randomX1, randomY1, 0), Quaternion.identity);

        item.transform.parent = spawnManager.transform;
    }

    public void SpeedUp() {
        float randomX1 = Random.Range(-20f, 150f);
        float randomY1 = Random.Range(33.4f, 90f);
        GameObject item = Instantiate(items[2], new Vector3(randomX1, randomY1, 0), Quaternion.identity);

        item.transform.parent = spawnManager.transform;
    }

    IEnumerator NewDelivery() {
        spawn.DestItems(0);
        yield return new WaitForSeconds(1);
        package = false;
        Delivery();
        Package();
    }
    IEnumerator NewSpeedUp() {
        spawn.DestItems(2);
        speed *= 1.5f;
        yield return new WaitForSeconds(5);
        speed /= 1.5f;
        SpeedUp();
    }
}
