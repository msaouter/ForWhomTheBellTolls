using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class SpiritObject : MonoBehaviour
{

    public SpiritScriptable spirit;
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


    // Start is called before the first frame update
    void Start()
    {
        delay = delayOnAttTargInSec;
        lastPosition = new List<Vector3>();
        upDownMove = upDownMove && (minY < maxY);

    }

    // Update is called once per frame
    void Update()
    {
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
        float slow = Mathf.Pow(((this.transform.position.y - (maxY + minY) / 2)/((maxY-minY)/2)),2) * upAndDownSlowDown;
        if (up && this.transform.position.y < maxY)
            this.gameObject.transform.position += new Vector3(0, (speedUpDown - slow)* Time.deltaTime, 0);
        else if (!up && this.transform.position.y > minY)
            this.gameObject.transform.position -= new Vector3(0, (speedUpDown - slow) * Time.deltaTime, 0);
        else
        {
            up = !up;
            UpDownMove();
        }
    }
}
