using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using FMODUnity;

using Unity.Jobs;


/* Supprimer ~SpiritObject en bas */

/* Controls :
 * A : Dyson
 * B : Statue
 * Y : Stele 
 * X : Arch
 * Left trigger : Sundial
 * Right trigger : House
 */

/* Needed function :
 * destructor -> Destruct the spirit
 * */

/* Bells to toll for this spirit */
public class Toll
{
    public List<BellName> bellToToll;
    public TypesOfTolls tolls;

    public Toll(List<BellName> enu, TypesOfTolls i)
    {
        this.bellToToll = enu;
        this.tolls = i;
    }
}

public class SpiritObject : MonoBehaviour
{
    /* Detection for right bell */
    public SpiritScriptable spirit;

    [SerializeField]
    int currentMove = 0;

    private float timer = 0f;

    /*public bool movesAutoLinkedToTheObject = false;
    public float rotateSpeed = 1;
    public bool movesAutoLinkedToTheScene = false;
    public float rotation = 1f;
    public Vector3 direction = Vector3.forward;
    public bool selfRotationUp = false;
    public float selfRotationSpeedUp = 1;
    */

    //public float forwardSpeed = 1;
    /*public bool selfRotationForward = false;
    public float selfRotationSpeedForward = 1;*/

    /* Delay of the attractive target for vfx */
    /*public float delayOnAttTargInSec = 1;
    private float delay;
    private List<Vector3> lastPosition;
    public Transform vfxPointer;*/

    /* Up & down move */
    public bool upDownMove = true;
    public float maxY = 2;
    public float minY = .5f;
    public float speedUpDown = .5f;
    public float upAndDownSlowDown = 0;
    private bool up = true;

    public float minDistanceOfTarget = .5f;
    public float distanceMinToTargetFinal = 3;
    public float speedChase = 3;
    public float turnSpeedChase = 1;
    public float forwardTurnSpeed = 1;
    //public float maxAngle = 45;
    //public float rotationSpeed = 1;
    //public float angleOfRotation = 40;
    //public float slowDownInTurn = .5f;
    //private bool crossed = true;
    private Vector3 startingPosition;
    public bool inChase = true;
    public bool turn = true;
    public Vector3 target;
    public List<Vector3> targetList;
    public int indice = 0;
    //a changer en false en final
    public bool initialRotation = true;
    public float roundSpeed = 40;
    public float distanceMinOfRotation = 4;
    public float distanceSwitchRotation = -.1f;
    public float distanceMinOfDance = 20;
    public VisualEffect visual;
    public GameObject wave;

    public bool doTheDance = false;

    [FMODUnity.EventRef, SerializeField]
    private string corruptSpirit;

    /*[FMODUnity.EventRef, SerializeField]
    private string recorruption;

    [FMODUnity.EventRef, SerializeField]
    private string waveSound;*/

    private FMOD.Studio.EventInstance instanceCorruptionSpirit;

    public int getCurrentMove()
    {
        return this.currentMove;
    }

    // Start is called before the first frame update
    void Start()
    {
        //delay = delayOnAttTargInSec;
        //lastPosition = new List<Vector3>();
        upDownMove = upDownMove && (minY < maxY);
        startingPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        this.transform.position += new Vector3(distanceMinOfRotation,0,0);
        SetTargetInitialRound();
        instanceCorruptionSpirit = RuntimeManager.CreateInstance(corruptSpirit);
        instanceCorruptionSpirit.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        UpdateCorruption();
        instanceCorruptionSpirit.start();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         if (movesAutoLinkedToTheObject)
            MouveAutoLinkedToTheObject();
        else if (movesAutoLinkedToTheScene)
        {
            MovesAutoLinkedToTheScene();
            if (selfRotationUp)
                SelfRotationUp();
        }*/
        MoveOfVfx();
        if (!doTheDance)
        {
            TimerManagement();

            /*
            if (selfRotationForward)
                SelfRotationForward();
            DelayAttractivePropertyTarget();
            */
            if (upDownMove)
                UpDownMove();

            RoundMovementManagement();
        } else
        {
            TurnAroudnCenter();
        }
    }

    protected void TimerManagement()
    {
        if (currentMove >= 1)
        {
            //timer += Time.deltaTime;
            if (timer > spirit.moves[currentMove].timeInBetween)
            {
                Debug.Log("Timer over");
                Frangipane();
                currentMove = 0;
                timer = 0;
                //resetFx ?
            }
        }
    }

