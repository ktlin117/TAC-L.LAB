using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioClip mainMenuTheme;
    AudioSource audioSrc;

    private void Start() {
        Cursor.visible = true;
        audioSrc = GetComponent<AudioSource>();
        audioSrc.PlayOneShot(mainMenuTheme);
    }

    public void playGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame() {
        Application.Quit();
    }
}
