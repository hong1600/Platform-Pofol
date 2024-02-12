using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform PlayerTr;
    private Transform camTr;

    [Range(2.0f, 100.0f)]
    public float distance = 10.0f;

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        camTr = GetComponent<Transform>();
    }

    private void Update()
    {
        camTr.position = PlayerTr.position + (-PlayerTr.forward * distance); 
    }
}
