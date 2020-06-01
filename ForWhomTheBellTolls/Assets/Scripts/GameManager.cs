using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
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

    public Camera camera;

    //private bool OnGame = true;
    //private int i = 0;

    private float timer = 0f;
    private float timerInput = 0f;

    [SerializeField]
    private float detectionTime = 2f;

    private bool inputDetected = false;
    
    [SerializeField]
    private float gameDuration = 480f;

    public List<GameObject> spirits;

    public List<Transform> spawnPoints;
    
    public List<SpiritObject> currentSpirits = new List<SpiritObject>();

    /* 0 Arche
     * 1 Statue
     * 2 Maison
     * 3 Cadran
     * 4 Dyson
     * 5 Stele
     */
    public List<GameObject> bells;

    Toll bellsTolled;

    private WaitForSeconds waitSeconds = new WaitForSeconds(5f);

    FMOD.Studio.EventInstance soundevent;
    [FMODUnity.EventRef, SerializeField]
    private string dysonRing;
    [FMODUnity.EventRef, SerializeField]
    private string steleRing;
    [FMODUnity.EventRef, SerializeField]
    private string statueRing;
    [FMODUnity.EventRef, SerializeField]
    private string sundialRing;
    [FMODUnity.EventRef, SerializeField]
    private string archRing;
    [FMODUnity.EventRef, SerializeField]
    private string houseRing;
    [FMODUnity.EventRef, SerializeField]
    private string apaisedSpirit;
    [FMODUnity.EventRef, SerializeField]
    private string newSpirit;



    // Start is called before the first frame update
    void Start()
    {
        List<BellName> bellNames = new List<BellName>();
        
        bellsTolled = new Toll(bellNames, TypesOfTolls.one);

        for (int i = 0; i < spirits.Count; i++) {
            if (i < spawnPoints.Count)
                currentSpirits.Add(Instantiate(spirits[i].gameObject, spawnPoints[i].position, Quaternion.identity).GetComponent<SpiritObject>());
            else
                currentSpirits.Add(Instantiate(spirits[i].gameObject, new Vector3(), Quaternion.identity).GetComponent<SpiritObject>());
        }
        /* Init sound */
        //soundevent = FMODUnity.RuntimeManager.CreateInstance(houseRing);

    //StartCoroutine(gameloop());

    }

    
    IEnumerator generateRandomSpirit(int index)
    {
        /* destroy the spirit apaised */
        Destroy(currentSpirits[index]);

        /* LIGNE COM SUIVANTE A IMPLEMENTER */
        /* Dire qu'un emplacement est déjà occupé pour ne pas spaw 2 esprits au même point */
        int rand = Random.Range(0, spirits.Count);
        int randPos = Random.Range(0, spawnPoints.Count);

        float x = spirits[index].transform.position.x + spawnPoints[randPos].transform.position.x;
        float y = spirits[index].transform.position.y + spawnPoints[randPos].transform.position.y;
        float z = spirits[index].transform.position.z + spawnPoints[randPos].transform.position.z;

        Instantiate(spirits[rand].gameObject, new Vector3(x, y, z), Quaternion.identity);

        currentSpirits[index].SetTargetInitial();

        yield return new WaitForSeconds(4f);
        RuntimeManager.PlayOneShot(newSpirit, currentSpirits[index].transform.position);
        yield return new WaitForSeconds(4f);
    }


    void checkingSpirits()
    {
        for (int j = 0; j < currentSpirits.Count; j++)
        {
            /* If true, rights bells have been rang with right tempo so we set spirit target to one of the right bells */
            if (currentSpirits[j].TollBell(bellsTolled))
            {
                currentSpirits[j].SetTargetRound(bells[BellNameToIndex(bellsTolled.bellToToll[0])].transform.position);
            }
            else
            {
                currentSpirits[j].SetTargetInitialRound(2);
            }


            if (currentSpirits[j].IsApaised())
            {
                Debug.Log("Spirit apaised");
                RuntimeManager.PlayOneShot(apaisedSpirit, currentSpirits[j].transform.position);
                currentSpirits[j].DoTheDance();
                currentSpirits.RemoveAt(j);
                j--;
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
        inputDetected = true;

        bellsTolled.tolls = TypesOfTolls.one;

        if (!bellsTolled.bellToToll.Contains(bellName))
        {
            bellsTolled.bellToToll.Add(bellName);
            ringBells(bellName);
        }
    }

    /* Check if gameDuration time have been spend in game */
    private bool isGameOver()
    {
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

    public void ringBells(BellName b)
    {
        /* remettre instructions pour sonner les cloches */
        switch (b)
        {
            case BellName.Dyson:
                //Debug.Log("Dyson distance : " + Vector3.Distance(bells.Find(x => x.name == "S_VBell_Sphere_LP").transform.position, camera.transform.position));
                RuntimeManager.PlayOneShot(dysonRing, bells.Find(x => x.name == "S_VBell_Sphere_LP").transform.position);
                //yield return new WaitForSeconds(1f);
                break;

            case BellName.Arch:
                //Debug.Log("Arch distance : " + Vector3.Distance(bells.Find(x => x.name == "S_VBell_Arch_LP_01").transform.position, camera.transform.position));
                RuntimeManager.PlayOneShot(archRing, bells.Find(x => x.name == "S_VBell_Arch_LP_01").transform.position);
                //yield return new WaitForSeconds(1f);
                break;

            case BellName.House:
                //Debug.Log("House distance : " + Vector3.Distance(bells.Find(x => x.name == "S_VBell_Temple").transform.position, camera.transform.position));
                RuntimeManager.PlayOneShot(houseRing, bells.Find(x => x.name == "S_VBell_Temple").transform.position);
                //yield return new WaitForSeconds(4f);
                break;

            case BellName.Statue:
                //Debug.Log("Statue distance : " + Vector3.Distance(bells.Find(x => x.name == "S_VBell_Statue_LP_02").transform.position, camera.transform.position));
                RuntimeManager.PlayOneShot(statueRing, bells.Find(x => x.name == "S_VBell_Statue_LP_02").transform.position);
                //yield return new WaitForSeconds(4f);
                break;

            case BellName.Stele:
                //Debug.Log("Statue distance : " + Vector3.Distance(bells.Find(x => x.name == "S_VBell_Stele_LP").transform.position, camera.transform.position));
                RuntimeManager.PlayOneShot(steleRing, bells.Find(x => x.name == "S_VBell_Stele_LP").transform.position);
                //yield return new WaitForSeconds(4f);
                break;

            case BellName.Sundial:
                //Debug.Log("Sundial distance : " + Vector3.Distance(bells.Find(x => x.name == "S_VBell_Sundial_LP_01").transform.position, camera.transform.position));
                //yield return new WaitForSeconds(4f);
                RuntimeManager.PlayOneShot(sundialRing, bells.Find(x => x.name == "S_VBell_Sundial_LP_01").transform.position);
                break;
        }
    }

    void Update()
    {

        /* Enlever coroutine */
        //while (!isGameOver()) {

            //Debug.Log(timerInput);

            if (inputDetected)
            {
                timerInput += Time.deltaTime;
            }

            if(timerInput >= detectionTime)
            {
                inputDetected = false;
                timerInput = 0f;
                checkingSpirits();
                
                /* Reset list after every check */
                bellsTolled.bellToToll.Clear();
                bellsTolled.tolls = TypesOfTolls.one;

            }
            /* Enfermer boucle dans un if timer == 2 et repasser ça en update ? */
        
        //}
        
        //yield return null;
    }

    private int BellNameToIndex(BellName name)
    {
        switch (name)
        {
            case BellName.Arch:
                return 0;
            case BellName.Statue:
                return 1;
            case BellName.House:
                return 2;
            case BellName.Sundial:
                return 3;
            case BellName.Dyson:
                return 4;
            case BellName.Stele:
                return 5;
        }
        return -1;
    }
}
