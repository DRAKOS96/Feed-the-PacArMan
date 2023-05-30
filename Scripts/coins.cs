using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class coins : MonoBehaviour
{
    public static coins instance;
    public AudioClip coinSource;

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        transform.Rotate(0,0.5f,0);
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("CameraPlayer"))
        {
 
            AudioSource.PlayClipAtPoint(coinSource, transform.position);
            ScoreManager.instance.AddPoint();

            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator WaitForDisappear()
    {

        yield return new WaitForSeconds(1);
    }
}