using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackTown
{

    [RequireComponent(typeof(SphereCollider))]
    public class AttackTargetTriggle : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> TargetList = new List<Transform>();

        private void Awake()
        {
            double dis = transform.GetComponent<BaseAttackTown>().MaxDis;
            transform.GetComponent<SphereCollider>().radius = (float)dis;
        }

        public void AddTarget(Transform target)
        {
            if (!TargetList.Contains(target))
            {
                AddTarget(target);
            }
        }
        public void SubTarget(Transform target)
        {
            if (TargetList.Contains(target))
            {
                SubTarget(target);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag=="Enemy")
            {
                AddTarget(other.transform);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag=="Enemy")
            {
                SubTarget(other.transform);
            }
        }

        public Transform GetTarget()
        {
            if (TargetList.Count!=null)
            {
                return TargetList[0];
            }
            else
            {
                return null;
            }
        }
    }
}
