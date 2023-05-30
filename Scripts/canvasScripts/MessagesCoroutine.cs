using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesCoroutine : MonoBehaviour
{
    [SerializeField] private GameObject Message_Rdy;

    [SerializeField] private GameObject Message_GO;

    [SerializeField] private GameObject Message_2;

    [SerializeField] private GameObject Message_3;

    [SerializeField] private GameObject Message_1;

    public AudioClip StartUp;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;

        //Arxikopoihsh canvas - diactivated

        Message_Rdy.SetActive(false);
        Message_GO.SetActive(false);

        Message_3.SetActive(false);
        Message_2.SetActive(false);
        Message_1.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {


        
    }

    public IEnumerator Starting_Messages()
    {
        //Message Ready
        Message_Rdy.SetActive(true);
        yield return new WaitForSeconds(2);
        Message_Rdy.SetActive(false);
        yield return new WaitForSeconds(1);

        //Start Counting 3
        GetComponent<AudioSource>().clip = StartUp;
        GetComponent<AudioSource>().Play();
        Message_3.SetActive(true);
        yield return new WaitForSeconds(1);
        Message_3.SetActive(false);

        //Counting 2
        Message_2.SetActive(true);
        yield return new WaitForSeconds(1);
        Message_2.SetActive(false);

        //Counting 1
        Message_1.SetActive(true);
        yield return new WaitForSeconds(1);
        Message_1.SetActive(false);

        //Display GO
        Message_GO.SetActive(true);
        yield return new WaitForSeconds(1);
        Message_GO.SetActive(false);

        //Start the time and the AI enemies
         ScoreManager.instance.StartTime();
       
    }
}
    
