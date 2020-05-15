using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human : MonoBehaviour
{
    PlayerInput inputs;
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

    public void checkBells(Toll toll)
    {
        if (Gamepad.current == null)
        {
            Debug.LogError("Gamepad missing");
        }

        /* Remettre un Toll */

        /*dyson*/
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            if (toll.bellToToll.Contains(BellName.Dyson)){
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("dyson volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Dyson);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("dyson one");

            }
            /*bells[0].tolled = true;
            bells[0].nbTimeTolled += 1;*/
        }
        /*else
        {
            bells[0].tolled = false;
            bells[0].nbTimeTolled = 0;
        }*/

        //inputs.currentActionMap["Toll"].

        /*statue*/
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            if (toll.bellToToll.Contains(BellName.Statue))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("statue volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Statue);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("statue one");

            }
            /*bells[1].tolled = true;
            bells[1].nbTimeTolled += 1;*/
        }
        /*else
        {
            bells[1].tolled = false;
            bells[1].nbTimeTolled = 0;
        }*/

        /*stele*/
        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            if (toll.bellToToll.Contains(BellName.Stele))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("stele volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Stele);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("stele one");

            }
            
            /*bells[2].tolled = true;
            bells[2].nbTimeTolled += 1;*/
        }
        /*else
        {
            bells[2].tolled = false;
            bells[2].nbTimeTolled = 0;
        }*/

        /*arch*/
        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            if (toll.bellToToll.Contains(BellName.Arch))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("arch volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Arch);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("arch one");

            }
            /*bells[3].tolled = true;
            bells[3].nbTimeTolled += 1;*/
        }
        /*else
        {
            bells[3].tolled = false;
            bells[3].nbTimeTolled = 0;
        }*/

        /*sundial*/
       if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            if (toll.bellToToll.Contains(BellName.Sundial))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("sundial volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.Sundial);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("sundial one");

            }
            /*bells[4].tolled = true;
            bells[4].nbTimeTolled += 1;*/
        }
        /*else
        {
            bells[4].tolled = false;
            bells[4].nbTimeTolled = 0;
        }*/

        /*house*/
        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            if (toll.bellToToll.Contains(BellName.House))
            {
                toll.tolls = TypesOfTolls.volley;
                Debug.Log("house volley");
            }
            else
            {
                toll.bellToToll.Add(BellName.House);
                toll.tolls = TypesOfTolls.one;
                Debug.Log("house one");

            }
            /*bells[5].tolled = true;
            bells[5].nbTimeTolled += 1;*/
        }
        /*else
        {
            bells[5].tolled = false;
            bells[5].nbTimeTolled = 0;
        }*/
    }

    void Update()
    {
        //checkBells(be);
    }
}
