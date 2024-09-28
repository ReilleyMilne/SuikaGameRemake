using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public TMP_Text currScoreText;
    public TMP_Text currLeaderboardScoreText;
    public TMP_Text LeaderboardTextScore1;
    public TMP_Text LeaderboardTextScore2;
    public TMP_Text LeaderboardTextScore3;

    public TMP_FontAsset customFont;

    private int currTotalScore;

    private bool firstUpdate = true;
    // Start is called before the first frame update
    async void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");
        cloudSaveScript saver = camera.GetComponent<cloudSaveScript>();
        await saver.SomeAsyncMethod();
        
        TMP_Text[] scores = new TMP_Text[3];
        scores[0] = LeaderboardTextScore1;
        scores[1] = LeaderboardTextScore2;
        scores[2] = LeaderboardTextScore3;
        for (int i = 1; i <= 3; i++)
        {
            string data = await saver.loadSomeData(i + "stScore");
            if(int.Parse(data) > 0)
            {
                scores[i - 1].SetText(data);
                scores[i - 1].font = customFont;
                scores[i - 1].fontSize = 25;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(int score)
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            currLeaderboardScoreText.SetText(currTotalScore.ToString());
            currLeaderboardScoreText.font = customFont;
            currLeaderboardScoreText.fontSize = 25;
        }
        currTotalScore += score;
        updateScores(currTotalScore);
        currScoreText.SetText(currTotalScore.ToString());
        currLeaderboardScoreText.SetText(currTotalScore.ToString());
    }

    public void updateScores(int newScore)
    {
        TMP_Text[] LeaderboardScores = new TMP_Text[3];
        LeaderboardScores[0] = LeaderboardTextScore1;
        LeaderboardScores[1] = LeaderboardTextScore2;
        LeaderboardScores[2] = LeaderboardTextScore3;

        for (int i = 1; i <= 3; i++)
        {
            if(LeaderboardScores[i-1].text.Equals("-----"))
            {
                LeaderboardScores[i - 1].SetText(newScore + "");
                LeaderboardScores[i - 1].font = customFont;
                LeaderboardScores[i - 1].fontSize = 25;
                break;
            }
            else if (newScore > int.Parse(LeaderboardScores[i-1].text))
            {
                // Store the new score in the current position
                LeaderboardScores[i - 1].SetText(newScore + "");
                break;
            }
        }
    }

    public async void saveScoresToCloud()
    {
        GameObject camera = GameObject.Find("Main Camera");
        cloudSaveScript saver = camera.GetComponent<cloudSaveScript>();
        await saver.SomeAsyncMethod();
        int LeaderboardScore1 = 0;
        int LeaderboardScore2 = 0;
        int LeaderboardScore3 = 0;

        if (!LeaderboardTextScore1.text.Equals("-----"))
        {
            LeaderboardScore1 = int.Parse(LeaderboardTextScore1.text);
        }
        if (!LeaderboardTextScore2.text.Equals("-----"))
        {
            LeaderboardScore2 = int.Parse(LeaderboardTextScore2.text);
        }
        if (!LeaderboardTextScore3.text.Equals("-----"))
        {
            LeaderboardScore3 = int.Parse(LeaderboardTextScore3.text);
        }
        await saver.saveLeaderboardData(LeaderboardScore1, "1stScore");
        await saver.saveLeaderboardData(LeaderboardScore2, "2stScore");
        await saver.saveLeaderboardData(LeaderboardScore3, "3stScore");
    }

    public int getScore()
    {
        return this.currTotalScore;
    }
}