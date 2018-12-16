using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    private Vector3 fireballOriginalScale;
    private Rigidbody rb;
    bool grounded, onFire, gameWon;

    public GameObject cam;
    private CameraController camControl;
    public GameObject manager;
    private GameController gameManager;
    private GameObject puller;
    private Puller pull;
    private GameObject launcher;
    private LauncherScript launch;

    public GameObject explosionPrefab, smallExplosionPrefab, fireball, hugeExplosionPrefab;
    public AudioClip winSound, flySound, dingSound, fireballSound, fireSound, clinkSound, distantExplosionSound;
    AudioSource audioSrc;
    public AudioSource audioSrc2;
    int i = 0; 

    GameObject[] itemList = new GameObject[3];
    int numItems = 0, maxhp = 100;
    int hp, fireballDuration;
    bool detonatorUsed;

    void Start() {
        fireballDuration = 0;
        fireballOriginalScale = fireball.transform.lossyScale;
        rb = GetComponent<Rigidbody>();
        camControl = cam.GetComponent<CameraController>();
        gameManager = manager.GetComponent<GameController>();
        audioSrc = GetComponent<AudioSource>();
        hp = maxhp;
        gameManager.updateHp(hp);
        Time.timeScale = 1;
        audioSrc.pitch = 1;
    }

    void FixedUpdate() {
        if (onFire) {
            if (i % 20 == 0) {
                updateHp(hp - 1);
            }
            if (i == 0)
                fireball.SetActive(true);
            else if (i > fireballDuration) {
                audioSrc.volume -= .1f;
                fireball.transform.localScale -= Vector3.one * Time.deltaTime * 5;
                if (i > fireballDuration + 10) {
                    fireball.SetActive(false);
                    onFire = false;
                    fireball.transform.localScale = fireballOriginalScale;
                    audioSrc.Stop();
                    audioSrc.volume = 1.0f;
                }
            }
            i++;
        }
        hp = gameManager.getHp();
        checkIfDead();
        if (transform.position.y < -10) {
            gameManager.EndGame();
        }
        Inputs();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * 2;
        Vector3 relativeMovement = Camera.main.transform.TransformVector(movement);

        rb.AddForce(new Vector3(relativeMovement.x, 0.0f, relativeMovement.z) * speed);
        if (rb.velocity.y < 0) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - .42f, rb.velocity.z);
        }
    }

    void Inputs() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (puller != null) {
                pull = puller.GetComponent<Puller>();
                rb.velocity = (pull.transform.position - transform.position) * pull.pullForce;
                audioSrc.PlayOneShot(flySound);
            }
            if (launcher != null) {
                launch = launcher.GetComponent<LauncherScript>();
                rb.velocity += Vector3.up * launch.force;
                audioSrc.PlayOneShot(dingSound);
            }
            if (canUseDetonator()) {
                GameObject lab = GameObject.FindGameObjectWithTag("Lab");
                audioSrc.PlayOneShot(distantExplosionSound);
                Instantiate(hugeExplosionPrefab, lab.transform.position, lab.transform.rotation);
                lab.gameObject.SetActive(false);
                detonatorUsed = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            audioSrc.pitch = (audioSrc.pitch == 0.75f ? 1f : 0.75f);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            audioSrc.pitch = (audioSrc.pitch == 1.5f ? 1f : 1.5f);
        }
    }

    public void setPuller(GameObject puller) {
        this.puller = puller;
    }

    public void setLauncher(GameObject launcher) {
        this.launcher = launcher;
    }

    private void OnTriggerEnter(Collider col) {
        if (col.tag == "Silver Key" || col.tag == "Gold Key" || col.tag == "Detonator") {
            audioSrc.PlayOneShot(dingSound);
            itemList[numItems++] = col.gameObject;
            col.gameObject.SetActive(false);
            gameManager.updateItemsText(itemList, numItems);
        }
        if (col.tag == "Finish") {
            gameWon = true;
            audioSrc.PlayOneShot(winSound);
            gameManager.levelComplete();
        }
        if (col.tag == "Red Barrel") {
            updateHp(hp - 40);
            rb.velocity = (transform.position - col.transform.position) * 20;
            Instantiate(smallExplosionPrefab, col.transform.position, col.transform.rotation);
            col.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (rb.velocity.x > 3 || rb.velocity.y > 3 || rb.velocity.z > 3)
            audioSrc2.PlayOneShot(clinkSound);
    }

    private void OnParticleCollision(GameObject col) {
        if (col.tag == "Flames") {
            if (!audioSrc.isPlaying) {
                audioSrc.PlayOneShot(fireballSound);
                audioSrc.PlayOneShot(fireSound);
            }
            fireballDuration = 300;
            onFire = true;
            i = 0;
        }
    }

        public bool useKey(String keyNeeded) {
        if (numItems > 0) {
            for (int i = 0; i < numItems; i++) {
                if (itemList[i].tag == keyNeeded) {
                    return true;
                }
            }
        }
        return false;
    }

    public bool canUseDetonator() {
        if (!detonatorUsed) {
            if (numItems > 0) {
                for (int i = 0; i < numItems; i++) {
                    if (itemList[i].tag == "Detonator") {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void checkIfDead() {
        if (hp <= 0) {
            hp = 0;
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    void updateHp(int hp) {
        this.hp = hp;
        if (hp < 0) {
            hp = 0;
            gameManager.EndGame();
        }
        gameManager.updateHp(hp);
    }
}