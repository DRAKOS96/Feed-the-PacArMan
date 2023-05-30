using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    //Time Varialbles
    bool timerActive = false;
    public float currentTime;
    public int startMinutes;
    public Text currentTimeText;

    public GameObject GameOverCanvas;
    public GameObject ScoreCanvas;
    public GameObject WinCanvas;

    public WinCanvasScript winCanvas;
    public EndGameCanvas loseCanvas;

    //Gia na exei syndesh me alla script
    public static ScoreManager instance;

    public Text scoreText;
    public Text highText;
    

    //New Try
    public float scoreCount;
    public float HighScoreCount;
    

    public int score = 0;

    public WinCanvasScript FinalScore;
   

    // Start is called before the first frame update
    void Start()
    {
        //Time Varaliables
        currentTime = startMinutes * 60;

        GameOverCanvas.SetActive(false);

        //HighScore - Score Varaliables

        scoreText.text = "Score : " + score;
        HighScoreCount =  PlayerPrefs.GetFloat("Highscore");
       
 
    }

    void Update()
    {

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + " : " + time.Seconds.ToString() + " s";

        

        scoreText.text = "Score : " + score;
        highText.text = "Highscore : " + HighScoreCount;

        if(score > HighScoreCount)
        {
            HighScoreCount = score;
            PlayerPrefs.SetFloat("Highscore", HighScoreCount);
        }

        if (timerActive == true)
        {
            currentTime = currentTime - Time.deltaTime;

            //Not go on negative values
            if (currentTime <= 0)
            {
               
                StopTimer();
                loseCanvas.PlayLoseMusic();

            }

        }

        if(score == 15)
        {
          
            HighScoreCount = score + Mathf.Round(currentTime);

            PlayerPrefs.SetFloat("Highscore", HighScoreCount);
            print(HighScoreCount);
            WinLevel();
            winCanvas.PlayWinMusic();
        }

      
    }

    private void Awake()
    {
        instance = this;
    }

   
    //Starting when said go!
    public void StartTime()
    {
        timerActive = true;
    
    }

    
    public void StopTimer()
    {
        timerActive = false;
        GameOverCanvas.SetActive(true);
        ScoreCanvas.SetActive(false);
        Time.timeScale = 0f;

    }


    public void WinLevel()
    {
        timerActive = false;
        WinCanvas.SetActive(true);
        ScoreCanvas.SetActive(false);
        Time.timeScale = 0f;

    }



    public void AddPoint()
    {
 
     score += 1;
            
    }

}
