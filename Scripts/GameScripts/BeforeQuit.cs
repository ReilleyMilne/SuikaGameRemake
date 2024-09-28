using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeQuit : MonoBehaviour
{
    void OnApplicationQuit()
    {
        GameObject scoreText = GameObject.Find("Scores");
        ScoreTracker scoreTracker = scoreText.GetComponent<ScoreTracker>();
        scoreTracker.saveScoresToCloud();
    }
}