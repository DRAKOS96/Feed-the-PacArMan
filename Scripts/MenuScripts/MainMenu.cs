using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject CanvasHowToPlay;
    public GameObject CanvasMainMenu;
    [SerializeField] AnimationClip title1;
    [SerializeField] AnimationClip title2;

    void Start()
    {
        CanvasMainMenu.SetActive(true);
        CanvasHowToPlay.SetActive(false);

        DontDestroyOnLoad(title1);
        DontDestroyOnLoad(title2);
    }

    public void PlayGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    
    }

    public void HowToPlay()
    {
        CanvasMainMenu.SetActive(false);
        CanvasHowToPlay.SetActive(true);
        
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void BackHowtoPlay()
    {
        CanvasMainMenu.SetActive(true);
        CanvasHowToPlay.SetActive(false);
    }

}
