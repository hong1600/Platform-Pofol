using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject bossbullet;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Destroy(bossbullet);
        }

        else if (collision.transform.CompareTag("Player"))
        {
            Destroy(bossbullet);
        }
    }
}
