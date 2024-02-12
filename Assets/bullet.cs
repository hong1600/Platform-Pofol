using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;


    private void Start()
    {

        Invoke("destroybullet", 2);
    }

    private void Update()
    {
        bulletmove();


    }


    private void bulletmove()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void destroybullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            destroybullet();
        }
        if (collision.transform.CompareTag("Boss"))
        {
            destroybullet();
        }
    }
}
