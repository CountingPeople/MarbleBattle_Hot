using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework
{
    public sealed class CameraModule : BaseModule<CameraModule>
    {
        internal GameObject MainCamObj { get; private set; }
        internal Transform MainCamTran { get; private set; }
        internal Camera MainCam { get; private set; }

        internal Camera ModelCam { get; private set; }
        internal Camera ModelCam2 { get; private set; }

        public Camera GetMainCamera()
        {
            return MainCam;
        }

        public Camera GetModelCamera()
        {
            return ModelCam;
        }

        public Camera GetModel2Camera()
        {
            return ModelCam2;
        }

        public override void Init()
        {
            GameObject obj = ResourcesModule.Instance.Load<GameObject>("Assets/Bundles/Prefab/CamRoot.prefab");
            MainCamTran = GameObject.Instantiate(obj).transform;
            MainCamTran.SetParent(transform);
            MainCam= MainCamTran.Find("UICam").GetComponent<Camera>();
            //MainCam.gameObject.AddComponent<CameraModel>();
            
            ModelCam = MainCamTran.Find("ModelCam").GetComponent<Camera>();

            ModelCam2 = MainCamTran.Find("ModelCam2").GetComponent<Camera>();


            //TouchCam.Instance.InitData();

            //MainCamObj = new GameObject("MainCamera", typeof(Camera));
            //MainCamTran = MainCamObj.transform;
            //MainCamTran.SetParent(transform);
            //MainCam = MainCamObj.GetComponent<Camera>();
            //MainCam.clearFlags = CameraClearFlags.Skybox;
            //MainCam.orthographic = true;
            //MainCam.backgroundColor = new Color(0.05f, 0.05f, 0.05f, 1);
            //MainCam.orthographicSize = 20;
            //MainCamObj.AddComponent<AudioListener>();
        }

        public void SetChapterCam()
        {
            var cam = GetMainCamera();
            cam.transform.localPosition = new Vector3(1585, 525, 970);
            cam.transform.localEulerAngles = new Vector3(55, -180, 0);
        }
        public void SetBattleCam()
        {
            var cam = GetMainCamera();
            cam.transform.localPosition = new Vector3(118, 58, 7);
            cam.transform.localEulerAngles = new Vector3(45, 0, 0);
        }


    }
}
