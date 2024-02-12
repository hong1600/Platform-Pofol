using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;

    Rigidbody2D rigid;
    public BoxCollider2D boxCollider;
    Vector2 movedir;

    public Color hitcolor;

    Animator ani;
    public Animator explosion;

    public float moveSpeed;

    public int maxHp;
    public int curHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attack = false;

    public bool ishit = false;

    public int nextMove;

    bool isLeft = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        enemyset();
    }

    private void enemyset()
    {
        maxHp = 4;
        curHp = 4;
        atkDmg = 1;
    }

    void Update()
    {
        moving();
    }


    private void moving()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void takedamage(int dam)
    {
        curHp -= dam;
        ishit = true;

        if (curHp <= 0)
        {
            SpawnManager._instance.enemyCount--;
            Destroy(gameObject);
            //Instantiate(explosion);
        }
        else 
        {
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.transform.CompareTag("Attack"))
        {
            takedamage(2);
            rigid.AddForce(Vector2.right, ForceMode2D.Impulse);
        }
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
    }
}
