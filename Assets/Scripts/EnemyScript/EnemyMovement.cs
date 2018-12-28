using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Transform aim; // Reference to the player's position.
    private PlayerHealth aimHealth; // Reference to the player's health.
    private EnemyHealth enemyHealth; // Reference to this enemy's health.
    private UnityEngine.AI.NavMeshAgent nav; // Reference to the nav mesh agent.
    public List<GameObject> aimList = new List<GameObject>();
    public float speed = 2.5f;

    private bool isMoving = false;
    private Animator animator;
    private EnemyAttack enemyAttack;

    void Awake() {
        // Set up the references.

        aimList = null;
        aim = GameObject.FindGameObjectWithTag("Player").transform;
        aimHealth = aim.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        lastPos = transform.position;
        lastTime = 0;
        // DontDestroyOnLoad(gameObject);

        enemyAttack = GetComponent<EnemyAttack>();
    }

    private Vector3 lastPos; //上一次运动停止的位置
    private float lastTime; //上一次运动停止的时间
    void Update() {
        if (enemyHealth.isDead) return;

        if (Vector3.Distance(lastPos, transform.position) > 0.3f) //如果上次静止的位置和当前位置不相同,就更新上次静止的位置和时间
        {
            lastTime = Time.time;
            lastPos = transform.position;
            animator.SetBool("isMoving", true);
        }

        //If the aim is dead, searsh for the next aim in the aimList untill find a building or the list is empty.
        while (aimList != null && aimHealth.isDead) {
            aimList.RemoveAt(0);
            aim = aimList[0].transform;
            aimHealth = aim.GetComponent<PlayerHealth>();
        };

        //If the enemy and the player have health left...
        if (!aimHealth.isDead) {
            // ... set the destination of the nav mesh agent to the player.

            // if (aimList == null) {
            //     aim = GameObject.FindGameObjectWithTag("Player").transform;
            //     aimHealth = aim.GetComponent<PlayerHealth>();
            // } else {
            //     aim = aimList[0].transform;
            //     aimHealth = aim.GetComponent<PlayerHealth>();
            // }

            if (enemyAttack == null || !enemyAttack.playerInRange) {
                nav.speed = speed;
                nav.SetDestination(aim.position);
            } else {
                nav.speed = 0f;
                SmoothRotate(aim.position);
                animator.SetBool("isMoving", false);
            }
        } else {
            // ... disable the nav mesh agent.
            nav.speed = 0f;
        }
    }

    private void SmoothRotate(Vector3 targetPosition) {
        targetPosition.y = transform.position.y;
        Quaternion q = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
        var lerp = Quaternion.Lerp(transform.rotation, q, 3f * Time.deltaTime);
        transform.rotation = lerp;
    }

}