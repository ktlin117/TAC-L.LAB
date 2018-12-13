using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour {

    public GameObject ball;
    private PlayerController player;

    // Use this for initialization
    void Start () {
        player = ball.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + new Vector3(0f, .5f, .05f);
	}
}
