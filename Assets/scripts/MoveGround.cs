using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public Transform starttrs;
    public Transform endtrs;
    public Transform trs;
    public float speed;

    void Start()
    {
        transform.position = starttrs.position;
        trs = endtrs;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, trs.position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, trs.position) <= 0.05f)
        {
            if(trs == endtrs) trs = starttrs;
            else trs = endtrs;
        }
    }
}
