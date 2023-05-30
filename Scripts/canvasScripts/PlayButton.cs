using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    //public GameObject ButtonCanvas;
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0f;
    }

    void Awake()
    {
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        Time.timeScale = 1f;

        // Hides the button
        startButton.gameObject.SetActive(false);
    }

    //kanonika xekinaei apo to MessageCoroutine
    public void Play_Button_Press()
    {
  
        //Display the 3-2-1-GO
        StartCoroutine(GetComponent<MessagesCoroutine>().Starting_Messages());

  
    }

}
