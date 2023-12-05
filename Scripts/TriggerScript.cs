using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public ScoringScript scoring;
    public Transform player;

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player) 
        {
            scoring.score++;
            Debug.Log("Score: " + scoring.score);
            Destroy(gameObject);
            Debug.Log("Collider Destroyed");
        }
    }
}
