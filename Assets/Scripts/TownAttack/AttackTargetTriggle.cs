using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackTown
{

    [RequireComponent(typeof(SphereCollider))]
    public class AttackTargetTriggle : MonoBehaviour
    {
        public List<Transform> TargetList = new List<Transform>();

        private void Awake()
        {
            double dis = transform.parent.GetComponent<BaseAttackTown>().MaxDis;
            transform.GetComponent<SphereCollider>().radius = (float)dis;
        }

        public void AddTarget(Transform target)
        {
            foreach (var item in TargetList)
            {
                if (item.name==target.name)
                {
                    return;
                }
            }
            TargetList.Add(target);
        }
        public void SubTarget(Transform target)
        {
            foreach (var item in TargetList)
            {
                if (item.name == target.name)
                {
                    return;
                }
            }
        }
       
    }
}
