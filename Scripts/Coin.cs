using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public GameObject coinPickupPrefab;

    // Start is called before the first frame update

    void Start()
    {
        Destroy(this.gameObject, 120f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
            GameObject.Find("GameManager").GetComponent<GameManager>().SpawnCoin();
            Destroy(this.gameObject);
        }
    }
}
