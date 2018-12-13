using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    bool gameHasEnded = false;
    private float restartDelay = 1.5f;
//  public GameObject completeLevelUI;
    private float nextLevelDelay = 3f;
    public static int numLives = 3;
    public int hpCount = 50;
    public Text lives;
    public Text gameState;
    public Text items;
    public Text hp;

    void Start() {
        lives.text = "Lives: " + numLives;
        gameState.text = "";
        items.text = "Items: None";
        hp.text = "HP: " + hpCount;
    }

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

/*    public void CompleteLevel() {
        completeLevelUI.SetActive(true);
    }*/

    public void EndGame() {
        if (gameHasEnded == false) {
            hpCount = 0;
            hp.text = "HP: " + hpCount;
            lives.text = "Lives: " + --numLives;
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            gameState.text = "You died!";
            Invoke("Restart", restartDelay);
        }
    }

    public void levelComplete() {
        gameHasEnded = true;
        lives.text = "Lives: " + ++numLives;
        Debug.Log("You win!");
        gameState.text = "You completed the level!";
        Invoke("NextLevel", nextLevelDelay);
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
