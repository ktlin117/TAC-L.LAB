using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour {

    public float force;
    public GameObject ball;
    private PlayerController player;

    private void Start() {
        player = ball.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            player.setLauncher(gameObject);
        }
    }
    private void OnTriggerExit(Collider col) {
        player.setLauncher(null);
    }

}
