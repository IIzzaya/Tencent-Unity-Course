using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackTown
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BaseAttackTown : MonoBehaviour
    {
        public Material MainMaterial;
        public Material BuildMaterial;
        private Transform Target;
        private Transform BulletList;

        //建造
        public bool IsBuilding = false;
        //巡视属性
        [SerializeField]
        private float SteerSpeed = 90;
        public float MaxDis = 10;
        //射击属性
        [SerializeField]
        private GameObject BaseShootObj;
        [SerializeField]
        private GameObject FireRoot;
        private float LastShootTime = 0;
        [SerializeField]
        private float ShootCd = 0.2f;


        private void Awake()
        {
            BulletList = transform.parent.Find("Bullet");
            if (BulletList==null)
            {
                BulletList = new GameObject("Bullet").transform;
                BulletList.parent = transform.parent;
            }
        }
        public BaseAttackTown()
        {
            MainMaterial = transform.GetComponent<MeshRenderer>().material;

            IsBuilding = true;
        }
        public void TownUpdate()
        {
            if (!IsBuilding)
            {
                if (Target == null)
                {
                    var eular = transform.localEulerAngles;
                    eular.y += SteerSpeed * Time.deltaTime;
                }
                else
                {
                    AimTo(Target.transform.position);
                    if (BaseShootObj != null)
                    {
                        var shoot = Instantiate(BaseShootObj);
                        if (FireRoot != null)
                        {
                            shoot.GetComponent<BaseShootObj>().BeginShooting(FireRoot.transform);
                        }
                        shoot.transform.parent = BulletList;
                        Destroy(shoot, 10);
                    }
                }
            }
        }
        private void AimTo(Vector3 targetPos)
        {
            Vector3 Dir = targetPos - transform.position;
            Dir.y = 0;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(Dir, transform.up),0.3f);
        }

        public void FindTarget(Transform target)
        {
            if (Target!=null)
            {
                
            }
        }
        public delegate void DeathDelegate();
        public void PrePareToBuild(Vector3 pos)
        {

        }



        
    }


}

