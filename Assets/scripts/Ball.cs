using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float speed;
    public float destroyBullet;

    public Transform player;

    private void Start()
    {
        Invoke("DestroyBall", destroyBullet);

        player = GameObject.FindObjectOfType<Player>().transform;
    }


    private void Update()
    {
        shoot();
    }



    private void shoot()
    {
        if (player.transform.eulerAngles.y == 0)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
            Debug.Log("¿À¸¥ÂÊ");
        }
        else if (player.transform.eulerAngles.y == -180)
        {
            transform.Translate(-transform.right * -1 * speed * Time.deltaTime);
            Debug.Log("¿ÞÂÊ");
        }
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}
