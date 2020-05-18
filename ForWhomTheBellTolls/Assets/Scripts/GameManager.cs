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

    int rand = 0;
    int randPos = 0;

    Toll intermediateList;
    Toll bellsTolled;

    private WaitForSeconds waitSeconds = new WaitForSeconds(5f);



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



    }


    // Start is called before the first frame update
    void Start()
    {
        if (nbSpirit <= 0)
        {
            nbSpirit = 1;
        }

        List<BellName> bellNames = new List<BellName>();

        intermediateList = new Toll(bellNames, TypesOfTolls.one);
        bellsTolled = new Toll(bellNames, TypesOfTolls.one);

        for(int i = 0; i < nbSpirit; i++){
            rand = Random.Range(0, spirits.Count);

            randPos = Random.Range(0, spawnPoints.Count);

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
        /* Add every input return to an intermediate list */
        intermediateList.bellToToll.Add(bellName);

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

        if (!bellsTolled.bellToToll.Contains(bellName))
        {
            bellsTolled.bellToToll.Add(bellName);
        }

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
}
