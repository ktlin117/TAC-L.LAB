using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    private Vector3 fireballOriginalScale;
    private Rigidbody rb;
    bool grounded, onFire;

    public GameObject cam;
    private CameraController camControl;
    public GameObject manager;
    private GameController gameManager;
    private GameObject puller;
    private Puller pull;

    public GameObject explosionPrefab, smallExplosionPrefab, fireball;
    public AudioClip winSound, flySound, dingSound, fireballSound;
    AudioSource audioSrc;
    public int i = 0; 
    int fireballDuration = 300;

    public GameObject[] itemList = new GameObject[3];
    int numItems = 0, maxhp = 100;
    public int hp;

    void Start() {
        fireballOriginalScale = fireball.transform.lossyScale;
        rb = GetComponent<Rigidbody>();
        camControl = cam.GetComponent<CameraController>();
        gameManager = manager.GetComponent<GameController>();
        audioSrc = GetComponent<AudioSource>();

        hp = maxhp;
        gameManager.updateHp(hp);
    }

    void FixedUpdate() {
        if (onFire) {
            if (i % 20 == 0) {
                updateHp(hp - 1);
            }
            if (i == 0)
                fireball.SetActive(true);
            else if (i > fireballDuration) {
                fireball.transform.localScale -= Vector3.one * Time.deltaTime * 5;
                if (i > fireballDuration + 10) {
                    fireball.SetActive(false);
                    onFire = false;
                    fireball.transform.localScale = fireballOriginalScale;
                }
            }
            i++;
        }
        hp = gameManager.getHp();
        checkIfDead();
        if (transform.position.y < -10) {
            gameManager.EndGame();
        }
        Inputs();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * 2;
        Vector3 relativeMovement = Camera.main.transform.TransformVector(movement);

        rb.AddForce(relativeMovement * speed);
        if (rb.velocity.y < 0) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - .42f, rb.velocity.z);
        }
    }

    void Inputs() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (puller != null) {
                pull = puller.GetComponent<Puller>();
                rb.velocity = (pull.transform.position - transform.position) * pull.pullForce;
                audioSrc.PlayOneShot(flySound);
                Debug.Log("Pull!");
            }
            else {
                Debug.Log("No pull.");
            }
        }
    }

    public void setPuller(GameObject puller) {
        this.puller = puller;
    }

    private void OnTriggerEnter(Collider col) {
        Debug.Log("Trigger activated");
        if (col.tag == "Key") {
            audioSrc.PlayOneShot(dingSound);
            itemList[numItems++] = col.gameObject;
            col.gameObject.SetActive(false);
            gameManager.updateItemsText(itemList, numItems);
        }
        if (col.tag == "Finish") {
            audioSrc.PlayOneShot(winSound);
            gameManager.levelComplete();
        }
        if (col.tag == "Red Barrel") {
            updateHp(hp - 40);
            rb.velocity = (transform.position - col.transform.position) * 20;
            Instantiate(smallExplosionPrefab, col.transform.position, col.transform.rotation);
            
            col.gameObject.SetActive(false);
        }
        if (col.tag == "Flames") {
            audioSrc.PlayOneShot(fireballSound);
        }
    }

    private void OnTriggerStay(Collider col) {
        if (col.tag == "Flames") {
            Debug.Log("Flames");
            onFire = true;
            i = 0;
        }
    }


        public bool useKey() {
        if (numItems > 0) {
            for (int i = 0; i < numItems; i++) {
                if (itemList[i].tag == "Key") {
                    return true;
                }
            }
        }
        return false;
    }

    void checkIfDead() {
        if (hp <= 0) {
            hp = 0;
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    void updateHp(int hp) {
        this.hp = hp;
        if (hp < 0) {
            hp = 0;
            gameManager.EndGame();
        }
        gameManager.updateHp(hp);
    }
}