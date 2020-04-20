using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SpiritGenerator : ScriptableObject
{
    public bool Generate(string spiritFileName, bool addInScene)
    {
        Debug.Log("Generating");
        try
        {
            TextAsset csvFile = Resources.Load<TextAsset>(spiritFileName);
            string[] data = csvFile.text.Split('\n');
            
            GameObject prefab = Resources.Load<GameObject>("Prefab/Fatespirit");
            for (int i = 1; i < data.Length; ++i)
            {
                string[] spiritText = data[i].Split(',');


                SpiritScriptable asset = ScriptableObject.CreateInstance<SpiritScriptable>();
                asset.spiritName = spiritText[0];
                asset.description = spiritText[1];
                /*
                if (i == 1)
                {
                    asset.artwork = Resources.Load<Sprite>("ImageTarot/Jean_Dodal_Tarot_trump_Fool");
                }
                else
                {
                    asset.artwork = Resources.Load<Sprite>("ImageTarot/Jean_Dodal_Tarot_trump_" + (i - 1) / 10 + (i - 1) % 10);
                }

                if (!LinkspiritToAttributsValue(ref asset.attributValue, StringToValue(spiritText, 2)))
                    Debug.Log("Error on generating the spirit " + spiritText[0]);

                AssetDatabase.CreateAsset(asset, "Assets/spirit/Fate/Generated/" + spiritText[0] + ".asset");
                if (addInScene)
                {
                    GameObject gen = Instantiate<GameObject>(prefab);
                    gen.GetComponent<FatespiritObject>().fate = asset;
                    gen.name = spiritText[0];
                }*/
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


    /* Chaos - Creation - Eloquence - Knoledge - Logique - Résilience - Secret - Silence - Violence */
    public bool LinkspiritToAttributsValue(ref List<NextMove> attributValue, int[] atts)
    {
        try
        {
            attributValue = new List<NextMove>();
            attributValue.Add(new AttributValue(Attribut.Chaos, atts[0]));
            attributValue.Add(new AttributValue(Attribut.Creation, atts[1]));
            attributValue.Add(new AttributValue(Attribut.Eloquence, atts[2]));
            attributValue.Add(new AttributValue(Attribut.Knowledge, atts[3]));
            attributValue.Add(new AttributValue(Attribut.Logic, atts[4]));
            attributValue.Add(new AttributValue(Attribut.Resilience, atts[5]));
            attributValue.Add(new AttributValue(Attribut.Secret, atts[6]));
            attributValue.Add(new AttributValue(Attribut.Silence, atts[7]));
            attributValue.Add(new AttributValue(Attribut.Violence, atts[8]));
            return true;
        }
        catch
        {
            Debug.Log("Error on Link");
            return false;
        }
    }

    public int[] StringToValue(string[] s, int ofset)
    {
        int[] res = new int[9];
        try
        {
            for (int i = 0; i < res.Length; ++i)
            {
                if (s[i + ofset] == "" || s[i + ofset] == null)
                {
                    res[i] = 0;
                }
                else
                {
                    res[i] = int.Parse(s[i + ofset]);
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
}
