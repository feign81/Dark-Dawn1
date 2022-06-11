using UnityEngine;
using UnityEngine.UI;
///<summary>
///肉
///<summary>

public class Meat : MonoBehaviour
{
    public static Meat _instance;
    public int count = 0;
    public Text meatCount;
    void Start()
    {
        _instance = this;
        this.gameObject.SetActive(false);
    }
    public void plusMeat()
    {
        MusicManager._instance.playMusicGetMeat();
        count++;
        if (!this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
        }
        if (!meatCount.gameObject.activeInHierarchy)
        {
            meatCount.gameObject.SetActive(true);
        }
        meatCount.text = count + "";
    }
}
