using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameController gameManager;
    public AudioClip victorySound;
    public AudioSource audioSrc;
    int i = 0, lives = 3;

    private void Start() {
        gameManager.setLives(lives);
    }

    private void Update() {
        i++;
        if (i == 2) {
            Cursor.visible = true;
        }
    }

    public void playGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void addALife() {
        audioSrc.PlayOneShot(victorySound);
        gameManager.setLives(++lives);
    }
}
