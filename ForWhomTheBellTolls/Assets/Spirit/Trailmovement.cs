using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailmovement : MonoBehaviour
{
    public bool selfRotationForward = false;
    public float selfRotationSpeedForward = 1;

    public bool upDownMove = false;
    public float maxY = 2;
    public float minY = .5f;
    public float speedUpDown = .5f;
    public float upAndDownSlowDown = 0;
    private bool up = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selfRotationForward)
            SelfRotationForward();
        if (upDownMove)
            UpDownMove();
    }

    protected void SelfRotationForward()
    {
        this.transform.Rotate(new Vector3(0, 0, selfRotationSpeedForward * Time.deltaTime));
    }

    protected void UpDownMove()
    {
        float slow = 0;
        if (this.transform.position.y > minY && this.transform.position.y < maxY)
            slow = Mathf.Pow(((this.transform.position.y - (maxY + minY) / 2) / ((maxY - minY) / 2)), 2) * upAndDownSlowDown;
        else
            up = this.transform.position.y < maxY;

        if (up)
            this.gameObject.transform.position += new Vector3(0, (speedUpDown - slow) * Time.deltaTime, 0);
        else
            this.gameObject.transform.position -= new Vector3(0, (speedUpDown - slow) * Time.deltaTime, 0);
    }
}