    protected void MovementManagement()
    {
        if (inChase && distanceMinToTargetFinal < Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.x, target.z)))
        {
            if (turn)
            {
                turn = TurnToTarget(targetList[indice]);
            }
            else if (GoToTarget(targetList[indice]))
            {
                if (indice + 1 < targetList.Count)
                {
                    ++indice;
                    turn = TurnToTarget(targetList[indice]);
                }
                else
                {
                    inChase = false;
                    initialRotation = true;
                    indice = 0;
                }
            }
        }
        else if (initialRotation)
        {
            initialRotation = InitialRotation();
        }
        else
        {
            if (Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.x, target.z)) < distanceMinOfRotation)
            {
                this.transform.Translate(this.transform.forward * speedChase * Time.deltaTime);
                //this.transform.Translate((this.transform.position - target).normalized * speedChase * Time.deltaTime); 
            }
            TurnAroudnTarget();
        }
    }

    protected void MoveOfVfx()
    {
        visual.gameObject.transform.localPosition = visual.GetFloat("Corruption Amount") * new Vector3 (Mathf.Sin(Time.time),0,0);
    }
    /*
    protected void MouveAutoLinkedToTheObject()
    {
        this.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        this.transform.Translate(this.transform.forward*forwardSpeed*Time.deltaTime);
    }

    protected void MovesAutoLinkedToTheScene()
    {
        this.transform.position += new Vector3(direction.x * Mathf.Cos(rotation) - direction.z * Mathf.Sin(rotation), direction.y, direction.x * Mathf.Sin(rotation) + direction.z * Mathf.Cos(rotation)) * forwardSpeed * Time.deltaTime;
        rotation += rotateSpeed * Time.deltaTime;
    }

    protected void SelfRotationUp()
    {
        this.transform.Rotate(new Vector3(0, selfRotationSpeedUp * Time.deltaTime, 0));
    }
    */    
    
    
