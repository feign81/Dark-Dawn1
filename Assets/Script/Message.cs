using UnityEngine;
using UnityEngine.UI;

///<summary>
///UI显示
///<summary>

public class Message : MonoBehaviour
{
    public static Message _instance;

    public Text Text;
    private float timer;
    private bool startTimer = false;
    private void Start()
    {
        _instance = this;
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                this.gameObject.SetActive(false);
                startTimer = false;
            }
        }
    }
    public void showMessage(string message)
    {
        Text.text = message;
        this.gameObject.SetActive(true);
    }

    public void hiddeSelf()
    {
        this.gameObject.SetActive(false);
    }
    public void showMessage(string message, float length)
    {
        startTimer = true;
        timer = length;
        Text.text = message;
        this.gameObject.SetActive(true);
    }
}
