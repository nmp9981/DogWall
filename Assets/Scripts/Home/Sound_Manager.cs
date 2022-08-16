﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Sound_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Sound_Manager sound;
    private SaveDataClass saveData;
    public AudioMixer master;
    public Slider Bgm,Sfx;
    void Awake()
    {
        if(sound == null)
        {
            sound = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        saveData = GameObject.Find("Data_Manager").gameObject.GetComponent<DataManager>().saveData;
        master = Resources.Load<AudioMixer>("Master");
        Find_Sliders();
        Load_Value();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Find_Sliders()
    {
        if(Bgm == null || Sfx == null)
        {
            GameObject Canvas = GameObject.Find("Canvas");
            GameObject Setting = null;
            for(int i = 0; i < Canvas.transform.childCount; i++)
            {
                if(Canvas.transform.GetChild(i).name == "Setting")
                {
                    Setting = Canvas.transform.GetChild(i).gameObject;
                    break;
                }
            }
            if(Setting == null)
            {
                Debug.Log("Setting패널이 존재하지 않음");
            }
            else
            {
                for(int i = 0; i < Setting.transform.childCount; i++)
                {
                    if(Setting.transform.GetChild(i).name == "SFX_Slider")
                        Sfx = Setting.transform.GetChild(i).GetComponent<Slider>();
                    else if(Setting.transform.GetChild(i).name == "BGM_Slider")
                        Bgm = Setting.transform.GetChild(i).GetComponent<Slider>();
                    if(Bgm != null && Sfx != null)
                        break;
                }
            }
            
        }
    }
    public void Sound_Control()
    {
        if(Bgm.value == -40)
        {
            master.SetFloat("BGM",-80);
        }
        else if(Sfx.value == -40)
        {
            master.SetFloat("SFX",-80);
        }
        else
        {
            master.SetFloat("BGM",Bgm.value);
            master.SetFloat("SFX",Sfx.value);
        }
        saveData.ui.bgm = Bgm.value;
        saveData.ui.sfx = Sfx.value;
    }
    public void Load_Value()
    {
        Bgm.value = saveData.ui.bgm;
        Sfx.value = saveData.ui.sfx;
        master.SetFloat("BGM",Bgm.value);
        master.SetFloat("SFX",Sfx.value);
    }
}
