using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackTown
{
    public class BaseAttackTown : MonoBehaviour
    {
        
        [SerializeField]
        private Shader MainShader;
        [SerializeField]
        private Material MainMaterial;
        private AttackTownMGR MGR;

        public Transform Target { get { return Triggle.GetTarget(); } }
        private Transform BulletList;
        private Transform Head;
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
        private List<GameObject> FireRoot=new List<GameObject>();
        private float LastShootTime = 0;
        [SerializeField]
        private float ShootCd = 0.2f;
        private int ShootIndex = 0;

        private AttackTargetTriggle Triggle;

        private void Awake()
        {
            Head = transform.Find("Head");
            MainMaterial = transform.Find("Body").GetComponent<MeshRenderer>().material;
            MainShader = MainMaterial.shader;
            Triggle = transform.GetComponent<AttackTargetTriggle>();
        }

 
        public void TownUpdate()
        {
            if (!IsBuilding)
            {
                if (Target == null)
                {
                    var eular = Head.transform.localEulerAngles;
                    eular.y += SteerSpeed * Time.deltaTime;
                    Head.transform.localEulerAngles = eular;
                }
                else
                {
                    
                    AimTo(Target.transform.position);
                    if (BaseShootObj != null)
                    {
                        var shoot = Instantiate(BaseShootObj);
                        if (FireRoot != null)
                        {
                            shoot.GetComponent<BaseShootObj>().BeginShooting(FireRoot[ShootIndex].transform);
                            ShootIndex++;
                            if (ShootIndex==FireRoot.Count)
                            {
                                ShootIndex = 0;
                            }

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
            Head.transform.localRotation = Quaternion.Lerp(Head.transform.localRotation, Quaternion.LookRotation(Dir, Head.transform.up),0.3f);
        }



        //建造开始初始化
        public void BuildInit(AttackTownMGR mgr)
        {
            MainMaterial.shader = Shader.Find("FX/Hologram Effect");
            transform.Find("Head").GetComponent<MeshRenderer>().material.shader = MainMaterial.shader;
            MGR = mgr;
            IsBuilding = true;
        }

        //建造ing
        public void Building(Vector3 pos,LayerMask layer)
        {
            RaycastHit hit;
            Ray ray = new Ray(pos+Vector3.up*10, -Vector3.up);
            if (Physics.Raycast(ray, out hit, 20, layer))
            {
                transform.position = hit.point;
            }
        }

        //建造
        public void Builded()
        {
            MainMaterial.shader = MainShader;
            transform.Find("Head").GetComponent<MeshRenderer>().material.shader = MainShader;

            IsBuilding = false;
            BulletList = MGR.Bullet;

        }





    }


}

