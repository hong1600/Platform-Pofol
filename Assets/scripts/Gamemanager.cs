using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    
    public GameObject player;
    

    private void Awake()
    {

    }
    void Start()
    {

    }


    void Update()
    {
    }

    private void LateUpdate()
    {
    }

    public void respawnplayer()
    {
        Invoke("respawnplayering()", 2 * Time.deltaTime);
        player.transform.position = new Vector2(-8.6f, 2.66f);
        player.SetActive(true);
    }
    private void respawnplayering()
    {
        player.transform.position = new Vector2(-8.6f, 2.66f);
        player.SetActive(true);
    }
}
