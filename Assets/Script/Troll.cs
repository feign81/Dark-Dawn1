using UnityEngine;

///<summary>
///怪物设计
///<summary>

public class Troll : MonoBehaviour
{
    public bool idle = true;//是否待机
    private Animator anim;

    public float timer = 2.0f;
    public float speed = 5;//速度

    private CharacterController controller;//角色控制器
    public float angle = 0;//角度

    public float health = 10;//生命

    private float destroyTimer = 1.2f;//延迟destroy时间
    private bool startDestroyTimer = false;//是否开始destroy

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (health > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (idle)
                {
                    transforToWAlk();
                }
                else
                {
                    transforToIdle();
                }
            }
            if (!idle)
            {
                if (Mathf.Abs(angle) >= 0.2f)//取绝对值
                {
                    float temp = angle * 0.05f;
                    transform.Rotate(new Vector3(0, temp, 0));
                    angle -= temp;
                }
                controller.SimpleMove(transform.forward * speed);
                //transform.position += transform.forward * Time.deltaTime * speed;

            }

        }
        if (startDestroyTimer)//销毁物体
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                GameObject.Destroy(this.gameObject);

                if (Random.Range(1, 11) <= 7)
                {
                    Meat._instance.plusMeat();
                }
            }
        }
    }

    private void transforToIdle()//待机
    {
        idle = true;
        timer = 2.0f;
        idleAnimation();
    }

    private void transforToWAlk()//移动
    {
        idle = false;
        timer = 5.0f;
        angle = Random.Range(-90, 90);
        //int temp = Random.Range(-90, 90);
        //transform.Rotate(new Vector3(0, temp, 0));
        walkAnimation();
    }

    public void walkAnimation()
    {
        anim.SetFloat("run", 0.0F);
        anim.SetFloat("idle", 0F);
        anim.SetFloat("walk", 1.0F);
    }
    public void idleAnimation()
    {
        anim.SetFloat("idle", 1F);
        anim.SetFloat("walk", 0.0F);
        anim.SetFloat("run", 0F);
    }
    public void die()//死亡
    {
        TrollSpawn.trollCount--;
        anim.SetFloat("death", 1.0F);
        startDestroyTimer = true;

    }
}
