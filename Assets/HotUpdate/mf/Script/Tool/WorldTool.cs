using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestDemo
{
    public static class WorldTool
    {

        public static Transform TreeRole;
        public static Transform Tree_apos1;
        public static Transform Tree_apos2;
        public static Transform Tree_bpos1;
        public static Transform Tree_bpos2;

        private static Transform tranParent;
        public static void LoadCfg()
        {
            TreeRole = GameObject.Find("Role") ? GameObject.Find("Role").transform : null;

            Tree_apos1 = GameObject.Find("Tree_apos1") ? GameObject.Find("Tree_apos1").transform:null;
            Tree_apos2 = GameObject.Find("Tree_apos2") ? GameObject.Find("Tree_apos2").transform : null;
            Tree_bpos1 = GameObject.Find("Tree_bpos1") ? GameObject.Find("Tree_bpos1").transform : null;
            Tree_bpos2 = GameObject.Find("Tree_bpos2") ? GameObject.Find("Tree_bpos2").transform : null;

            tranParent = GameObject.Find("tran_scene").transform;

            RandScene();
        }

        public static void RandScene()
        {
            int rand = Random.Range(0, 3);
            for (int i = 0; i < 3; i++)
            {
                tranParent.GetChild(i).gameObject.SetActive(i==rand);
            }
        }

    }
}
