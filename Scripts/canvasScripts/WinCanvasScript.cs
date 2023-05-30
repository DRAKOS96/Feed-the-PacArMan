using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvasScript : MonoBehaviour
{
    //Gia na exei syndesh me alla script
    public static WinCanvasScript instance;

    public AudioClip WinSound;
    
    public ScoreManager scores;
    public ScoreManager timeLEFT;

    public Text scorepoint2;
    public Text timeLeft;

    public Text total;
    public int  totalScore;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scorepoint2.text = "Your Score is = " + scores.score.ToString() + "Points";

        timeLeft.text = "Remaining time = " + (Mathf.RoundToInt(timeLEFT.currentTime)) + "sec";

        totalScore = scores.score + (Mathf.RoundToInt(timeLEFT.currentTime));
        total.text = "Final Score = " + totalScore.ToString() + "Points";

    }

    public void PlayWinMusic()
    {
        GetComponent<AudioSource>().clip = WinSound;
        GetComponent<AudioSource>().Play();
    }
}
