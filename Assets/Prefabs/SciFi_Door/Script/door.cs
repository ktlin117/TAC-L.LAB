using UnityEngine;
using System.Collections;
using TMPro;

public class door : MonoBehaviour {

    GameObject thedoor;
    public GameObject ball, keyNeeded, currDoor;
    public TextMeshProUGUI gameState;
    private PlayerController player;

    public bool unlocked;

    private void Start() {
        player = ball.GetComponent<PlayerController>();
    }

    void OnTriggerEnter (Collider col){
        if (!unlocked) {
            unlocked = player.useKey(keyNeeded.tag);
        }
        if (unlocked) {
            currDoor.GetComponent<Animation>().Play("open");
       }
        else {
            gameState.text = "Need " + keyNeeded.tag + " to open this door!";
        }
    }

    void OnTriggerExit (Collider col) {
        if (unlocked) {
            currDoor.GetComponent<Animation>().Play("close");
        }
        gameState.text = "";
    }
}