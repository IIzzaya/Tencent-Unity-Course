using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackTown
{

    public class AttackTownMGR : MonoBehaviour
    {

        public List<BaseAttackTown> AttackTown = new List<BaseAttackTown>();
        public List<GameObject> AttackTownPrefab = new List<GameObject>();
        public LayerMask Layer = new LayerMask();
        //建造
        private GameObject AttackTownPrepareToBuild;
        public void ChooseTownToBuild(int x,Vector3 pos)
        {
            if (AttackTownPrefab.Count-1>x)
            {
                AttackTownPrepareToBuild = Instantiate(AttackTownPrefab[x],transform);
                var town = AttackTownPrepareToBuild.GetComponent<BaseAttackTown>();
                AttackTownPrepareToBuild.GetComponent<MeshRenderer>().material = town.BuildMaterial;
                RefreshPos(pos);
            }
        }
        public void RefreshPos(Vector3 pos)
        {
            pos.y += 10;
            RaycastHit hit;
            Ray ray = new Ray(pos, -Vector3.up);
            if (Physics.Raycast(ray,out hit,10,Layer))
            {
                AttackTownPrepareToBuild.transform.position = hit.point;
            }
        }
        public void QuizBuild()
        {
            Destroy(AttackTownPrepareToBuild);
        }
        public void DecideToBuild()
        {
            var town = AttackTownPrepareToBuild.GetComponent<BaseAttackTown>();
            AttackTownPrepareToBuild.GetComponent<MeshRenderer>().material = town.MainMaterial;
            town.IsBuilding = false;
        }
        

        public void Update()
        {
            foreach (var item in AttackTown)
            {
                item.TownUpdate();
            }
        }
    }
}
