using UnityEngine;

///<summary>
///音效设计
///<summary>

public class MusicManager : MonoBehaviour
{
    public static MusicManager _instance;

    public AudioSource audioSource;
    public AudioClip getMeat;
    public AudioClip getTrex;
    public AudioClip getSkill;
    public AudioClip getWin;
    public AudioClip fire;

    private void Start()
    {
        _instance = this;

    }

    public void playMusicGetMeat()
    {
        this.audioSource.PlayOneShot(getMeat);
    }

    public void playMusicGetTrex()
    {
        this.audioSource.PlayOneShot(getTrex);
    }
    public void playMusicGetSkill()
    {
        this.audioSource.PlayOneShot(getSkill);
    }
    public void playMusicGetWin()
    {
        this.audioSource.PlayOneShot(getWin);
    }
    public void playMusicGetFire()
    {
        this.audioSource.PlayOneShot(fire);
    }
}
