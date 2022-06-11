using UnityEngine;

///<summary>
///修炼技能
///<summary>

public class Exercise : MonoBehaviour
{
    public bool isStart = false;
    public float timer = 5f;
    public bool isExerciseEnd = false;//是否修炼完成

    public GameObject Player;
    public GameObject skillUI;

    private float restTimer;


    private void Start()
    {
        restTimer = timer;
    }
    private void Update()
    {
        if (isStart && isExerciseEnd == false)
        {
            timer -= Time.deltaTime;
            Message._instance.showMessage("修炼时间还剩下:" + timer.ToString("0.00") + "秒!");
            //print("Now left time" + timer);
            if (timer <= 0)
            {
                isExerciseEnd = true;
                skillUI.SetActive(true);
                Message._instance.showMessage("技能修炼完成!");
                MusicManager._instance.playMusicGetSkill();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getSkill = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {//进入触发器
        if (other.gameObject.tag == "Player" && isExerciseEnd == false)
        {
            isStart = true;
            timer = restTimer;
            Message._instance.showMessage("修炼时间还剩下:" + timer.ToString("0.00") + "秒!");
        }
    }
    public void OnTriggerExit(Collider other)
    {//退出触发器
        if (other.gameObject.tag == "Player")
        {
            isStart = false;
            Message._instance.hiddeSelf();
        }
    }
}
