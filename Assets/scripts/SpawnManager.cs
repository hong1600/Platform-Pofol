using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public float spawnTime = 3f;
    public float curTime;
    public int enemyCount;
    public int maxCount;

    public static SpawnManager _instance;

    private void Start()
    {
        _instance = this;
    }

    void Update()
    {
        int x = Random.Range(0, spawnPoints.Length);

        spawnmonster(x);
    }

    public void spawnmonster(int ranNum)
    { 
        if( curTime >= spawnTime && enemyCount < maxCount) 
        {
            Instantiate(enemy, spawnPoints[ranNum]);
            curTime = 0;
            enemyCount ++;
        }
        curTime += Time.deltaTime;
    }

}
