using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bells : MonoBehaviour
{
    public BellName bellName;

    /*public List<string> bellsTolling;*/
    
    public bool toll;

    /* Needed function :
     * isTolling -> return True if used by a player
     * 
     */

    bool isTolling()
    {
        if (toll)
        {
            //Debug.Log("Toll");
            return true;
        }
        //Debug.Log("Don't Toll");
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        isTolling();
    }
}
