using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichMovement : MonoBehaviour {


    Transform aim;               // Reference to the player's position.
    //PlayerHealth aimHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
    public List<GameObject> aimList = new List<GameObject>();


    void Awake()
    {
        // Set up the references.

        aimList = null;
        aim = GameObject.FindGameObjectWithTag("Player").transform;
        //aimHealth = aim.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }




    void Update()
    {
        //If the aim is dead, searsh for the next aim in the aimList untill find a building or the list is empty.
        //while(aimList != null && aimHealth.currentHealth <= 0)
        //{
        //    aimList.RemoveAt(0);
        //    aim = aimList[0].transform;
        //    aimHealth = aim.GetComponent<PlayerHealth>();
        //
        //};
        // If the enemy and the player have health left...
        //if (enemyHealth.currentHealth > 0 && aimHealth.currentHealth > 0)
        //{
            //    // ... set the destination of the nav mesh agent to the player.
            //   
            //    if (aimList == null)
            //    {
            //        aim = GameObject.FindGameObjectWithTag("Player").transform;
            //        aimHealth = aim.GetComponent<PlayerHealth>();
            //    }
            //    else
            //    {
            //        aim = aimList[0].transform;
            //        aimHealth = aim.GetComponent<PlayerHealth>();
            //    }
            nav.SetDestination(aim.position);
       //}
        // Otherwise...
        //else
        //{
            // ... disable the nav mesh agent.
        //    nav.enabled = false;
       // }
    }
}
