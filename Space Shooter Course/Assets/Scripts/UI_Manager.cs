using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start() {
        _scoreText.text = "Score:\t" + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null) {
            Debug.LogError("GameManager is Null");
        }
    }

    public void Score(int points) {
        _scoreText.text = "Score:\t" + points.ToString();
    }

    public void UpdteLives(int lives) {
        _livesImg.sprite = _liveSprites[lives];

        if (lives == 0) {
            GameOverSequence();
        }
    }

    public void UpdteLives2(int lives) {
        //_livesImg.sprite = _liveSprites[lives];

        if (lives == 0) {
            GameOverSequence();
        }
    }

    void GameOverSequence() {
        _gameManager.GameOver();
        _restartText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker() {
        while (true) {
            _gameOverText.text = "GAME OVER";
            _gameOverText.color = new Color(1, 0, 0, 1);
            yield return new WaitForSeconds(1f);
            _gameOverText.text = "GAME OVER";
            _gameOverText.color = new Color(0, 1, 0, 1);
            yield return new WaitForSeconds(1f);
        }
    }

}
