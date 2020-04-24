using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bells : MonoBehaviour
{
    public BellName bellName;

    /*public List<string> bellsTolling;*/
    
    public bool toll;
    public Material changing;
    public Material defaultM;

    /*private float timer = 0;
    private bool timerReached = false;*/

    /* Needed function :
     * isTolling -> return True if used by a player
     * 
     */

    bool isTolling()
    {
        if (toll)
        {
            //Debug.Log("Toll");
            StartCoroutine(routine());
            return true;
        }
        //Debug.Log("Don't Toll");
        return false;
    }


    IEnumerator routine()
    {
        Debug.Log("Change material 1");
        gameObject.GetComponent<Renderer>().material = changing;

        yield return new WaitForSeconds(1);

        Debug.Log("Change material 2");
        gameObject.GetComponent<Renderer>().material = defaultM;

    }

    // Update is called once per frame
    void Update()
    {
        isTolling();
        
        /*if (isTolling())
        {

            if (!timerReached)
            {
                timer += Time.deltaTime;
            }

            if(!timerReached && timer > 5)
            {
                changeMaterial();
                timerReached = true;
            }
        }*/
  
    }
}
