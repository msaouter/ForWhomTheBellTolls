using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SpiritGenerator : ScriptableObject
{
    
    public bool Generate(string spiritFileName, bool addInScene, int defaltTime)
    {
        Debug.Log("Generating");
        try
        {
            TextAsset csvFile = Resources.Load<TextAsset>(spiritFileName);
            string[] data = csvFile.text.Split('\n');

            GameObject prefab = Resources.Load<GameObject>("Prefab/A_Spirit");
            for (int i = 1; i < data.Length; ++i)
            {
                string[] spiritText = data[i].Split(',');
                SpiritScriptable asset = ScriptableObject.CreateInstance<SpiritScriptable>();
                asset.spiritName = spiritText[0];
                asset.description = spiritText[1];
                

                if (!LinkspiritToAttributsValue(ref asset.moves, spiritText, 2, defaltTime))
                    Debug.Log("Error on generating the spirit " + spiritText[0]);
                
                AssetDatabase.CreateAsset(asset, "Assets/Spirit/Generated/" + spiritText[0] + ".asset");
                
                if (addInScene)
                {
                    
                    GameObject gen = Instantiate<GameObject>(prefab);
                    gen.GetComponent<SpiritObject>().spirit = asset;
                    gen.name = spiritText[0];
                    
                }
            }

            AssetDatabase.SaveAssets();

            return true;
        }
        catch
        {
            Debug.Log("Error on generating the file (probably not able to access " + spiritFileName + ")");
            //Debug.Log();
            return false;
        }

    }


    /* Arche Statue Maison Cadran Dyson Stele */
    public bool LinkspiritToAttributsValue(ref List<NextMove> attributValue, string[] s, int offset, int defaltTime)
    {
        try
        {
            attributValue = new List<NextMove>();
            List<int> arc = StringToValue(s[offset]);
            List<int> sta = StringToValue(s[offset + 1]);
            List<int> hou = StringToValue(s[offset + 2]);
            List<int> sun = StringToValue(s[offset + 3]);
            List<int> dys = StringToValue(s[offset + 4]);
            List<int> ste = StringToValue(s[offset + 5]);
            string[] time = s[offset + 6].Split(';');
            string[] tol = s[offset + 7].Split(';');
            int i = 1;
            List<Bell> toToll;
            bool conti = true;
            while (conti)
            {
                toToll = new List<Bell>();
                if (arc.Contains(i))
                    toToll.Add(Bell.Arch);
                if (sta.Contains(i))
                    toToll.Add(Bell.Statue);
                if (hou.Contains(i))
                    toToll.Add(Bell.House);
                if (sun.Contains(i))
                    toToll.Add(Bell.Sundial);
                if (dys.Contains(i))
                    toToll.Add(Bell.Dyson);
                if (ste.Contains(i))
                    toToll.Add(Bell.Stele);
                Debug.Log(toToll.Count);
                if(toToll.Count != 0)
                {
                    attributValue.Add(new NextMove(toToll, toll(tol,i), timeFor(time, i, defaltTime))) ;
                    ++i;
                } else
                {
                    conti = false;
                }
                    
            } 

            return true;
        }
        catch
        {
            Debug.Log("Error on Link");
            return false;
        }
    }

    public List<int> StringToValue(string s)
    {
        List<int> res = new List<int> ();
        string[] vs = s.Split(';');

        try
        {
            for (int i = 0; i < vs.Length; ++i)
            {
                if (!(vs == null || vs[i] == "" || vs[i] == null))
                {

                    res.Add(int.Parse(vs[i]));
                }
            }
        }
        catch
        {
            Debug.Log("Error on StringToValue");
            Debug.Log(s);
        }
        return res;
    }

    private int timeFor(string[] s, int i, int defaltTime)
    {
        for(int j = 0; j < s.Length; j+=2)
        {
            if (s[j]!= "" && stringToInt(s[j]) == i)
                return (stringToInt(s[j + 1]));
        }
        return defaltTime;
    }

    private TypesOfTolls toll (string[] s, int i)
    {
        try
        {
            for (int j = 0; j < s.Length; ++j)
            {
                if (s[j] != null && s[j] != "" && stringToInt(s[j]) == i)
                    switch (s[j + 1])
                    {
                        case ("o"):
                            return TypesOfTolls.one;

                        case ("v"):
                            return TypesOfTolls.volley;

                        default:
                            return TypesOfTolls.one;
                    }
            }
        }
        catch
        {
            Debug.Log("Error on Toll");
        }
        return TypesOfTolls.one;
    }

    private int stringToInt (string s)
    {
        try
        {
            return int.Parse(s);
        } catch
        {
            return -1;
        }
    }
}
