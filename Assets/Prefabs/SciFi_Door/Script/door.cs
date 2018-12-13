using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {

    GameObject thedoor;
    public GameObject ball;
    private PlayerController player;

    public bool unlocked;

    private void Start() {
        player = ball.GetComponent<PlayerController>();
    }

    void OnTriggerEnter (Collider col){
        unlocked = player.useKey();
        if (unlocked) {
             thedoor = GameObject.FindWithTag("SF_Door");
             thedoor.GetComponent<Animation>().Play("open");
       }
    }

    void OnTriggerExit (Collider col) {
        if (unlocked) {
             thedoor = GameObject.FindWithTag("SF_Door");
             thedoor.GetComponent<Animation>().Play("close");
        }
     }
}