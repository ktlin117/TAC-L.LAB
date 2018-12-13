using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    private Rigidbody rb;
    bool grounded;

    public GameObject cam;
    private CameraController camControl;
    public GameObject manager;
    private GameController gameManager;
    private GameObject puller;
    private Puller pull;

    public GameObject explosionPrefab;
    public AudioClip explosionSound, clinkSound, winSound, meowSound;
    AudioSource explosionAudio, clinkAudio, winAudio, meowAudio;

    public GameObject[] itemList = new GameObject[3];
    int numItems = 0;
    int hp = 50;

    void Start() {
        rb = GetComponent<Rigidbody>();
        camControl = cam.GetComponent<CameraController>();
        gameManager = manager.GetComponent<GameController>();
        explosionAudio = GetComponent<AudioSource>();
        clinkAudio = GetComponent<AudioSource>();
        winAudio = GetComponent<AudioSource>();
        meowAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        hp = gameManager.getHp();
        if (hp <= 0) {
            hp = 0;
            explosionAudio.PlayOneShot(explosionSound);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
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
            itemList[numItems++] = col.gameObject;
            col.gameObject.SetActive(false);
            gameManager.updateItemsText(itemList, numItems);
        }
        if (col.tag == "Finish") {
            gameManager.levelComplete();
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
}