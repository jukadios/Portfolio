using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Image coins;

    public void updateAmmo(int count, bool weapon) {
        if (weapon)
            ammoText.text = "Ammo: " + count;
        else
            ammoText.text = "";
    }

    public void updateCoins(int coin) {
        if (coin > 0)
            coins.enabled = true;
        else
            coins.enabled = false;
    }
}
