using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackTown
{

    public class AttackTownMGR : MonoBehaviour
    {

        public List<BaseAttackTown> AttackTurretList = new List<BaseAttackTown>();
        public List<GameObject> AttackTownPrefab = new List<GameObject>();
        public LayerMask Layer = new LayerMask();
        //建造
        [SerializeField]
        private GameObject AttackTownPrepareToBuild;
        [SerializeField]
        private BaseAttackTown TurretCode;

        private Transform TurretObj;

        public Transform Bullet;
        public Vector3 buildPos = Vector3.zero;

        private void Awake()
        {
            TurretObj = transform.Find("Turrets");
            Bullet = transform.Find("Bullet");
        }

        //开始建造
        public void ChooseTownToBuild(int x,Vector3 pos)
        {
            if (AttackTownPrefab.Count>x)
            {
                Debug.Log("Build");

                AttackTownPrepareToBuild = Instantiate(AttackTownPrefab[x],TurretObj);
                TurretCode = AttackTownPrepareToBuild.GetComponent<BaseAttackTown>();

                TurretCode.BuildInit(this);
                TurretCode.Building(pos,Layer);
            }
        }
        //建造ing
        public void Building(Vector3 pos)
        {
            if (AttackTownPrepareToBuild!=null)
            {
                TurretCode.Building(pos, Layer);
            }
        }
        //停止建造
        public void QuizBuild()
        {
            Destroy(AttackTownPrepareToBuild);
            TurretCode = null;
            AttackTownPrepareToBuild = null;
            
        }
        //确认建造
        public void DecideBuild(Vector3 pos)
        {
            TurretCode.Building(pos, Layer);
            TurretCode.Builded();
            AttackTurretList.Add(TurretCode);
            AttackTownPrepareToBuild = null;
            TurretCode = null;
            
        }
        public void Update()
        {
            GetInput();
            Building(buildPos);

            foreach (var item in AttackTurretList)
            {
                item.TownUpdate();
            }

            
        }

        public void GetInput()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 300, Layer))
            {
                buildPos = hit.point;
            }
            if (AttackTownPrepareToBuild!=null)
            {
               
                if (Input.GetKeyDown(KeyCode.W))
                {
                    DecideBuild(buildPos);
                    Debug.Log("Decide");

                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    QuizBuild();
                    Debug.Log("Quiz");

                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ChooseTownToBuild(0, buildPos);
                }
            }

        }
    }
}
