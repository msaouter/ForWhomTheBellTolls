using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human : MonoBehaviour
{
    PlayerInput inputs;
    InputAction inputAction;
    [SerializeField]
    private List<Bells> bells;
    

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
    public void checkBells()
    {
        if (Gamepad.current == null)
        {
            Debug.LogError("Gamepad missing");
        }

        /*dyson*/
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            bells[0].tolled = true;
            bells[0].nbTimeTolled += 1;
            //Debug.Log("dyson");
        }
        else
        {
            bells[0].tolled = false;
            bells[0].nbTimeTolled = 0;
        }

        /*statue*/
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            bells[1].tolled = true;
            bells[1].nbTimeTolled += 1;
            //Debug.Log("statue");
        }
        else
        {
            bells[1].tolled = false;
            bells[1].nbTimeTolled = 0;
        }

        /*stele*/
        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            bells[2].tolled = true;
            bells[2].nbTimeTolled += 1;
            //Debug.Log("stele");
        }
        else
        {
            bells[2].tolled = false;
            bells[2].nbTimeTolled = 0;
        }

        /*arch*/
        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            bells[3].tolled = true;
            bells[3].nbTimeTolled += 1;
            //Debug.Log("arch");
        }
        else
        {
            bells[3].tolled = false;
            bells[3].nbTimeTolled = 0;
        }

        /*sundial*/
       if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            bells[4].tolled = true;
            bells[4].nbTimeTolled += 1;
            //Debug.Log("sundial");
        }
        else
        {
            bells[4].tolled = false;
            bells[4].nbTimeTolled = 0;
        }

        /*house*/
        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            bells[5].tolled = true;
            bells[5].nbTimeTolled += 1;
            //Debug.Log("house");
        }
        else
        {
            bells[5].tolled = false;
            bells[5].nbTimeTolled = 0;
        }
    }

    /*public void cheeckBells()
    {
        
    }*/

    void Update()
    {
        checkBells();
    }
}
