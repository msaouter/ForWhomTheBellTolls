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
    public int nbSpirit = 5;

    public List<GameObject> spawnPoints;
    
    public List<GameObject> currentSpirits;
    
    //public GameObject parent;
    
    //public List<Bells> bells;

    /*[SerializeField]
    private int volleyLimit;*/

    private float timer = 0f;
    Human human;

    //Toll tolling;

    int rand = 0;
    int randPos = 0;

    Toll bellsTolled;

    //Human human;


    void generateRandomSpirit(int index)
    {
        /* Dire qu'un emplacement est déjà occupé pour ne pas spaw 2 esprits au même point */
        rand = Random.Range(0, spirits.Count);
        randPos = Random.Range(0, spawnPoints.Count);

        float x = spirits[index].transform.position.x + spawnPoints[randPos].transform.position.x;
        float y = spirits[index].transform.position.y + spawnPoints[randPos].transform.position.y;
        float z = spirits[index].transform.position.z + spawnPoints[randPos].transform.position.z;

        /*float x = spawnPoints[randPos].transform.position.x;
        float y = spawnPoints[randPos].transform.position.y;
        float z = spawnPoints[randPos].transform.position.z;*/

        currentSpirits[index] = Instantiate(spirits[rand], new Vector3(x, y, z), Quaternion.identity);

        currentSpirits[index].GetComponent<SpiritObject>().target = spawnPoints[randPos].transform;
        //currentSpirits[index].
        /*currentSpirits[index].transform.position = new Vector3();*/



    }


    // Start is called before the first frame update
    void Start()
    {
        /*human = new Human();*/

        //human.checkBells();

        if(nbSpirit <= 0)
        {
            nbSpirit = 1;
        }

        //tolledBells = new bool[6];

        List<BellName> bellNames = new List<BellName>();
        bellsTolled = new Toll(bellNames, TypesOfTolls.one);


        /*for(int i = 0; i < 6; i++)
        {
            //Debug.Log(i);
            tolledBells[i] = false;
        }*/

        for(int i = 0; i < nbSpirit; i++){
            rand = Random.Range(0, spirits.Count);
            //Debug.Log(spirits[rand].name);

            randPos = Random.Range(0, spawnPoints.Count);

            /*Debug.Log(rand);
            Debug.Log(randPos);*/

            float x = spirits[i].transform.position.x + spawnPoints[randPos].transform.position.x;
            float y = spirits[i].transform.position.y + spawnPoints[randPos].transform.position.y;
            float z = spirits[i].transform.position.z + spawnPoints[randPos].transform.position.z;

            currentSpirits.Add(Instantiate(spirits[rand], new Vector3(x, y, z), Quaternion.identity));

            currentSpirits[i].GetComponent<SpiritObject>().target = spawnPoints[randPos].transform;
        }

    }


    IEnumerator checkingSpirits()
    {
        for (int j = 0; j < currentSpirits.Count; j++)
        {
            /* If true, rights bells have been rang with right tempo so we set spirit target to one of the right bells */
            if (currentSpirits[j].GetComponent<SpiritObject>().TollBell(bellsTolled))
            {
                //currentSpirits[j].GetComponent<SpiritObject>().target = [une des bonnes cloches sonnées]
            }
            else
            {
                //currentSpirits[j].GetComponent<SpiritObject>().target = [un des spawn points]
            }


            if (currentSpirits[j].GetComponent<SpiritObject>().IsApaised())
            {
                Debug.Log("Spirit apaised");
                generateRandomSpirit(j);
            }
        }
        yield return new WaitForSeconds(5);
    }


    // Update is called once per frame
    void Update()
    {
        /* Rajouter coroutine */
        /*for(int i = 0; i < bells.Count; i++)
        {
            if (bells[i].tolled && !(bellsTolled.bellToToll.Contains(bells[i].bellName)))
            {
                bellsTolled.bellToToll.Add(bells[i].bellName);
                
                /* Conversion volley/one */
        /*if(bells[i].nbTimeTolled > 15)
        {
            bellsTolled.tolls = TypesOfTolls.volley;
        }
        else
        {
            bellsTolled.tolls = TypesOfTolls.one;
        }
    }
}*/

        //human.checkBells();

        human.checkBells(bellsTolled);

        StartCoroutine("checkingSpirits");

        


        /* Clear de la liste à chaque frame pour ne pas regarder les moves précédents */
        bellsTolled.bellToToll.Clear();

    }
}
