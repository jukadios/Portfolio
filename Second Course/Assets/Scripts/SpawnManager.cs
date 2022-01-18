using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*[SerializeField]
    private GameObject[] items;*/

    private CarDriver driver;

    void Start()
    {
        driver = GameObject.Find("Car 3").GetComponent<CarDriver>();
    }

    void Update()
    {
        
    }

    public void DestItems(int index) {

        switch(index) {
            case 0:
                Debug.Log("Delivery" + this.transform.GetChild(index));
                Destroy(this.transform.GetChild(index).gameObject);
                break;
            case 1:
                Debug.Log("Package" + this.transform.GetChild(index));
                Destroy(this.transform.GetChild(index).gameObject);
                break;
            case 2:
                Debug.Log("Speed up" + this.transform.GetChild(index));
                Destroy(this.transform.GetChild(index).gameObject);
                break;
            default:
                break;

        }
    }

}
