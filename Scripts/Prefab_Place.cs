using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab_Place : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;

    public Vector3 m_MyPosition;

    

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {

        //m_MyPosition.Set(0.02f, 0, 0.45f);
        myPrefab.SetActive(true);
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(myPrefab, m_MyPosition, Quaternion.identity);
    }
    
}
