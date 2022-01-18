using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIM : MonoBehaviour {

    [SerializeField]
    private Text _text, _lives;

    public void CoinsDisplay(int coins) {
        _text.text = "Coins: " + coins;
    }

    public void LivesDisplay(int lives) {
        _lives.text = "Lives: " + lives;
    }
}
