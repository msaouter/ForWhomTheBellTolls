using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human : MonoBehaviour
{

    public static GameManager gameManager;

    /* If button associated with the bell have been pressed, register it on the buffer
     **/

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
            gameManager.registerBell(BellName.Dyson);

        }

        /*statue*/
        if (Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            gameManager.registerBell(BellName.Statue);
        }
       
        /*stele*/
        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            gameManager.registerBell(BellName.Stele);
        }

        /*arch*/
        if (Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            gameManager.registerBell(BellName.Arch);
        }
        
        /*sundial*/
       if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            gameManager.registerBell(BellName.Sundial);
        }
        

        /*house*/
        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            gameManager.registerBell(BellName.House);
        }

        /* Reset la partie */
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            gameManager.Restart();
        }
    }

    void Update()
    {
        checkBells();
    }
}
