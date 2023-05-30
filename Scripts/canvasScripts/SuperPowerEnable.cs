using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperPowerEnable : MonoBehaviour
{

    public ScriptManager scriptManager;
    public Button SuperButton;
    public bool TimerSwitch = false;

    public AudioClip SuperPowerIsPressed;

    void Awake()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        Button btn = SuperButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //Timer start to countdown
        TimerSwitch = true;
        GetComponent<AudioSource>().clip = SuperPowerIsPressed;
        GetComponent<AudioSource>().Play();

        scriptManager.EnableSuperPower();


        StartCoroutine(WaitForDisappear2(1.5f));
        

    }
    public IEnumerator WaitForDisappear2(float secs)
    {
       
        yield return new WaitForSeconds(secs);
        this.gameObject.SetActive(false);
    }

}
