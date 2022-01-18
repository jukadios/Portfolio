using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    //public puede puedes alterar desde inspector de unity, si es private no.
    public bool _isPlayerOne = false;
    public bool _isPlayerTwo = false;

    [SerializeField]//muestra los valores privados en unity para modificarlos
    private float speed = 10f;
    private float _speedMul = 1.5f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShot;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _fireRatio = .2f;
    private float _canFire = 0;

    private AudioSource _audioLaser;
    ///private AudioSource _audioExplotion;
    private AudioSource _audioPp;

    [SerializeField]
    private int _lives = 3;
    private int _lives2 = 3;
    private Spawn_M _spawnManager;

    private bool _tripleShotActive = false;
    private bool _shieldsActive = false;

    [SerializeField]
    private int _score;

    private UI_Manager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    //private IEnumerator _coroutine;

    // Start is called before the first frame update
    void Start() {

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_M>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioLaser = GameObject.Find("Audio_Laser").GetComponent<AudioSource>();
        _audioPp = GameObject.Find("Audio_Powerup").GetComponent<AudioSource>();

        if (_gameManager._isCoop == false) {
            //Take the current position = new positon (0,0,0)
            transform.position = new Vector3(0, -3, 0);
        }

        if (_spawnManager == null) {
            Debug.LogError("Spawn Manager value is NULL");
        }

        if (_uiManager == null) {
            Debug.Log("UI Manager es nulo");
        }

        _shield.SetActive(false);
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);

        _audioLaser.Pause();
    }

    // Update is called once per frame
    void Update() {

        if (_isPlayerOne) {
            move();
        }
        if (_isPlayerTwo) {
            movePlayer2();
        }

        screenLimit();

        #if UNITY_ANDROID
            if (( CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canFire) {
                shooting();
            }
        #else
            if (_isPlayerOne && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && Time.time > _canFire) {
                    shooting();
            }
            if (_isPlayerTwo && (Input.GetKeyDown(KeyCode.KeypadEnter)) && Time.time > _canFire) {
                    shooting();
            }
        #endif
        StartCoroutine(sidesLimits());
    }

    void move() {

        /*float horInput = CrossPlatformInputManager.GetAxis("Horizontal");// Input.GetAxis("Horizontal");
        float verInput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horInput, verInput, 0);
        transform.Translate(move * speed * Time.deltaTime);*/

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

    }

    void movePlayer2() {

        /*float horInput = CrossPlatformInputManager.GetAxis("Horizontal");// Input.GetAxis("Horizontal");
        float verInput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horInput, verInput, 0);
        transform.Translate(move * speed * Time.deltaTime);*/

        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    void screenLimit() {
        /*Limites de la pantalla en y para hacer una transision
        Mathf.Clamp solo pone limites no hace la transision pero se puede sustituir en ciertos casos para los if, else
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -6, 7), 0);*/

        if (transform.position.y >= 5.6f) {
            transform.position = new Vector3(transform.position.x, 5.6f, 0);
        } else if (transform.position.y <= -3.6f) {
            transform.position = new Vector3(transform.position.x, -3.6f, 0);
        }

        //Limites de la pantalla en x para hacer una transision

        if (transform.position.x >= 11.3f) {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x <= -11.3f) {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    /************Borra al jugador si permanece mas de 5 segundos en los limites***************/
    IEnumerator sidesLimits() {
        if (transform.position.x >= 10.5f || transform.position.x <= -10.5f) {
            yield return new WaitForSeconds(5f);
            if (transform.position.x >= 10.5f || transform.position.x <= -10.5f) {
                //Debug.Log("hola");
                Destroy(this.gameObject);
                _lives = 0;

                _uiManager.UpdteLives(_lives);

                if (_lives <= 0) {
                    _spawnManager.OnPlayerDeath();
                    Destroy(this.gameObject);
                    _lives = 0;
                }
            }
            else if(transform.position.x < 10.4f || transform.position.x > -10.4f) {
                //Debug.Log("adios");
                yield return 0;
            }
        }
    }

    

    void shooting() {

        //|| Input.GetKeyDown(KeyCode.Mouse0) tecla para disparar con el mouse

        //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canFire) {

            _canFire = Time.time + _fireRatio;

            if (_tripleShotActive == true) {
                Instantiate(tripleShot, transform.position, Quaternion.identity);
            } else {
                //transform.position is the spawn position of the laser, Quaternion is for control the rotation in unity
                Instantiate(laserPrefab, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
            }
            _audioLaser.Play();
        //}

    }

    //public deja que otros archivos accesen a eso
    public void damage() {

        if (_isPlayerOne) {

            if (_shieldsActive == true) {
                _shieldsActive = false;
                _shield.SetActive(false);
                return;
            }

            _lives--;

            if(_lives == 2) {

                int _damEngine = Random.Range(0, 3);
                if (_damEngine == 1)
                    _rightEngine.SetActive(true);
                else
                    _leftEngine.SetActive(true);

            }else if(_lives == 1) {

                if (_rightEngine.activeSelf == true)
                    _leftEngine.SetActive(true);
                else
                    _rightEngine.SetActive(true);

            }

            _uiManager.UpdteLives(_lives);

        }

        if (_isPlayerTwo) {

            if (_shieldsActive == true) {
                _shieldsActive = false;
                _shield.SetActive(false);
                return;
            }

            _lives2--;

            if (_lives2 == 2) {

                int _damEngine = Random.Range(0, 3);
                if (_damEngine == 1)
                    _rightEngine.SetActive(true);
                else
                    _leftEngine.SetActive(true);

            }
            else if (_lives2 == 1) {

                if (_rightEngine.activeSelf == true)
                    _leftEngine.SetActive(true);
                else
                    _rightEngine.SetActive(true);

            }

            _uiManager.UpdteLives2(_lives2);

        }

        if (_lives <= 0 || _lives2 <= 0) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _lives = 0;
        }
    }

    public void TripleShotActive()
    {
        _tripleShotActive = true;
        _audioPp.Play();
        StartCoroutine(TPCoolDown());
    }

    IEnumerator TPCoolDown()
    {
        yield return new WaitForSeconds(5f);
        _tripleShotActive = false;
    }

    public void SpeedActive()
    {
        speed *= _speedMul;
        _audioPp.Play();
        StartCoroutine(SpeedCoolDown());
    }

    IEnumerator SpeedCoolDown()
    {
        yield return new WaitForSeconds(5f);
        speed /= _speedMul;
    }

    public void ShieldsActive()
    {
        _shield.SetActive(true);
        _audioPp.Play();
        _shieldsActive = true;
    }

    public void Score(int points) {
        _score += points;
        _uiManager.Score(_score);
    }
}
