using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    //Gia na exei syndesh me alla script
    public static ScriptManager instance;
    public int check = 1;

    //public GameObject TurtleShell;
    //public GameObject CameraPlayer;

    public EnemyPatroling EnemyPatroling;
    public exthros exthosScript;

    // Start is called before the first frame update
    void Start()
    {
        
   
    }

     public void EnableSuperPower()
    {
       // print("enable super power from ScriptManager");
        check = 2;

    }

  
    public void ReturnToNormal()
    {

        check = 1;
        
        EnemyPatroling.checkEnemy = 1;
        exthosScript.checkEnemy = 1;

    }

}
