using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCanvas : MonoBehaviour
{
   //Gia na exei syndesh me alla script
    public static EndGameCanvas instance;

    public ScoreManager scores;

    public Text scorepoint;

    public AudioClip LoseSound;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

        scorepoint.text = "Your Score is = " + scores.score.ToString() + " Points";
    }

    public void PlayLoseMusic()
    {
        GetComponent<AudioSource>().clip = LoseSound;
        GetComponent<AudioSource>().Play();
    }
}