/*
    protected void SelfRotationForward()
    {
        this.transform.Rotate(new Vector3(0, 0, selfRotationSpeedForward * Time.deltaTime));
    }

    //Regler le retard property attractive tareget
    protected void DelayAttractivePropertyTarget()
    {
        lastPosition.Add(this.transform.position);
        if (delay < 0)
        {
            vfxPointer.position = lastPosition[0];
            lastPosition.RemoveAt(0);
        }
        else
            delay -= Time.deltaTime;
    }
    */

    /* Spirit move up & down */
    protected void UpDownMove()
    {
        float slow = 0;
        if (this.transform.position.y > minY && this.transform.position.y < maxY)
            slow = Mathf.Sqrt(Mathf.Abs((this.transform.position.y - (maxY + minY) / 2) / ((maxY - minY) / 2))) * upAndDownSlowDown;
        else
            up = this.transform.position.y < maxY;

        if (up)
            this.gameObject.transform.position += new Vector3(0, (speedUpDown - slow) * Time.deltaTime, 0);
        else
            this.gameObject.transform.position -= new Vector3(0, (speedUpDown - slow) * Time.deltaTime, 0);
    }


    public bool IsApaised()
    {
        return currentMove >= spirit.moves.Count;
    }

    /*
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

    public void checkSpiritMoves(List<BellName> spiritMoves, string listName)
    {
        string listString = "";
        listString += listName + " ";

        listString += " Count : " + spiritMoves.Count + " ";

        foreach (BellName b in spiritMoves)
        {
            listString += b;
            listString += ", ";
        }

        Debug.Log(listString);
    }
    */

    /* Check if all right bells have been tolled & if the current bells tolling are the right ones */
    public bool TollBell(Toll t)
    {
        if (spirit.moves[currentMove].bellToToll.Count == t.bellToToll.Count && spirit.moves[currentMove].tolls == t.tolls)
        {
            foreach (BellName b in spirit.moves[currentMove].bellToToll)
            {
                if (!t.bellToToll.Contains(b))
                {
                    Frangipane();
                    currentMove = 0;
                    UpdateCorruption();
                    //timer = 0;
                    return false;
                }
            }
            ++currentMove;
            UpdateCorruption();
            wave.SetActive(false);
            wave.SetActive(true);
            //RuntimeManager.PlayOneShot(waveSound, this.transform.position);
            //timer = 0;
            return true;
        }
        else
        {
            Frangipane();
            currentMove = 0;
            UpdateCorruption();
            //timer = 0;
            return false;
        }
    }

    void UpdateCorruption()
    {
        visual.SetFloat("Corruption Amount", 1 - (float)(currentMove) / spirit.moves.Capacity);
        instanceCorruptionSpirit.setParameterByName("Apaisement", (float)(currentMove) / spirit.moves.Capacity);
    }

    /*
    //testing variable
    public bool leftSide;
     Wip
    public void RandomMovesWithTarget (Vector3 target)
    {
        if (turn)
            Overturn(target, left);
        else
            if (Vector3.Distance(target, this.gameObject.transform.position) > minDistanceOfTarget)
            {
                leftSide = ((target.x == startingPosition.x) && ((target.y - startingPosition.y)<0 && this.gameObject.transform.position.x < startingPosition.x) || ((target.y - startingPosition.y) > 0 && this.gameObject.transform.position.x > startingPosition.x))
                    || ((target.x > startingPosition.x) && (this.gameObject.transform.position.y > (this.gameObject.transform.position.x * ((target.y - startingPosition.y) /(target.x - startingPosition.x)) +(target.y - target.x*((target.y - startingPosition.y) / (target.x - startingPosition.x)))))) 
                    || ((target.x < startingPosition.x) && (this.gameObject.transform.position.y < (this.gameObject.transform.position.x * ((target.y - startingPosition.y) / (target.x - startingPosition.x)) + (target.y - target.x * ((target.y - startingPosition.y) / (target.x - startingPosition.x))))));

                if (Random.Range(0,maxAngle) < Vector3.Angle(target - startingPosition, target - this.gameObject.transform.position) && ((left && !leftSide) || (!left && leftSide)))
                {
                    turn = true;
                    Overturn(target, left);
                } else
                {
                    this.gameObject.transform.position += this.gameObject.transform.forward * speedChase * Time.deltaTime;
                } 
                if (Vector3.Angle(target - startingPosition, target - this.gameObject.transform.position) > maxAngle)
                    left = !left;
            }
    }
    */

    /* turn left to turn around right bell */
    //public float angle;
    /*protected void Overturn (Vector3 target, bool left)
    {
        if (left)
            this.gameObject.transform.Rotate(this.gameObject.transform.up * rotationSpeed * Time.deltaTime);
        else
            this.gameObject.transform.Rotate(this.gameObject.transform.up * -rotationSpeed * Time.deltaTime);


        angle = Vector3.Angle(target - startingPosition, this.gameObject.transform.forward);
        this.gameObject.transform.position += this.gameObject.transform.forward * ((speedChase - Mathf.Abs((-(1/maxAngle)*angle + 1) * slowDownInTurn)) * Time.deltaTime);

        if (angle > angleOfRotation)
        {
            turn = false;
            left = !left;
        }
    }*/
    
    public List<Vector3> TransitionnalPointAffectation(int numberOfPoint, Vector3 target, float ecart)
    {
        List<Vector3> result = new List<Vector3>();
        float angle = Mathf.Deg2Rad * Vector2.SignedAngle(Vector2.up, new Vector2(target.x - this.gameObject.transform.position.x, target.z - this.gameObject.transform.position.z));
        float distance = Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.x, target.z));

        for (int i = 1; i < numberOfPoint; ++i)
        {
            result.Add(new Vector3(this.gameObject.transform.position.x + (Mathf.Pow(-1, i) * ecart / i * Mathf.Cos(angle) - i * distance / numberOfPoint * Mathf.Sin(angle)), this.gameObject.transform.position.y, this.gameObject.transform.position.z + (Mathf.Pow(-1, i) * ecart / i * Mathf.Sin(angle) + i * distance / numberOfPoint * Mathf.Cos(angle))));
        }

        result.Add(target);

        return result;
    }


    public float distanceMinToTarget = 1;
    public bool GoToTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedChase * Time.deltaTime);

        if (Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.x, target.z)) < distanceMinToTarget)
            return true;

        return false;
    }


    public float angleMinToTarget = 10;
    public bool TurnToTarget(Vector3 target)
    {
        transform.position += transform.forward * Time.deltaTime * forwardTurnSpeed /* * Mathf.Sqrt(Mathf.Abs((Vector2.Angle(new Vector2(target.x - this.gameObject.transform.position.x, target.z - this.gameObject.transform.position.z), new Vector2(this.gameObject.transform.forward.x, this.gameObject.transform.forward.z)) / 90 - 0.6f)))*/;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, target - transform.position, turnSpeedChase * Time.deltaTime, 0.0f));


        if (Vector2.Angle(new Vector2(target.x - this.gameObject.transform.position.x, target.z - this.gameObject.transform.position.z), new Vector2(this.gameObject.transform.forward.x, this.gameObject.transform.forward.z)) < angleMinToTarget)
            return false;

        return true;
    }

    protected bool InitialRotation()
    {
        if (Vector2.Angle(new Vector2(this.transform.forward.x, this.transform.forward.z), new Vector2(target.x - this.transform.position.x, target.z - this.transform.position.z)) == 90)
            return false;

        this.transform.position += (this.transform.forward * forwardTurnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(-(target - transform.position).z, 0, (target - transform.position).x), turnSpeedChase * Time.deltaTime, 0.0f));


        return true;
    }

    void TurnAroudnTarget()
    {
        transform.RotateAround(target,Vector3.up, roundSpeed * Time.deltaTime);
        //this.gameObject.transform.position += (this.transform.forward * roundSpeed * Time.deltaTime);
        this.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(-(target - transform.position).z, 0, (target - transform.position).x), 100, 0.0f));
    }

    public void SetTarget(Vector3 tar) {
        this.target = tar;
        targetList = TransitionnalPointAffectation(4, target, Vector3.Distance(this.gameObject.transform.position, target) / 3);
        inChase = true;
        initialRotation = true;
        indice = 0;
        turn = true;
    }

    public void SetTargetRound(Vector3 tar)
    {
        this.target = tar;
        targetList = RoundMoveIntermadiaryPoints(3, target);
        inChase = true;
        initialRotation = true;
        indice = 0;
    }

    public void SetTargetRound(Vector3 tar, int i)
    {
        this.target = tar;
        targetList = RoundMoveIntermadiaryPoints(i, target);
        inChase = true;
        initialRotation = true;
        indice = 0;
    }

    public void SetTargetInitial()
    {
        SetTarget(startingPosition);
    }

    public void SetTargetInitialRound()
    {
        SetTargetRound(startingPosition,0);
    }

    public void SetTargetInitialRound(int i)
    {
        SetTargetRound(startingPosition, i);
    }

    public void Frangipane()
    {
        if (currentMove != 0)
        {
            SetTargetInitialRound(2);
            //RuntimeManager.PlayOneShot(recorruption, this.transform.position);
        }
    }
    

    protected List<Vector3> RoundMoveIntermadiaryPoints(int numberOfPoint, Vector3 target)
    {
        List<Vector3> result = new List<Vector3>();

        for (int i = 1; i <= numberOfPoint; ++i)
        {
            result.Add(new Vector3((- this.gameObject.transform.position.x + target.x) * (2*i-1) / (numberOfPoint*2) + this.gameObject.transform.position.x, this.gameObject.transform.position.y, (- this.gameObject.transform.position.z + target.z) * (2 * i - 1) / (numberOfPoint * 2) + this.gameObject.transform.position.z));
        }
        result.Add(target);
        return result;
    }

    //public float bug;
    protected void RoundMovementManagement()
    {
        /*if (indice + 1 < targetList.Count)
        bug = Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(targetList[indice].x, targetList[indice].z))
                - Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(targetList[indice + 1].x, targetList[indice + 1].z));*/
        if (inChase && distanceMinToTargetFinal < Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.x, target.z)))
        {
            transform.RotateAround(targetList[indice], Vector3.up,Mathf.Pow(-1,indice) * roundSpeed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Mathf.Pow(-1, indice) * new Vector3(-(targetList[indice] - transform.position).z, 0, (targetList[indice] - transform.position).x), 100, 0.0f));
            if (indice + 1 < targetList.Count && Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(targetList[indice].x, targetList[indice].z))
                - Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(targetList[indice + 1].x, targetList[indice + 1].z))
                > distanceSwitchRotation)
            {
                ++indice;
                transform.RotateAround(targetList[indice], Vector3.up, Mathf.Pow(-1, indice) * roundSpeed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, Mathf.Pow(-1, indice) * new Vector3(-(targetList[indice] - transform.position).z, 0, (targetList[indice] - transform.position).x), 100, 0.0f));
                
            }
        }/*
        else if (initialRotation)
        {
            inChase = false;
            initialRotation = InitialRotation();
        }*/
        else
        {
            inChase = false;
            if (Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.x, target.z)) < distanceMinOfRotation)
            {
                this.transform.position += (new Vector3(this.gameObject.transform.position.x - target.x, 0, this.gameObject.transform.position.z - target.z).normalized * speedChase * Time.deltaTime);
                //this.transform.Translate((this.transform.position - target).normalized * speedChase * Time.deltaTime); 
            } else
            {
                TurnAroudnTarget();
            }
        }
    }

    public void DoTheDance()
    {
        doTheDance = true;
        target = new Vector3();
    }

    public float danceSpeed = 20;
    public void TurnAroudnCenter()
    {

        if (Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.x, target.z)) < distanceMinOfDance)
        {
            this.transform.position +=(new Vector3(this.gameObject.transform.position.x - target.x, 0 , this.gameObject.transform.position.z - target.z).normalized * speedChase * Time.deltaTime);
            
            //this.transform.Translate((this.transform.position - target).normalized * speedChase * Time.deltaTime); 
        }
        else
        {
            transform.RotateAround(target, Vector3.up, danceSpeed * Time.deltaTime);
            //this.gameObject.transform.position += (this.transform.forward * roundSpeed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(-(target - transform.position).z, 0, (target - transform.position).x), 100, 0.0f));
            
        }
    }

    public void playApaised()
    {
        visual.SendEvent("OnCalm");
    }


    ~SpiritObject()
    {

    }
}
