using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour {

    public GameObject ball;
    public float pullForce;
    private PlayerController player;

    private void Start() {
        player = ball.GetComponent<PlayerController>();
        (gameObject.GetComponent("Halo") as Behaviour).enabled = false;
    }

    private void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            (gameObject.GetComponent("Halo") as Behaviour).enabled = true;
            player.setPuller(gameObject);
        }
    }
    private void OnTriggerExit(Collider col) {
        (gameObject.GetComponent("Halo") as Behaviour).enabled = false;
        player.setPuller(null);
    }
}
