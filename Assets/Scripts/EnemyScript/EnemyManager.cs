using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject player;
    PlayerHealth playerHealth;
    public GameObject[] enemy;
    public float spawnTime = 1f;
    public Transform[] spawnPoints;
    public List<GameObject> enemyList;
    public int wave;
    public int enemyNum;
    GameObject currentEnemy;
    float timer;
    float waveTimer;
    float waveDelay = 20f;
    public int spawnNum;
    public int spawnIndex;

    void Start()
    {
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyList = new List<GameObject>();
        wave = 1;
        spawnNum = 0;
        spawnIndex = 10;
    }

    private void Update()
    {
        if (playerHealth.currentHealth > 0)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                if (spawnNum < spawnIndex)
                {
                    Spawn();
                    spawnNum++;
                }
                else if (spawnNum >= spawnIndex && enemyNum == 0)
                {
                    waveTimer = Time.deltaTime;
                    if (waveTimer >= waveDelay)
                    {
                        waveTimer = 0;
                        spawnNum = 0;
                        wave++;
                        spawnIndex = Mathf.FloorToInt(5 * (10 - 8 * Mathf.Exp(-1 * wave / 10)));
                    }
                }

            }
        }
    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemy.Length);

        currentEnemy = Instantiate(enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        enemyList.Add(currentEnemy);
        enemyNum++;
    }

    public void enemyDelete(GameObject DE)
    {
        enemyList.Remove(DE);
        enemyNum--;
    }

    
}
