using UnityEngine;

///<summary>
///相机跟随
///<summary>
public class CameraFlow : MonoBehaviour
{

    public Transform target;


    public float distanceUp = 15f;
    public float distanceAway = 10f;
    public float smooth = 2f;//位置平滑移动值
    public float camDepthSmooth = 5f;
    // Use this for initialization

    // Update is called once per frame


    void Update()
    {
        //相机的位置
        Vector3 disPos = target.position + Vector3.up * distanceUp - target.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);
        //相机的角度
        transform.LookAt(target.position);
    }


}
