using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour{

    public static BGMManager instance;
    public AudioSource BGM;
    public AudioClip BGMClip;
    #region singleton
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion singleton

    public void Start(){
        //BGM.loop = true;
        BGM.clip = BGMClip;
        BGM.Play();
    }
}
