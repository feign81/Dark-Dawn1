
using UnityEngine;

namespace UnityChan
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public float smooth = 3f;
        Transform standardPos;          // the usual position for the camera, specified by a transform in the game
        Transform frontPos;         // Front Camera locater
        Transform jumpPos;          // Jump Camera locater


        bool bQuickSwitch = false;  //Change Camera Position Quickly


        void Start()
        {
            // 初始化
            standardPos = GameObject.Find("CamPos").transform;

            if (GameObject.Find("FrontPos"))
                frontPos = GameObject.Find("FrontPos").transform;

            if (GameObject.Find("JumpPos"))
                jumpPos = GameObject.Find("JumpPos").transform;

            //启动相机
            transform.position = standardPos.position;
            transform.forward = standardPos.forward;
        }

        void FixedUpdate()
        {

            //if (Input.GetButton ("Fire1")) {  // left Ctlr    
            // Change Front Camera
            //setCameraPositionFrontView ();
            if (Input.GetButton("Fire2"))
            {  //Alt   
                //Change Jump Camera
                setCameraPositionJumpView();
            }
            else
            {
                // return the camera to standard position and direction
                setCameraPositionNormalView();
            }
        }

        void setCameraPositionNormalView()
        {
            if (bQuickSwitch == false)
            {
                // the camera to standard position and direction
                transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);
                transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
            }
            else
            {
                // the camera to standard position and direction / Quick Change
                transform.position = standardPos.position;
                transform.forward = standardPos.forward;
                bQuickSwitch = false;
            }
        }

        void setCameraPositionFrontView()
        {
            // Change Front Camera
            bQuickSwitch = true;
            transform.position = frontPos.position;
            transform.forward = frontPos.forward;
        }

        void setCameraPositionJumpView()
        {
            // Change Jump Camera
            bQuickSwitch = false;
            transform.position = Vector3.Lerp(transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);
        }
    }
}
