using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5f, gravity = 9.81f;
    private bool cursor = false;
    [SerializeField]
    private GameObject muzzle, hitMarker, weapon;
    [SerializeField]
    private AudioSource shooting;
    [SerializeField]
    private int currentAmmo, maxAmmo = 50;
    private bool reloading, hasWeapon;

    private UIManager uiManager;

    public int coins = 0;

    void Start() {
        cursor = false;
        hasWeapon = false;
        weapon.SetActive(false);
        controller = GetComponent<CharacterController>();
        currentAmmo = maxAmmo;

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update() {
        uiManager.updateCoins(coins);
        Raycast();
        Movement();
        Pause();
    }

    void Movement() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = dir * speed;
        velocity.y -= gravity;

        velocity = transform.transform.TransformDirection(velocity);

        controller.Move(velocity * Time.deltaTime);
    }

    void Pause() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            cursor = !cursor;
        }
        if(cursor) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Raycast() {
        
        if (Input.GetMouseButton(0) && currentAmmo > 0 && hasWeapon) {

            currentAmmo--;

            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            muzzle.SetActive(true);

            if(!shooting.isPlaying)
                shooting.Play(0);

            if (Physics.Raycast(rayOrigin, out hitInfo)) {
                //Debug.Log("Hit: " + hitInfo.transform.name);
                StartCoroutine(HitMarker(hitInfo));
                Destructibles crate = hitInfo.transform.GetComponent<Destructibles>();
                if(crate != null) {
                    crate.DestroyCrate();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && reloading == false) {
            reloading = true;
            StartCoroutine(Reload());
        }
        else {
            muzzle.SetActive(false);
            shooting.Stop();
        }
        
        uiManager.updateAmmo(currentAmmo, hasWeapon);

    }

    IEnumerator HitMarker(RaycastHit hitInfo) {
        GameObject hitMarker2 = Instantiate(hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        yield return new WaitForSeconds(1);
        Destroy(hitMarker2.gameObject);
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        reloading = false;
    }

    public void EnableWeapons() {
        weapon.SetActive(true);
        hasWeapon = true;
    }
}
