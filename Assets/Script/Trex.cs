using UnityEngine;

///<summary>
///恐龙设计
///<summary>

public class Trex : MonoBehaviour
{
    private CharacterController Controller;//角色控制器
    public float speed = 8;//速度
    public Animation animation;
    private string currentAnimtionName;//当前的动画
    public GameObject helpTriggerGameObject;
    public bool getControl = false;//是否可以控制
    public GameObject camera;
    private void Start()
    {
        Controller = this.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (getControl)
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 30 * Time.deltaTime, 0));//左右转
            Controller.SimpleMove(transform.forward * speed * Input.GetAxis("Vertical"));//前后移动
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                if (currentAnimtionName != "walk_loop")//前进
                {
                    this.GetComponent<Animation>().CrossFade("walk_loop");
                    currentAnimtionName = "walk_loop";
                }
            }
            else
            {
                if (currentAnimtionName != "idle")//待机
                {
                    this.GetComponent<Animation>().CrossFade("idle");
                    currentAnimtionName = "idle";
                }
            }
            if (Input.GetButtonDown("Fire1"))//攻击
            {
                this.GetComponent<Animation>().CrossFade("hit");
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject == helpTriggerGameObject)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                other.GetComponent<HelpTrigger>().destroyFence();
            }
        }
    }
    public void GetControl()
    {
        getControl = true;
        camera.SetActive(true);
    }
    public void LoseControl()
    {
        getControl = false;
        camera.SetActive(false);
    }
}
