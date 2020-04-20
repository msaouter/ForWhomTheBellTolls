using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bell
{
    Dyson,
    Statue,
    Stele,
    Arch,
    Sundial,
    House
}

public enum TypesOfTolls
{
    one,
    volley
}

[System.Serializable]
public class NextMove
{
    public List<Bell> bellToToll;
    public TypesOfTolls tolls;
    public float timeInBetween;


    public NextMove (List<Bell> enu, float i)
    {
        this.bellToToll = enu;
        this.timeInBetween = i;
        this.tolls = TypesOfTolls.one;
    }

    public NextMove(List<Bell> enu, TypesOfTolls toll, float i)
    {
        this.bellToToll = enu;
        this.timeInBetween = i;
        this.tolls = toll;
    }
}


[CreateAssetMenu(fileName = "New Spirit", menuName = "Spirit/defalt")]
public class SpiritScriptable : ScriptableObject
{
    public string spiritName;
    public string description;
    public List<NextMove> moves;

}
