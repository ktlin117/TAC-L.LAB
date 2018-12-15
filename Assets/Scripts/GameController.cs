using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    bool gameHasEnded = false;
    private float restartDelay = 2.5f;
//  public GameObject completeLevelUI;
    private float nextLevelDelay = 3.5f;
    public static int numLives = 3;
    public int hpCount;
    public Text lives, gameState, items, hp;
    public AudioClip gameMusic;
    AudioSource audioSrc;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        lives.text = "Lives: " + numLives;
        gameState.text = "";
        items.text = "Items: None";
        hp.text = "HP: " + hpCount;
    }

    void Update() {
        if (hpCount == 0) {
            EndGame();
        }
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
            audioSrc.Stop();
            hpCount = 0;
            hp.text = "HP: " + hpCount;
            lives.text = "Lives: " + --numLives;
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            if (numLives >= 0) {
                gameState.text = "You died!";
            }
            else {
                numLives = 4;
                gameState.text = "You lost all your lives! You will be given 5 additional lives.";
                lives.text = "Lives: " + numLives;
            }
            Invoke("Restart", restartDelay);
        }
    }

    public void levelComplete() {
        audioSrc.Stop();
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

    public void updateItemsText(GameObject[] itemsList, int itemCount) {
        items.text = "Items: ";
        for (int i = 0; i < itemCount; i++) {
            if (i == 0)
                items.text += itemsList[i].tag;
            else
                items.text += ", " + itemsList[i].tag;
        }
    }

    public int getHp() {
        return hpCount;
    }

    public void updateHp(int hp) {
        hpCount = hp;
        this.hp.text = "HP: " + hpCount;
    }

}
