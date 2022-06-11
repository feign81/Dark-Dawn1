using UnityEngine;

///<summary>
///技能
///<summary>

public class skill : MonoBehaviour
{
    public int track = 5;

    private void Start()
    {
        Destroy(this.gameObject, 7);
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")//判定标签为Enemy的物体
        {
            other.GetComponent<Troll>().health -= track * Time.deltaTime;
            if (other.GetComponent<Troll>().health <= 0)
            {
                other.GetComponent<Troll>().die();
            }
        }

    }
}
