using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     * Boucle de jeu :
     * 1. Apparition esprit
     * 2. Sonner une cloche
     *  3.a. si la cloche est bonne, commencer à apaiser l'esprit
     *  3.b. si la cloche n'est pas bonne, repartir de 0
     * 4. Esprit apaisé, on remet un nouvel esprit
     * */


    public List<GameObject> spirits;
    public List<GameObject> spawnPoints;
    public List<GameObject> currentSpirits;
    public GameObject parent;
    
    public List<Bells> bells;

    public int nbSpirit = 5;

    Toll tolling;

    bool[] tolledBells;

    int rand = 0;
    int randPos = 0;

    Toll bellsTolled;


    void generateRandomSpirit(int index)
    {
        rand = Random.Range(0, spirits.Count);
        randPos = Random.Range(0, spawnPoints.Count);

        float x = spawnPoints[randPos].transform.position.x;
        float y = spawnPoints[randPos].transform.position.y;
        float z = spawnPoints[randPos].transform.position.z;

        currentSpirits[index] = Instantiate(spirits[index], new Vector3(x, y, z), Quaternion.identity, parent.transform);
        //currentSpirits[index].
        /*currentSpirits[index].transform.position = new Vector3();*/

    }


    // Start is called before the first frame update
    void Start()
    {
        tolledBells = new bool[6];

        List<BellName> bellNames = new List<BellName>();
        bellsTolled = new Toll(bellNames, TypesOfTolls.one);


        for(int i = 0; i < 6; i++)
        {
            tolledBells[i] = false;
        }

        for(int i = 0; i < nbSpirit; i++){
            rand = Random.Range(0, spirits.Count);
            randPos = Random.Range(0, spawnPoints.Count);

            currentSpirits.Add(Instantiate(spirits[i], spawnPoints[randPos].transform, true));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i = 0; i < bells.Count; i++)
        {
            if (bells[i].tolled)
            {
                bellsTolled.bellToToll.Add(bells[i].bellName);
                
                /* Conversion volley/one */
                if(bells[i].nbTimeTolled > 2)
                {
                    bellsTolled.tolls = TypesOfTolls.volley;
                }
                else
                {
                    bellsTolled.tolls = TypesOfTolls.one;
                }
            }

            if(bellsTolled.bellToToll.Count == 0)
            {
                bellsTolled.bellToToll.Add(BellName.None);
                bellsTolled.tolls = TypesOfTolls.one;
            }
        }

        for(int j = 0; j < currentSpirits.Count; j++)
        {

            /* Val pour les moves de l'esprit à récup ici */
            //Debug.Log(bellsTolled.bellToToll[0]);

            /*if(!(bellsTolled.bellToToll[0] == BellName.None))
            {

            }*/
            /* If true, rights bells have been rang with right tempo so we set spirit target to one of the right bells */
            if (currentSpirits[j].GetComponent<SpiritObject>().TollBell(bellsTolled))
            {
                currentSpirits[j].GetComponent<SpiritObject>().target = bells[0].transform;
            }

            if (currentSpirits[j].GetComponent<SpiritObject>().IsApaised())
            {
                Debug.Log("Spirit apaised");
                generateRandomSpirit(j);
            }

        }


        /* Clear de la liste à chaque frame pour ne pas regarder les moves précédents */
        bellsTolled.bellToToll.Clear();

    }
}
