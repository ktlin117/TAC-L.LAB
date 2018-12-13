using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    private Rigidbody rb;
    bool grounded;

    public GameObject cam;
    private CameraController camControl;

    public GameObject manager;
    private GameController gameManager;
    public GameObject puller;
    private Puller pull;
    public GameObject[] itemList = new GameObject[3];
    int numItems = 0;

    void Start() {
        rb = GetComponent<Rigidbody>();
        camControl = cam.GetComponent<CameraController>();
        gameManager = manager.GetComponent<GameController>();
    }

    void FixedUpdate() {
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