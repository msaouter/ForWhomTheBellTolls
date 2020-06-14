using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MouvementTrailer : MonoBehaviour
{
    public bool goToTarget = true;
    public Transform target;
    public float speed = 1;

    /* Up & down move */
    public bool upDownMove = true;
    public float maxY = 7;
    public float minY = 4f;
    public float speedUpDown = 3f;
    public float upAndDownSlowDown = 2.5f;
    private bool up = true;

    public bool leftRightMove = true;
    public VisualEffect visual;



    // Start is called before the first frame update
    void Start()
    {
        upDownMove = upDownMove && (minY < maxY);
    }

    // Update is called once per frame
    void Update()
    {
        if (goToTarget)
            GoToTarget();
        if (upDownMove)
            UpDownMove();
        if (leftRightMove)
            MoveOfVfx();

    }

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

    protected void MoveOfVfx()
    {
        visual.gameObject.transform.localPosition = visual.GetFloat("Corruption Amount") * new Vector3(Mathf.Sin(Time.time), 0, 0);
    }

    protected void GoToTarget()
    {
        this.gameObject.transform.LookAt(target);

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
