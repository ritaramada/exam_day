using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    public ScorePoints scorePoints = null;
    TMP_Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = gameObject.GetComponent<TMP_Text>();

        if (scorePoints != null)
        {
            scoreText.text = scorePoints.score.ToString();
        }
        
    }

}
