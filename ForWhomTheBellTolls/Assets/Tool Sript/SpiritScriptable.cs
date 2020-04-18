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

[System.Serializable]
public class NextMove
{
    public List<Bell> bellToToll;
    public float timeInBetween;

    public NextMove (List<Bell> enu, float i)
    {
        this.bellToToll = enu;
        this.timeInBetween = i;
    }
}

[CreateAssetMenu(fileName = "New Spirit", menuName = "Spirit/defalt")]
public class SpiritScriptable : ScriptableObject
{
    public string spiritName;
    public string description;
    public List<NextMove> moves;

}
