using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {

    public GameObject ball;
    private PlayerController player;

    private void Start() {
        player = ball.GetComponent<PlayerController>();
    }

    private void Update() {
        transform.position = player.transform.position + new Vector3(0f, -.5f, 0f);
    }

    private void OnCollisionEnter(Collision col) {
        if (col.collider.tag == "End") {
            Debug.Log("You did it!");
        }
    }

    private void OnCollisionExit(Collision col) {
     
    }
}
