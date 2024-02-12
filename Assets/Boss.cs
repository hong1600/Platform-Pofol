using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject boss;
    public GameObject bullet;
    public GameObject player;

    public float cooltime;
    public float curtime;


    bool isLeft = true;

    public float movespeed;

    public float bulletspeed;

    public float bosshp = 20;

    private int pattern = 1;

    
    void Start()
    {
        
    }

    void Update()
    {
        move();
        attack();
        reload();
    }

    private void move()
    {
        boss.transform.Translate(Vector2.right * movespeed * Time.deltaTime);
    }




    private void attack()
    {
        if (curtime < cooltime)
            return;

        else if (curtime >= cooltime)
        {

            GameObject bulletL = Instantiate(bullet, transform.position + Vector3.left * 0.3f, transform.rotation);
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            rigidL.AddForce(dirL.normalized * bulletspeed, ForceMode2D.Impulse);

            GameObject bulletR = Instantiate(bullet, transform.position + Vector3.right * 0.3f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Vector3 dirR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            rigidR.AddForce(dirR.normalized * bulletspeed, ForceMode2D.Impulse);
            //    switch (pattern)
            //    {
            //        case 1:
            //            GameObject bulletL = Instantiate(bullet, transform.position + Vector3.left * 0.3f, transform.rotation);
            //            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            //            Vector3 dirL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            //            rigidL.AddForce(dirL.normalized * bulletspeed, ForceMode2D.Impulse);

            //            GameObject bulletR = Instantiate(bullet, transform.position + Vector3.right * 0.3f, transform.rotation);
            //            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            //            Vector3 dirR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            //            rigidR.AddForce(dirR.normalized * bulletspeed, ForceMode2D.Impulse);

            //            pattern++;

            //            curtime = 0;
            //            break;

            //        case 2:
            //            GameObject bulletr = Instantiate(bullet, transform.position + Vector3.right * 0.3f, transform.rotation);
            //            GameObject bulletrr = Instantiate(bullet, transform.position + Vector3.right * 0.45f, transform.rotation);
            //            GameObject bulletl = Instantiate(bullet, transform.position + Vector3.left * 0.3f, transform.rotation);
            //            GameObject bulletll = Instantiate(bullet, transform.position + Vector3.left * 0.45f, transform.rotation);

            //            Rigidbody2D rigidr = bullet.GetComponent<Rigidbody2D>();
            //            Rigidbody2D rigidrr = bullet.GetComponent<Rigidbody2D>();
            //            Rigidbody2D rigidl = bullet.GetComponent<Rigidbody2D>();
            //            Rigidbody2D rigidll = bullet.GetComponent<Rigidbody2D>();


            //            rigidr.AddForce(Vector2.down.normalized * 8, ForceMode2D.Impulse);
            //            rigidrr.AddForce(Vector2.down.normalized * 8, ForceMode2D.Impulse);
            //            rigidl.AddForce(Vector2.down.normalized * 8, ForceMode2D.Impulse);
            //            rigidll.AddForce(Vector2.down.normalized * 8, ForceMode2D.Impulse);

            //            bulletr.transform.Translate(-transform.up * bulletspeed * Time.deltaTime);
            //            bulletrr.transform.Translate(-transform.up * bulletspeed * Time.deltaTime);
            //            bulletl.transform.Translate(-transform.up * bulletspeed * Time.deltaTime);
            //            bulletll.transform.Translate(-transform.up * bulletspeed * Time.deltaTime);
            //            pattern = 1;

            //            curtime = 0;
            //            break;

            //    }
            //}
        }

            curtime = 0;
    }

    private void reload()
    {
        curtime += Time.deltaTime;
    }

    private void takedamage(int damage)
    {
        if (bosshp <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            bosshp -= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EndPoint"))
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }

        if (collision.transform.CompareTag("Attack"))
        {
            takedamage(2);
        }
    }
}
