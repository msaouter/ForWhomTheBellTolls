using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameManager gm;
    public void LaGrandMere(bool b)
    {
        gm.spiritToSpawn[0] = b;
        Debug.Log(b);
    }
    public void LHiver(bool b)
    {
        gm.spiritToSpawn[1] = b;
    }
    public void Letoile(bool b)
    {
        gm.spiritToSpawn[2] = b;
    }
    public void LeMortHumain(bool b)
    {
        gm.spiritToSpawn[3] = b;
    }
    public void LeCimetiere(bool b)
    {
        gm.spiritToSpawn[4] = b;
    }
    public void LesHirondelles(bool b)
    {
        gm.spiritToSpawn[5] = b;
    }
    public void LaTable(bool b)
    {
        gm.spiritToSpawn[6] = b;
    }
    public void LeRoi(bool b)
    {
        gm.spiritToSpawn[7] = b;
    }
    public void LaDiviniteOubliee(bool b)
    {
        gm.spiritToSpawn[8] = b;
    }
    public void LaMusique(bool b)
    {
        gm.spiritToSpawn[9] = b;
    }

    public void playButon()
    {
        gm.TutorialStart();
    }
}
