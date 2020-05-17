using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

using Unity.Jobs;


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

    public float forwardSpeed = 1;
    public bool selfRotationForward = false;
    public float selfRotationSpeedForward = 1;

    public float delayOnAttTargInSec = 1;
    private float delay;
    private List<Vector3> lastPosition;
    public Transform vfxPointer;

    /* Up & down move */
    public bool upDownMove = true;
    public float maxY = 2;
    public float minY = .5f;
    public float speedUpDown = .5f;
    public float upAndDownSlowDown = 0;
    private bool up = true;

    public float minDistanceOfTarget = .5f;
    public float distanceMinToTargetFinal = 3;
    public float maxAngle = 45;
    public float speedChase = 6;
    public float turnSpeedChase = 1;
    public float rotationSpeed = 1;
    public float forwardTurnSpeed = 1;
    public float angleOfRotation = 40;
    public float slowDownInTurn = .5f;
    private Vector3 startingPosition;
    public bool inChase = true;
    //private bool crossed = true;
    public bool turn = true;
    public Transform target;
    private List<Vector3> targetList;
    private int indice = 0;
    //a changer en false en final
    public bool initialRotation = true;
    public float roundSpeed = 6;


    // Start is called before the first frame update
    void Start()
    {
        delay = delayOnAttTargInSec;
        lastPosition = new List<Vector3>();
        upDownMove = upDownMove && (minY < maxY);
        //startingPosition = this.gameObject.transform.position;
        targetList = TransitionnalPointAffectation(5, target.position, Vector3.Distance(this.gameObject.transform.position, target.position) / 3);
        /*
        GameObject gen;
        foreach (Vector3 v in targetList)
        {
            gen = new GameObject();
            gen.transform.position = v;
        }
        */
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
        }
        if (selfRotationForward)
            SelfRotationForward();
        DelayAttractivePropertyTarget();
        if (upDownMove)
            UpDownMove();

        if (inChase && distanceMinToTargetFinal < Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(target.position.x, target.position.z)))
        {
            if (turn)
            {
                turn = TurnToTarget(targetList[indice]);
            }
            else if (GoToTarget(targetList[indice]))
            {
                if (indice + 1 < targetList.Count) {
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
        } else if (initialRotation)
        {
            upDownMove = false;
            initialRotation = InitialRotation();
        } else
        {
            TurnAroudnTarget();
        }
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
    }

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
        Debug.Log("current Move : " + currentMove);
        Debug.Log("spirit.moves.Count : " + spirit.moves.Count);
        return currentMove >= spirit.moves.Count;
    }

    /* Controls :
 * A : Dyson
 * B : Statue
 * Y : Stele 
 * X : Arch
 * Left trigger : Sundial
 * Right trigger : House
 */

    public void checkList(Toll toll)
    {
        Debug.Log("Count : " + toll.bellToToll.Count);
        foreach(BellName b in toll.bellToToll)
        {
            Debug.Log(b);
        }
    }

    /* Check if all right bells have been tolled & if the current bells tolling are the right ones
     * 
     Returns :
     0 if wrong bell or not enough have been rang
     1 if right bell have been rang but not enough times (volley/one difference)
     2 if the right bell have been rang with the right type of toll 
     */
    public bool TollBell(Toll t)
    {
        //Debug.Log("currentMove : " + currentMove);
        if(currentMove >= 1)
        {
            timer += Time.deltaTime;
        }

        if (currentMove >= 1 && timer > spirit.moves[currentMove].timeInBetween)
        {
            currentMove = 0;
            //timer = 0;
            return false;
        }

        //checkList(t);

        
        Debug.Log(t.tolls);
        if (spirit.moves[currentMove].bellToToll.Count == t.bellToToll.Count)
            foreach (BellName b in spirit.moves[currentMove].bellToToll)
            {
                if (!t.bellToToll.Contains(b))
                {
                    Debug.Log("Doesn't contain right bell");
                    currentMove = 0;
                    //timer = 0;
                    return false;
                }
            }
        else
        {
            Debug.Log("Capacity !=");
            currentMove = 0;
            //timer = 0;
            return false;
        }

        if (spirit.moves[currentMove].tolls == t.tolls)
        {
            Debug.Log("Right move");
            ++currentMove;
            //timer = 0;
            return true;
        }

        Debug.Log("Wrong type of toll");
        currentMove = 0;
        //timer = 0;
        return false;
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
    public float angle;
    protected void Overturn (Vector3 target, bool left)
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
    }
    
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
        if (Vector2.Angle(new Vector2(this.transform.forward.x, this.transform.forward.z), new Vector2(target.position.x - this.transform.position.x, target.position.z - this.transform.position.z)) == 90)
            return true;

        this.transform.position += (this.transform.forward * forwardSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(-(target.position - transform.position).z, 0, (target.position - transform.position).x), turnSpeedChase * Time.deltaTime, 0.0f));


        return false;
    }

    void TurnAroudnTarget()
    {
        this.gameObject.transform.position += (this.transform.forward * roundSpeed * Time.deltaTime);
        this.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(-(target.position - transform.position).z, 0, (target.position - transform.position).x), 100, 0.0f));
    }

    public void SetTarget(Transform tar) {
        this.target = tar;
        targetList = TransitionnalPointAffectation(4, target.position, Vector3.Distance(this.gameObject.transform.position, target.position) / 3);
        inChase = true;
        initialRotation = true;
        indice = 0;
        turn = true;
    }

    ~SpiritObject()
    {

    }
}
