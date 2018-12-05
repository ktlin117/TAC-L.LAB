using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;

    void Update() {
        // Quit anytime using ESC or "Q"
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) {
            Time.timeScale = 1;
            Debug.Log("QUIT button hit");     // for debugging in editor
            Application.Quit();
        }
        // Hit 'R' to restart level
        if (Input.GetKeyDown(KeyCode.R)) {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // Hit 'P' to pause/unpause game
        if (!gameHasEnded && Input.GetKeyDown(KeyCode.P)) {
            Time.timeScale = (Time.timeScale > 0 ? 0 : 1); // toggle 1 <--> 0
        }
        // Hit 'Z' to toggle slow-motion
        if (!gameHasEnded && Input.GetKeyDown(KeyCode.Z)) {
            Time.timeScale = (Time.timeScale == 0.5f ? 1 : 0.5f); // toggle 1 <--> 0.5 
        }
    }

    public void CompleteLevel() {
        completeLevelUI.SetActive(true);
    }

    public void EndGame() {
        if (gameHasEnded == false) {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
