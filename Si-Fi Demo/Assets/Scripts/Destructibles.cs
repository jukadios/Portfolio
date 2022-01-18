using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{
    [SerializeField]
    private GameObject crateDestoyed;

    public void DestroyCrate() {

        Instantiate(crateDestoyed, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
    }

}
