using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierScript : MonoBehaviour
{

    [SerializeField] private Material BarriersMaterial;
    public AudioClip WarningSound;
    public EnemyPatroling EnemyPatroling;

    public GameObject GameOverCanvas;
    public GameObject ScoreCanvas;

    public AudioClip GameOverSound;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        BarriersMaterial.color = Color.white;
    }

    void OnTriggerEnter(Collider other)
    {
        //Change the colour of barrriers 
        if (other.CompareTag("CameraPlayer"))
        {
            print("Damage apo barrier , emeine:" + EnemyPatroling.currentHealth);

            GetComponent<AudioSource>().clip = WarningSound;
            GetComponent<AudioSource>().Play();
            Handheld.Vibrate();
            BarriersMaterial.color = Color.red;
            EnemyPatroling.TakeDamage(3);

        }

        if (EnemyPatroling.currentHealth <= 0)
        {

            print("Gameover from Barrier");

            // Game over
            GetComponent<AudioSource>().clip = GameOverSound;
            GetComponent<AudioSource>().Play();
            GameOverCanvas.SetActive(true);
            ScoreCanvas.SetActive(false);


            Time.timeScale = 0f;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CameraPlayer"))
        {
            BarriersMaterial.color = Color.white;
        }
    }

}
