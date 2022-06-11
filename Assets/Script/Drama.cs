using UnityEngine;
using UnityEngine.SceneManagement;

///<summary>
///主界面按钮
///<summary>

public class Drama : MonoBehaviour
{
    public void clickButtonStart()
    {
        SceneManager.LoadScene(1);
    }
    public void clickButtonQuit()
    {
        Application.Quit();
    }
}
