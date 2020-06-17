using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoListener : MonoBehaviour
{
    public static GameManager gameManager;
    //public bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    /* 1 : contact
     * 0 : pas contact */
    void PrintStringParsing(string[] parsed)
    {
        for(int i = 0; i < parsed.Length; i++)
        {
            Debug.Log(parsed[i]);
        }
    }


    /* Bell order on data :
     * ARCH, STATUE, TEMPLE, SUNDIAL, DYSON, STELE
     * Actualise this if you change the order of irl bells
     */

    void Parser(string data)
    {
        string[] dataParsed = data.Split(','); //Data parsing

        //If there's a 1, it means a bell have been rang
        for(int i = 0; i < dataParsed.Length; i++)
        {
            if (dataParsed[i].Equals("0"))
            {
                /* remplacer cette instruction par register bells */
                //activated = true;
                
                /* register the right bell rang */
                switch (i)
                {
                    case 0: gameManager.registerBell(BellName.Arch);
                        break;
                    case 1: gameManager.registerBell(BellName.Statue);
                        break;
                    case 2: gameManager.registerBell(BellName.House);
                        break;
                    case 3: gameManager.registerBell(BellName.Sundial);
                        break;
                    case 4: gameManager.registerBell(BellName.Dyson);
                        break;
                    case 5: gameManager.registerBell(BellName.Stele);
                        break;
                }
            }

            /*else
            {
                //activated = false;
            }*/

        }
        
        
    }

    void OnMessageArrived(string msg)
    {
        Debug.Log("Arrived : " + msg);
        Parser(msg);
    }

    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Arduino connected successfully" : "Arduino disconnected");
    }
}
