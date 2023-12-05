using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringScript : MonoBehaviour
{
    public int score;
    [SerializeField] public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
    }
}
