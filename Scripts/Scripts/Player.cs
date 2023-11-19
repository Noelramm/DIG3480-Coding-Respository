using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public float playerSpeed;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 4f;
    private int maxLives = 3;
    public int lives;
    public GameObject gM;
    public AudioClip coinSound;
    public AudioClip healthSound;
    public AudioClip shieldPickupSound;
    public AudioClip shieldPowerDownSound;
    public GameObject shield;
    public GameObject playerPlane;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 6f;
        lives = maxLives;
        gM = GameObject.Find("GameManager");
        playerPlane = GameObject.Find("Player(Clone)");
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * playerSpeed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        } else if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseLife()
    {
        lives--;
        //lives -= 1;
        //lives = lives - 1;
        if (lives <= 0) 
        {
            //Game Over
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    public void GainLife()
    {
        lives++;
        if (lives > maxLives)
        {
            lives = maxLives;
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        switch(trigger.name)
        {
            case "Coin(Clone)":
            // Coin pickup
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
                gM.GetComponent<GameManager>().EarnScore(1);
                Destroy(trigger.gameObject);
                break;
            case "Health(Clone)":
            // Health pickup
                AudioSource.PlayClipAtPoint(healthSound, transform.position);
                if (lives >= 3) {
                    gM.GetComponent<GameManager>().EarnScore(1);
                } else if (lives < 3) {
                    lives++;
                    gM.GetComponent<GameManager>().LifeGained(1);
                }
                Destroy(trigger.gameObject);
                break;
            case "ShieldPickup(Clone)":
            // Shield pickup
                AudioSource.PlayClipAtPoint(shieldPickupSound, transform.position);
                Destroy(trigger.gameObject);
                StartCoroutine("ShieldBreak");
                shield.SetActive(true);
                playerPlane.GetComponent<CircleCollider2D>().enabled = false;
                break;
        }
    }

    IEnumerator ShieldBreak ()
    {
        yield return new WaitForSeconds(10f);
        AudioSource.PlayClipAtPoint(shieldPowerDownSound, transform.position);
        playerPlane.GetComponent<CircleCollider2D>().enabled = true;
        shield.SetActive(false);
    }
}


