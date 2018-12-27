using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AttackTown
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BaseShootObj : MonoBehaviour
    {
        protected float Speed;
        public virtual void BeginShooting(Transform tra)
        {
            transform.position = tra.position;
            transform.GetComponent<Rigidbody>().velocity = tra.forward * Speed;
        }

        public virtual void Flying()
        {

        }

        public virtual void Hit()
        {

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Ground")
            {
                Destroy(transform);
            }
        }
    }



}
