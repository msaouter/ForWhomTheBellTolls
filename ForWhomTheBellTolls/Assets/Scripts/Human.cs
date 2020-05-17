using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human : MonoBehaviour
{
    PlayerInput inputs;
    public static GameManager gameManager;
    //InputAction inputAction;
    //[SerializeField]
    //public Toll toll;


    /*void Start()
    {
        inputs.GetComponent<PlayerInput>();
        inputAction = inputs.currentActionMap["Toll"];
    }*/

    /*void Start()
    {
        inputs.GetComponent<PlayerInput>();

        inputs.currentActionMap["Toll"].performed += ctx => 
    }*/

    /* If button associated with the bell have been pressed, tells
     * the bell that it's actually ringing. Else, tells the bell
     * that it doesn't ring anymore.
     **/

    /*void Awake()
    {
        inputs = new PlayerInput();
    }*/

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    public void checkBells()
    {
        if (Gamepad.current == null)
        {
            Debug.LogError("Gamepad missing");
        }

        /* Remettre un Toll */

        /*dyson*/
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("Dyson");
            gameManager.registerBell(BellName.Dyson);

            /*if (toll.bellToToll.Contains(BellName.Dyson)){
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("dyson volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Dyson);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("dyson one");

            }*/

        }

        /*statue*/
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            Debug.Log("Statue");
            gameManager.registerBell(BellName.Statue);

            /*if (toll.bellToToll.Contains(BellName.Statue))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("statue volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Statue);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("statue one");

            }*/
        }
       
        /*stele*/
        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("Stele");
            gameManager.registerBell(BellName.Stele);
            
            /*if (toll.bellToToll.Contains(BellName.Stele))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("stele volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Stele);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("stele one");

            }*/
        }

        /*arch*/
        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            Debug.Log("Arch");
            gameManager.registerBell(BellName.Arch);
            
            /*if (toll.bellToToll.Contains(BellName.Arch))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("arch volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Arch);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("arch one");

            }*/
        }
        
        /*sundial*/
       if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            Debug.Log("Sundial");
            gameManager.registerBell(BellName.Sundial);
            
            /*if (toll.bellToToll.Contains(BellName.Sundial))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("sundial volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Sundial);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("sundial one");

            }*/
        }
        

        /*house*/
        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            Debug.Log("House");
            gameManager.registerBell(BellName.House);
            
            /*if (toll.bellToToll.Contains(BellName.House))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("house volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.House);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("house one");

            }*/
        }
    }

    void Update()
    {
        checkBells();
    }
}
