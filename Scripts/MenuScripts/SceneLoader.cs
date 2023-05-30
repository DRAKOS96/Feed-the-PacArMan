using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    public GameObject PauseCanvas;
    public GameObject Group;
    public GameObject resetHow;

    

    //public Button ResetHow;

    void Start()
    {
        PauseCanvas.SetActive(true);
        resetHow.SetActive (false);
        Group.SetActive(false);

      
    }

    public void BackToMenuButton()
    {
        
        //SceneManager.LoadScene("Menu");

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void RetryButton()
    {

        //SceneManager.LoadScene("Menu");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }


    public void PauseGame()
    {
        Group.SetActive(true);
        Time.timeScale = 0;
        
    }
    public void ResumeGame()
    {
        Group.SetActive(false);
        Time.timeScale = 1;
        
    }

    public void HowToPlay()
    {
        
        Group.SetActive(false);
        resetHow.SetActive(true);

    }

    public void ResetHowTo()
    {
        resetHow.SetActive(false);
        Group.SetActive(true);

    }

}
