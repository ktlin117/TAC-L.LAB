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
    }

    void Inputs() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Space pressed.");
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}