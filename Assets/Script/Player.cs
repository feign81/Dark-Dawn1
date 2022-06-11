using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///主角设计
///<summary>
///
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{

    public float animSpeed = 1.5f;              // 动画播放速度
    public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
    public bool useCurves = true;               // 在Mecanim中设定是否使用曲线调整

    public float useCurvesHeight = 0.5f;        // 曲线修正的有效高度


    public float forwardSpeed = 7.0f;//前进速度
    public float backwardSpeed = 2.0f;//后退速度
    public float rotateSpeed = 2.0f;//回旋速度
    public float jumpPower = 3.0f;//跳跃程度

    private CapsuleCollider col;
    private Rigidbody rb;

    private Vector3 velocity;

    private float orgColHight;
    private Vector3 orgVectColCenter;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    private GameObject cameraObject;    // 参考主相机

    public bool getSkill = false;//是否获得技能

    public float timer = 1f;
    private float timerRest;
    public GameObject skillPrefab;//技能预制体

    public bool isNearTrex = false;//是否靠近恐龙
    public bool isGetControlTrex = false;//是否可以控制恐龙
    public bool getControl = true;//是否可以控制主角
    public int needMeatCount = 5;

    public GameObject camera;
    public Trex trex;

    //Animator anim;
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");


    void Start()
    {
        //controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        timerRest = timer;
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        cameraObject = GameObject.FindWithTag("MainCamera");
        orgColHight = col.height;
        orgVectColCenter = col.center;
    }
    void FixedUpdate()
    {
        if (getControl)
        {
            float h = Input.GetAxis("Horizontal");              // 水平
            float v = Input.GetAxis("Vertical");                // 垂直
            anim.SetFloat("Speed", v);
            anim.SetFloat("Direction", h);
            anim.speed = animSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            rb.useGravity = true;

            velocity = new Vector3(0, 0, v);
            velocity = transform.TransformDirection(velocity);


            if (v > 0.1)
            {
                velocity *= forwardSpeed;       // 乘以移动速度
            }
            else if (v < -0.1)
            {
                velocity *= backwardSpeed;  // 乘以移动速度
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (currentBaseState.fullPathHash == locoState)
                {
                    if (!anim.IsInTransition(0))
                    {
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                        anim.SetBool("Jump", true);
                    }
                }
            }
            // 前后移动
            transform.localPosition += velocity * Time.fixedDeltaTime;

            // 左右移动
            transform.Rotate(0, h * rotateSpeed, 0);

        }
        if (getSkill)
        {//E键释放技能
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.E) && timer <= 0)
            {
                GameObject.Instantiate(skillPrefab, transform.position, Quaternion.identity);
                MusicManager._instance.playMusicGetFire();
                timer = timerRest;

            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && isGetControlTrex)
        {//Q切换角色
            if (getControl)
            {
                trex.GetControl();
                this.LoseControl();
            }
            else
            {
                trex.LoseControl();
                this.GetControl();
            }
        }

        if (currentBaseState.fullPathHash == locoState)
        {

            if (useCurves)
            {
                resetCollider();
            }
        }
        else if (currentBaseState.fullPathHash == jumpState)
        {
            cameraObject.SendMessage("setCameraPositionJumpView");
            if (!anim.IsInTransition(0))
            {
                if (useCurves)
                {
                    float jumpHeight = anim.GetFloat("JumpHeight");
                    float gravityControl = anim.GetFloat("GravityControl");
                    if (gravityControl > 0)
                        rb.useGravity = false;

                    Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.distance > useCurvesHeight)
                        {
                            col.height = orgColHight - jumpHeight;
                            float adjCenterY = orgVectColCenter.y + jumpHeight;
                            col.center = new Vector3(0, adjCenterY, 0);
                        }
                        else
                        {
                            resetCollider();
                        }
                    }
                }
                anim.SetBool("Jump", false);
            }
        }

        else if (currentBaseState.fullPathHash == idleState)
        {

            if (useCurves)
            {
                resetCollider();
            }
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Rest", true);
            }
        }
        else if (currentBaseState.fullPathHash == restState)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Rest", false);
            }
        }
    }

    void OnGUI()
    {//是否靠近恐龙
        if (isNearTrex && !isGetControlTrex) { GUI.Box(new Rect(Screen.width - 260, 10, 250, 150), ""); showDialog(); }
    }
    public void showDialog()
    {
        if (Meat._instance.count == 0)
        {
            GUI.Label(new Rect(Screen.width - 245, 30, 250, 30), "恐龙非常饿，想吃5块肉。");
        }
        else
        {
            GUI.Label(new Rect(Screen.width - 245, 30, 250, 30), "恐龙需要5块肉，是否把肉喂给恐龙？");
            bool yes = GUI.Button(new Rect(Screen.width - 245, 50, 30, 30), "是");
            bool no = GUI.Button(new Rect(Screen.width - 245, 90, 30, 30), "否");
            if (yes)
            {
                needMeatCount -= Meat._instance.count;
                Meat._instance.count = 0;
                if (needMeatCount <= 0)
                {
                    isGetControlTrex = true;
                    Message._instance.showMessage("使用Q键切换恐龙", 6);
                    MusicManager._instance.playMusicGetTrex();
                }
                isNearTrex = false;
            }
            if (no)
            {
                isNearTrex = false;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {//进入触发器
        if (other.tag == "Trex")
        {
            isNearTrex = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {//退出触发器
        if (other.tag == "Trex")
        {
            isNearTrex = false;
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
    void resetCollider()
    {
        col.height = orgColHight;
        col.center = orgVectColCenter;
    }
    public void clickButton()
    {
        SceneManager.LoadScene(0);
    }
}
