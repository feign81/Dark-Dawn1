using UnityEngine;

///<summary>
///胜利触发器
///<summary>

public class WinTrigger : MonoBehaviour
{
    public bool isWin = false;//是否胜利

    public GameObject winGUI;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")//判断是否Player
        {
            isWin = true;
            winGUI.SetActive(true);
            MusicManager._instance.playMusicGetWin();
        }
    }
    private void OnGUI()
    {
        if (isWin)
        {
            bool res = GUI.Button(new Rect(Screen.width - 250, 50, 50, 30), "Quit");
            if (res)
            {
                Application.Quit(); //退出程序
            }
        }
    }
}
