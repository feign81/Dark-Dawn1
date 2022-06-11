using UnityEngine;

///<summary>
///
///<summary>

public class HelpTrigger : MonoBehaviour
{
    public GameObject fence1;//物体一
    public GameObject fence2;//物体二

    public bool isTrackFence = false;

    public void destroyFence()
    {//破坏物体
        if (fence1 != null)
        {
            GameObject.Destroy(fence1);
            isTrackFence = true;
        }
        else
        {
            if (fence2 != null) { GameObject.Destroy(fence2); }
        }
    }
    public void OnTriggerEnter(Collider other)
    {//进入触发器
        if (other.tag == "Player" && !isTrackFence)
        {
            Message._instance.showMessage("需要借助恐龙的力量", 4f);
        }
    }
}
