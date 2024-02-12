using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject objplayer;
    public Gamemanager manager;
    public GameObject enemy;
    public GameObject bossbullet;

    CircleCollider2D circle;
    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    Vector3 movedir;
    Animator ani;
    SpriteRenderer sr;
    public SpriteRenderer playersr;
    public TextMeshProUGUI hptext;
    public TextMeshProUGUI damagetext;

    [SerializeField] int movespeed;

    [SerializeField] float jumpForce;

    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGround;

    [SerializeField] float fallGravity;
    Vector2 vecGravity;

    bool doubleJump;
    [SerializeField] float doublejumppower;

    bool playerlookright;

    [SerializeField] float teleport;
    ParticleSystem effect;
    public GameObject teleEffect;

    public int maxHp;
    public int curHp;
    public int atkDmg;
    public float atkSpeed = 1;
    bool attack;
    float attackDelay;
    bool attackReady;
    



    private void Awake()
    {
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        hptext = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        playerset();

    }

    public void playerset()
    {
        maxHp = 10;
        curHp = 10;
        atkDmg = 2;
    }

    private void Update()
    {
        moving();
        turning();
        jumping();

        animation();

        groundchecked();
        gravityCheck();

        tele();

        playerlook();

        shootbullet();

        //playerstatus();
    }

    
    private void moving()
    {
        float hor = Input.GetAxis("Horizontal");

        movedir.x = hor * movespeed;
        movedir.y = rigid.velocity.y;
        rigid.velocity = movedir;

        //if (hor > 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //}
        //else if (hor < 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}

    }

    private void turning()
    {
        if (transform.localScale.x > -1 && movedir.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (transform.localScale.x < 1 && movedir.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
    private new void animation()
    {
        ani.SetInteger("Horizontal", (int)movedir.x);
    }

    private void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundchecked())
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                doubleJump = false;
            }
        }
    }

    bool groundchecked()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1, 0.28f),
                   CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void gravityCheck()
    {
        if (rigid.velocity.y < 0)
        {
            rigid.velocity -= vecGravity * fallGravity * Time.deltaTime;
        }
    }

    private void tele()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if (playerlookright == true)
            {
                objplayer.transform.Translate(Vector2.right * teleport);
                Instantiate(teleEffect, gameObject.transform);
            }
            else if (playerlookright == false)
            {
                objplayer.transform.Translate(Vector2.left * teleport);
                Instantiate(teleEffect, gameObject.transform);
            }
        }
    }

    public void playerlook()
    {
        if (gameObject.transform.localScale.x > 0)
        {
            playerlookright = false;
        }
        else if (gameObject.transform.localScale.x < 0)
        {
            playerlookright = true;
        }
    }

    bool ishit = false;
    bool isknockback = false;
    public float knockbackspeed;

    Color color1 = new Color(1, 0, 0, 0.5f);
    Color color2 = new Color(1, 1, 1, 1f);
    public void takedamage(int dam, Vector2 pos)
    {
        if (ishit == false)
        {
            ishit = true;
            curHp -= dam;

            if (curHp <= 0)
            {
                gameObject.SetActive(false);
                manager.respawnplayer();
            }
            else
            {
                float x = transform.position.x - pos.x;
                if (x < 0)
                    x = 1;
                else
                    x = -1;

                StartCoroutine(knockback(x));
                StartCoroutine(hurtroutine());
                StartCoroutine(changecolor());
            }
        }
    }
    IEnumerator knockback(float dir)
    {
        isknockback = true;
        float curtime = 0;
        while (curtime < 0.2f)
        {
            if (transform.rotation.y == 0)
                transform.Translate(Vector2.left * knockbackspeed * Time.deltaTime * dir);
            else
                transform.Translate(Vector2.left * knockbackspeed * Time.deltaTime * -1 * dir);

            curtime += Time.deltaTime;
            yield return null;
        }
        ishit = false;
    }

    IEnumerator hurtroutine()
    {
        yield return new WaitForSeconds(5);
        ishit = false;
    }

    IEnumerator changecolor()
    {
        yield return new WaitForSeconds(0.1f);
        sr.color = color1;
        yield return new WaitForSeconds(0.1f);
        sr.color = color2;
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            takedamage (1, enemy.transform.position);
        }

        if (collision.transform.CompareTag("Bossbullet"))
        {
            takedamage(2, bossbullet.transform.position);
        }

        if (collision.transform.CompareTag("Potal"))
        {
            objplayer.transform.position = new Vector2(66f, 1.3f);
            
        }
    }

    public GameObject bullet;

    public float bulletspeed = 10f;

    public Transform bulletpos;

    private void shootbullet()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(bullet, gameObject.transform.position, transform.rotation);
            
        }
    }

    //private void playerstatus()
    //{
    //    hptext.text = $"{curHp}";
    //    damagetext.text = $"{atkDmg}";
    //}

}





