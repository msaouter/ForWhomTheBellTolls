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

    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    GameObject gameobj = new GameObject("GameManager");
                    _instance = gameobj.AddComponent<GameManager>();
                }
            }
            return _instance;

        }

    }

    private bool OnGame = true;
    private int i = 0;

    private float timer = 0f;
    
    [SerializeField]
    private float gameDuration = 480f;

    public List<GameObject> spirits;
    public int nbSpirit = 5;

    public List<GameObject> spawnPoints;
    
    public List<GameObject> currentSpirits;
    
    //public GameObject parent;
    
    //public List<Bells> bells;

    /*[SerializeField]
    private int volleyLimit;*/

    /*private float timer = 0f;
    Human human;*/

    //Toll tolling;

    int rand = 0;
    int randPos = 0;

    Toll intermediateList;
    Toll bellsTolled;

    private WaitForSeconds waitSeconds = new WaitForSeconds(5f);

    //Human human;


    void generateRandomSpirit(int index)
    {
        /* destroy the spirit apaised */
        Destroy(currentSpirits[index]);

        /* LIGNE COM SUIVANTE A IMPLEMENTER */
        /* Dire qu'un emplacement est déjà occupé pour ne pas spaw 2 esprits au même point */
        rand = Random.Range(0, spirits.Count);
        randPos = Random.Range(0, spawnPoints.Count);

        float x = spirits[index].transform.position.x + spawnPoints[randPos].transform.position.x;
        float y = spirits[index].transform.position.y + spawnPoints[randPos].transform.position.y;
        float z = spirits[index].transform.position.z + spawnPoints[randPos].transform.position.z;

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

        if (nbSpirit <= 0)
        {
            nbSpirit = 1;
        }

        //tolledBells = new bool[6];

        List<BellName> bellNames = new List<BellName>();

        intermediateList = new Toll(bellNames, TypesOfTolls.one);
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

        StartCoroutine(gameloop());

    }


    void checkingSpirits()
    {
        for (int j = 0; j < currentSpirits.Count; j++)
        {
            /* If true, rights bells have been rang with right tempo so we set spirit target to one of the right bells */
            if (currentSpirits[j].GetComponent<SpiritObject>().TollBell(bellsTolled))
            {
                //Debug.Log("Right bells rang");
                //currentSpirits[j].GetComponent<SpiritObject>().target = [une des bonnes cloches sonnées]
            }
            else
            {
                //Debug.Log("Wrong bells rang");
                //currentSpirits[j].GetComponent<SpiritObject>().target = [un des spawn points]
            }


            if (currentSpirits[j].GetComponent<SpiritObject>().IsApaised())
            {
                //Debug.Log("Spirit apaised");
                generateRandomSpirit(j);
            }
        }
        //yield return new WaitForSeconds(waitSeconds) /*null*/;
    }

    private int countItem(List<BellName> bellNames, BellName bellToCount)
    {
        int count = 0;

        foreach(BellName b in bellNames)
        {
            if(b == bellToCount)
            {
                count++;
            }
        }
        return count;
    }


    public void registerBell(BellName bellName)
    {
        //Debug.Log("COUCOU");
        //intermediateList.bellToToll.Clear();

        /* Add every input return to an intermediate list */
        intermediateList.bellToToll.Add(bellName);

        //checkList(intermediateList);
        //Debug.Log(intermediateList.bellToToll.Contains(bellName));

        /* Séparation volley/one à faire */
         /* si + 3 fois même cloche dans la liste -> volley
          * sinon one */

        if(countItem(intermediateList.bellToToll, bellName) > 3)
        {
            bellsTolled.tolls = TypesOfTolls.volley;
        }

        else
        {
            bellsTolled.tolls = TypesOfTolls.one;
        }

        //Debug.Log("Current bell already in bellsTolled : " + bellsTolled.bellToToll.Contains(bellName));
        //Debug.Log(bellName + " not in bellsTolled : " + !bellsTolled.bellToToll.Contains(bellName));

        if (!bellsTolled.bellToToll.Contains(bellName))
        {
            bellsTolled.bellToToll.Add(bellName);
            //Debug.Log(" BellsTolled list size " + bellsTolled.bellToToll.Count);
            //Debug.Log("Bell added : " + bellName);
            //Debug.Log("Bell registered");
        }

        /*if (countItem(intermediateList))
        {
            bellsTolled.tolls = TypesOfTolls.volley;
            //Debug.Log(bellsTolled.tolls);
        }*/
        /*else
        {
            bellsTolled.bellToToll.Add(bellName);
            bellsTolled.tolls = TypesOfTolls.one;
            /*Debug.Log(bellsTolled.bellToToll.Contains(bellName));
            Debug.Log(bellsTolled.tolls);
        }*/
    }


        /* Check if gameDuration time have been spend in game */
       private bool isGameOver()
    {
        //Debug.Log(timer);
        if(timer >= gameDuration)
        {
            return true;
        }

        timer += Time.deltaTime;
        return false;
    }

    public void checkList(Toll toll, string listName)
    {
        string listString = "";
        listString += listName + " ";
        listString += " Count : " + toll.bellToToll.Count + " ";
        foreach (BellName b in toll.bellToToll)
        {
            listString += b;
            listString += ", ";
        }

        Debug.Log(listString);
    }

    private IEnumerator gameloop()
    {
        while (!isGameOver()) {
            //Debug.Log("BellsTolled list : ");
            checkList(bellsTolled, "bellsTolled");
            checkingSpirits();

            /* Reset list after every check */
            bellsTolled.bellToToll.Clear();
            bellsTolled.tolls = TypesOfTolls.one;

            intermediateList.bellToToll.Clear();

            yield return waitSeconds;
        
        }
        
        yield return null;
    }

    // Update is called once per frame
    /*void Update()
    {*/
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
        /* Clear de la liste à chaque frame pour ne pas regarder les moves précédents */
        //bellsTolled.bellToToll.Clear();

    //}
}
