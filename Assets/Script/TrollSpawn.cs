using UnityEngine;

///<summary>
///怪物生成
///<summary>

public class TrollSpawn : MonoBehaviour
{
    public GameObject trollPrefab;//怪物预制体
    public static int trollCount = 0;//初始数量
    public static int trollMaxCount = 10;//最大数量
    public float timer = 5;
    private float timerReset;

    private void Start()
    {
        timerReset = timer;
    }
    private void Update()
    {
        if (trollCount < trollMaxCount)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                trollCount++;
                GameObject.Instantiate(trollPrefab, transform.position, Quaternion.identity);
                timer = timerReset;
            }
        }
    }
}
