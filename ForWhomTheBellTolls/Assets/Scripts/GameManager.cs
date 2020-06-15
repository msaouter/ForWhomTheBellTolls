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

     /* Pour scale utiser le scale + strenth 
      * lightnig * 2
      * FarLightning
      * Particule de l'expension
      */

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



    //private bool OnGame = true;
    //private int i = 0;
    public bool inTutorial = true;

    private float timer = 0f;
    private float timerInput = 0f;

    [SerializeField]
    private float detectionTime = 2f;

    private bool inputDetected = false;
    
    [SerializeField]
    private float gameDuration = 480f;

    public List<GameObject> spirits;
    private List<SpiritObject> appaisedSpirits = new List<SpiritObject>();

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

    public GameObject CM_vcam1_Main;
    public GameObject CM_vcam1_Statue;
    public GameObject CM_vcam1_DysonSphere;
    public GameObject CM_vcam1_Sundial;
    public GameObject CM_vcam1_Arch;
    public GameObject CM_vcam1_Stele;
    public GameObject CM_vcam1_Temple;

    /* Grand mère
     * Hiver
     * Etoile
     * Mort Humain
     * Cimetiere
     * Hirondelles
     * Table
     * Roi
     * DiviniteOubliee
     * Musique
     */
    public bool[] spiritToSpawn;

    public float timeCameraTutorial = 3;
    private float tutotime = -1;
    private bool[] tutoBell;


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
    
    [FMODUnity.EventRef, SerializeField]
    private string layerSpirit;

    [FMODUnity.EventRef, SerializeField]
    private string Ouverture;

    [FMODUnity.EventRef, SerializeField]
    private string fermeture;

    [FMODUnity.EventRef, SerializeField]
    private string recorruption;

    [FMODUnity.EventRef, SerializeField]
    private string waveSound;



    private FMOD.Studio.EventInstance instanceLayerSpirit;
    
    public GameObject canva;

    // Start is called before the first frame update
    void Start()
    {
        instanceLayerSpirit = RuntimeManager.CreateInstance(layerSpirit);
        

        List<BellName> bellNames = new List<BellName>();
        
        bellsTolled = new Toll(bellNames, TypesOfTolls.one);

        tutoBell = new bool[6];

        spiritToSpawn = new bool[spirits.Count];
    }

    

    IEnumerator GenerateSpirit(float timeWated)
    {
        int i = 0;
        for (int IndexToSpawn = 0;IndexToSpawn < spirits.Count;++IndexToSpawn)
        {
            if (spiritToSpawn[IndexToSpawn]) { 
                yield return new WaitForSeconds(timeWated);
                /* LIGNE COM SUIVANTE A IMPLEMENTER */
                if (i < spawnPoints.Count)
                {
                    currentSpirits.Add(Instantiate(spirits[IndexToSpawn].gameObject, spawnPoints[i].position, Quaternion.identity).GetComponent<SpiritObject>());
                }
                else
                    currentSpirits.Add(Instantiate(spirits[IndexToSpawn].gameObject, new Vector3(), Quaternion.identity).GetComponent<SpiritObject>());

                RuntimeManager.PlayOneShot(newSpirit, currentSpirits[i].transform.position);
                ++i;
                instanceLayerSpirit.setParameterByName("Nombre Esprit", i);
            }
        }
        yield break;
    }

    void checkingSpirits()
    {
        int i = 0;
        for (int j = 0; j < currentSpirits.Count; j++)
        {
            i = currentSpirits[j].getCurrentMove();
            /* If true, rights bells have been rang with right tempo so we set spirit target to one of the right bells */
            if (currentSpirits[j].TollBell(bellsTolled))
            {
                currentSpirits[j].SetTargetRound(bells[BellNameToIndex(bellsTolled.bellToToll[0])].transform.position);
                RuntimeManager.PlayOneShot(waveSound, currentSpirits[j].transform.position);
                if (currentSpirits[j].IsApaised())
                {
                    Debug.Log("Spirit apaised");
                    RuntimeManager.PlayOneShot(apaisedSpirit, currentSpirits[j].transform.position);
                    
                    currentSpirits[j].playApaised();
                    currentSpirits[j].DoTheDance();
                    appaisedSpirits.Add(currentSpirits[j]);
                    currentSpirits.RemoveAt(j);
                    j--;
                }
            } else if(i != 0)
            {
                RuntimeManager.PlayOneShot(recorruption, currentSpirits[j].transform.position);
            }
            i = 0;
        }
        instanceLayerSpirit.setParameterByName("Nombre Esprit", currentSpirits.Count);
        if (currentSpirits.Count == 0)
            RuntimeManager.PlayOneShot(fermeture, this.transform.position);
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

    private void gameOver()
    {
        RuntimeManager.PlayOneShot(fermeture, this.transform.position);
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

        bells[BellNameToIndex(b)].GetComponentInChildren<ParticleSystem>().Play();
    }

    void Update()
    {
        if (inputDetected)
        {
            timerInput += Time.deltaTime;
        }

        if (tutotime>-1)
        {
            tutotime += Time.deltaTime;
            if (tutotime > timeCameraTutorial)
            {
                TutorialCinematic();
                tutotime = -1;
            }
        }

        if(timerInput >= detectionTime)
        {
            inputDetected = false;
            timerInput = 0f;
            if (inTutorial)
            {
                if (bellsTolled.bellToToll.Count != 1)
                {
                    TutorialCinematic();
                    bellsTolled.bellToToll.Clear();
                }
                else 
                { 
                    TutorialCinematic(bellsTolled.bellToToll[0]);
                    tutotime = 0;
                    tutoBell[BellNameToIndex(bellsTolled.bellToToll[0])] = true;
                    bellsTolled.bellToToll.Clear();
                    inTutorial = !(tutoBell[0] && tutoBell[1] && tutoBell[2] && tutoBell[3] && tutoBell[4] && tutoBell[5]);
                    if (!inTutorial)
                    {
                        //event fin de tutorial
                        StartGame();
                    }
                }
            } else
            {
                checkingSpirits();
                
                /* Reset list after every check */
                bellsTolled.bellToToll.Clear();
                bellsTolled.tolls = TypesOfTolls.one;
            }
        }

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
    

    
    private void TutorialCinematic(BellName name) {
        if (name == BellName.Dyson) // Dysonsphere
        {
            CM_vcam1_DysonSphere.SetActive(true);
            CM_vcam1_Main.SetActive(false);
            CM_vcam1_Statue.SetActive(false);
            CM_vcam1_Sundial.SetActive(false);
            CM_vcam1_Arch.SetActive(false);
            CM_vcam1_Stele.SetActive(false);
            CM_vcam1_Temple.SetActive(false);
            return;
        }

        if (name == BellName.Statue) // Statue
        {
            CM_vcam1_DysonSphere.SetActive(false);
            CM_vcam1_Main.SetActive(false);
            CM_vcam1_Statue.SetActive(true);
            CM_vcam1_Sundial.SetActive(false);
            CM_vcam1_Arch.SetActive(false);
            CM_vcam1_Stele.SetActive(false);
            CM_vcam1_Temple.SetActive(false);
            return;
        }

        if (name == BellName.Stele) // Stele
        {
            CM_vcam1_DysonSphere.SetActive(false);
            CM_vcam1_Main.SetActive(false);
            CM_vcam1_Statue.SetActive(false);
            CM_vcam1_Sundial.SetActive(false);
            CM_vcam1_Arch.SetActive(false);
            CM_vcam1_Stele.SetActive(true);
            CM_vcam1_Temple.SetActive(false);
            return;
        }

        if (name == BellName.House) // Temple
        {
            CM_vcam1_DysonSphere.SetActive(false);
            CM_vcam1_Main.SetActive(false);
            CM_vcam1_Statue.SetActive(false);
            CM_vcam1_Sundial.SetActive(false);
            CM_vcam1_Arch.SetActive(false);
            CM_vcam1_Stele.SetActive(false);
            CM_vcam1_Temple.SetActive(true);
            return;
        }

        if (name == BellName.Arch) // Arch
        {
            CM_vcam1_DysonSphere.SetActive(false);
            CM_vcam1_Main.SetActive(false);
            CM_vcam1_Statue.SetActive(false);
            CM_vcam1_Sundial.SetActive(false);
            CM_vcam1_Arch.SetActive(true);
            CM_vcam1_Stele.SetActive(false);
            CM_vcam1_Temple.SetActive(false);
            return;
        }

        if (name == BellName.Sundial) // Sundial
        {
            CM_vcam1_DysonSphere.SetActive(false);
            CM_vcam1_Main.SetActive(false);
            CM_vcam1_Statue.SetActive(false);
            CM_vcam1_Sundial.SetActive(true);
            CM_vcam1_Arch.SetActive(false);
            CM_vcam1_Stele.SetActive(false);
            CM_vcam1_Temple.SetActive(false);
            return;
        }
    }

    private void TutorialCinematic()
    {
        CM_vcam1_DysonSphere.SetActive(false);
        CM_vcam1_Main.SetActive(true);
        CM_vcam1_Statue.SetActive(false);
        CM_vcam1_Sundial.SetActive(false);
        CM_vcam1_Arch.SetActive(false);
        CM_vcam1_Stele.SetActive(false);
        CM_vcam1_Temple.SetActive(false);
    }

    public void TutorialStart()
    {
        tutotime = -1;
        for (int i = 0; i < tutoBell.Length; ++i)
            tutoBell[i] = false;
        inTutorial = true;
        canva.SetActive(false);
        RuntimeManager.PlayOneShot(Ouverture, this.transform.position);
    }

    public void Restart()
    {
        Debug.Log("RestartGame");
        RuntimeManager.MuteAllEvents(true);
        foreach (SpiritObject s in currentSpirits)
            Destroy(s.gameObject);
        foreach (SpiritObject s in appaisedSpirits)
            Destroy(s.gameObject);

        currentSpirits.Clear();
        appaisedSpirits.Clear();
        timer = 0f;
        timerInput = 0f;
        canva.SetActive(true);
    }


    public float timeBeteweenSpawn = 2f;
    private void StartGame()
    {
        Debug.Log("StartGame");
        instanceLayerSpirit.setParameterByName("Nombre Esprit",1);
        instanceLayerSpirit.start();
        
        StartCoroutine(GenerateSpirit(timeBeteweenSpawn));
    }
}
