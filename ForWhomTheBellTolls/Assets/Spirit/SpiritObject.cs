using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class Toll
{
    public List<Bell> bellToToll;
    public TypesOfTolls tolls;

    public Toll(List<Bell> enu, TypesOfTolls i)
    {
        this.bellToToll = enu;
        this.tolls = i;
    }
}

public class SpiritObject : MonoBehaviour
{

    public SpiritScriptable spirit;
    private int currentMove = 0;
    

    public bool movesAutoLinkedToTheObject = false;
    public float rotateSpeed = 1;
    public float forwardSpeed = 1;
    public bool movesAutoLinkedToTheScene = false;
    public float rotation = 1f;
    public Vector3 direction = Vector3.forward;
    public bool selfRotationUp = false;
    public float selfRotationSpeedUp = 1;
    public bool selfRotationForward = false;
    public float selfRotationSpeedForward = 1;
    public float delayOnAttTargInSec = 1;
    private float delay;
    private List<Vector3> lastPosition;
    public Transform vfxPointer;

    public bool upDownMove = true;
    public float maxY = 2;
    public float minY = .5f;
    public float speedUpDown = .5f;
    public float upAndDownSlowDown = 0;
    private bool up = true;

    public float minDistanceOfTarget = 3;
    public float maxAngle = 45;
    public float speedChase = 1;
    public float rotationSpeed = 1;
    public float angleOfRotation = 40;
    public float slowDownInTurn = .5f;
    private bool left = true;
    private Vector3 startingPosition;
    private bool inChase = false;
    private bool crossed = true;
    public bool turn = true;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        delay = delayOnAttTargInSec;
        lastPosition = new List<Vector3>();
        upDownMove = upDownMove && (minY < maxY);
        startingPosition = this.gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (movesAutoLinkedToTheObject)
            MouveAutoLinkedToTheObject();
        else if (movesAutoLinkedToTheScene)
        {
            MovesAutoLinkedToTheScene();
            if (selfRotationUp)
                SelfRotationUp();
        }
        if (selfRotationForward)
            SelfRotationForward();
        DelayAttractivePropertyTarget();*/
        RandomMovesWithTarget(target.position);
        if (upDownMove)
            UpDownMove();
    }

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

    protected void UpDownMove()
    {
        float slow = 0;
        if (this.transform.position.y > minY && this.transform.position.y < maxY)
            slow = Mathf.Pow(((this.transform.position.y - (maxY + minY) / 2) / ((maxY - minY) / 2)), 2) * upAndDownSlowDown;
        else
            up = this.transform.position.y < maxY;

        if (up)
            this.gameObject.transform.position += new Vector3(0, (speedUpDown - slow)* Time.deltaTime, 0);
        else
            this.gameObject.transform.position -= new Vector3(0, (speedUpDown - slow) * Time.deltaTime, 0);
    }

    public bool IsApaised()
    {
        return currentMove > spirit.moves.Capacity;
    }

    public bool TollBell(Toll t)
    {
        if (spirit.moves[currentMove].bellToToll.Capacity == t.bellToToll.Capacity)
            foreach (Bell b in spirit.moves[currentMove].bellToToll)
            {
                if (!t.bellToToll.Contains(b))
                {
                    currentMove = 0;
                    return false;
                }
            }
        else
        {
            currentMove = 0;
            return false;
        }

        if (spirit.moves[currentMove].tolls == t.tolls)
        {
            ++currentMove;
            return true;
        }

        currentMove = 0;
        return false;
    }


    //testing variable
    public bool leftSide;
    /* Wip */
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
}
