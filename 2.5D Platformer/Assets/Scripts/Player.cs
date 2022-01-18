using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 5f, _gravity = .5f, _jump = 15f;

    private float _yvel;
    private bool _djump = false;
    [SerializeField]
    private int _coins, _lives = 3;

    private UIM _UIM;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _UIM = GameObject.Find("Canvas").GetComponent<UIM>();

        if(_UIM == null) {
            Debug.LogError("UI Manager is null");
        }
        else {
            _UIM.CoinsDisplay(_coins);
            _UIM.LivesDisplay(_lives);
        }
    }

    void Update()
    {
        Movement();
    }

    void Movement() {

        float hor = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(hor, 0, 0);
        Vector3 vel = dir * _speed;

        if (_controller.isGrounded == true) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _yvel = _jump;
                _djump = true;
            }
        }
        else {
            if (_djump && Input.GetKeyDown(KeyCode.Space)) {
                _yvel = _jump * 2;
                _djump = false;
            }
            _yvel -= _gravity;
        }

        vel.y = _yvel;
        _controller.Move(vel * Time.deltaTime);

    }

    public void CollectCoins() {
        _coins += 1;

        _UIM.CoinsDisplay(_coins);
    }

    public void LivesCounter() {
        _lives--;

        if(_lives == 0) {
            Debug.Log("You lose, beatch");
            SceneManager.LoadScene(0);
        }

        //transform.position = new Vector3(-10, 1.5f, 0);
        _UIM.LivesDisplay(_lives);
    }

}
