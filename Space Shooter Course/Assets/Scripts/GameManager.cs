using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _isCoop = false;
    private bool _gameOver = false;

    private Scene scene;

    private void Start() {
        scene = SceneManager.GetActiveScene();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver) {
            if (scene.name != "Co-Op") {
                SceneManager.LoadScene(1);
            }
            else {
                SceneManager.LoadScene(2);
            }
        }else if (Input.GetKey(KeyCode.Space) && _gameOver){
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    public void GameOver() {
        _gameOver = true;
    }

}
