using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager global;

    public GameObject player;
    PlayerHealth playerHealth;
    public GameObject[] enemyPrefab;
    public float spawnTime = 1f;
    public Transform[] spawnPoints;
    public List<GameObject> enemyList;
    public int wave;
    public int enemyNum;
    GameObject currentEnemy;
    private float timer;
    private float waveTimer;
    public float waveDelay = 8f;
    public int spawnNum;
    public int spawnIndex;

    void Awake() {
        if (EnemyManager.global == null) {
            EnemyManager.global = this;
        }
        Debug.Log("Enemy Manager Loaded");
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyList = new List<GameObject>();
        wave = 1;
        spawnNum = 0;
        spawnIndex = 10;
    }

    private void Update() {
        if (!playerHealth.isDead) {
            timer += Time.deltaTime;
            if (timer >= spawnTime) {
                timer = 0;
                if (spawnNum < spawnIndex) {
                    Spawn();
                    spawnNum++;
                }
            }
            if (spawnNum >= spawnIndex && enemyNum == 0) {
                waveTimer += Time.deltaTime;
                if (waveTimer >= waveDelay) {
                    waveTimer = 0;
                    spawnNum = 0;
                    wave++;
                    spawnIndex = Mathf.FloorToInt(5 * (10 - 8 * Mathf.Exp(-1 * wave / 10)));
                }
            }
        }
    }

    void Spawn() {
        if (playerHealth.currentHealth <= 0f) {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        if (enemyPrefab.Length > 0) {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            currentEnemy = Instantiate(enemyPrefab[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            enemyList.Add(currentEnemy);
            enemyNum++;
        }

    }

    public static void EnemyDelete(GameObject DE) {
        global.enemyList.Remove(DE);
        global.enemyNum--;
    }

}