using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.CodeDom.Compiler;
using System;

public class Sound_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Sound_Manager sound;
    private DataManager Data;
    public AudioMixer master;
    public Slider Bgm, Sfx;

    AudioClip[] BGMs = new AudioClip[(int)BGM.MaxCount];
    AudioClip[] SFXs = new AudioClip[(int)SFX.MaxCount];

    HashSet<string> SFXNames =  new HashSet<string> ();
    HashSet<string> BGMNames = new HashSet<string>();

    AudioSource BGMSource;
    AudioSource SFXSource;

    enum BGM
    {
        엠피리언_전투BGM_2,
        월령국_전투BGM_최종,
        제한구역_변주,
        제한구역,
        홈화면_BGM,
        MaxCount
    }

    enum SFX
    {
        Back_button,
        Back_to_home,
        Button,
        Clear,
        Game_over,
        Quest_entry,
        Reinforce,
        MaxCount
    }

    void Awake()
    {
        if (sound == null)
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
        Data = GameObject.Find("Data_Manager").gameObject.GetComponent<DataManager>();
        master = Resources.Load<AudioMixer>("Master");
        Find_Sliders();
        Load_Value();
        SoundPooling();
        SetAudioSource();
        Play("홈화면_BGM"); // 테스트용 BGM
        Play("Clear"); // 테스트용 SFX
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Find_Sliders()
    {
        if (Bgm == null || Sfx == null)
        {
            GameObject Canvas = GameObject.Find("Canvas");
            GameObject Setting = null;
            for (int i = 0; i < Canvas.transform.childCount; i++)
            {
                if (Canvas.transform.GetChild(i).name == "Setting")
                {
                    Setting = Canvas.transform.GetChild(i).gameObject;
                    break;
                }
            }
            if (Setting == null)
            {
                Debug.Log("Setting패널이 존재하지 않음");
            }
            else
            {
                for (int i = 0; i < Setting.transform.childCount; i++)
                {
                    if (Setting.transform.GetChild(i).name == "SFX_Slider")
                        Sfx = Setting.transform.GetChild(i).GetComponent<Slider>();
                    else if (Setting.transform.GetChild(i).name == "BGM_Slider")
                        Bgm = Setting.transform.GetChild(i).GetComponent<Slider>();
                    if (Bgm != null && Sfx != null)
                        break;
                }
            }

        }
    }
    public void Sound_Control()
    {
        master.SetFloat("BGM", Bgm.value);
        master.SetFloat("SFX", Sfx.value);
        if (Bgm.value == -40)
        {
            master.SetFloat("BGM", -80);
        }
        if (Sfx.value == -40)
        {
            master.SetFloat("SFX", -80);
        }

    }
    public void Load_Value()
    {
        master.SetFloat("BGM", Data.saveData.ui.bgm);
        master.SetFloat("SFX", Data.saveData.ui.sfx);
        Bgm.value = Data.saveData.ui.bgm;
        Sfx.value = Data.saveData.ui.sfx;
    }
    public void Save_Value()
    {
        Data.saveData.ui.bgm = Bgm.value;
        Data.saveData.ui.sfx = Sfx.value;
    }

    void SoundPooling()
    {
        string[] BGMNames = System.Enum.GetNames(typeof(BGM));
        string[] SFXNames = System.Enum.GetNames(typeof(SFX));
        for (int i = 0; i < BGMNames.Length - 1; i++)
        {
            BGMs[i] = Resources.Load<AudioClip>("sound/BGM/" + BGMNames[i]);
            this.BGMNames.Add(BGMNames[i]);
        }
        for (int i = 0; i < SFXNames.Length - 1; i++)
        {
            SFXs[i] = Resources.Load<AudioClip>("sound/SFX/" + SFXNames[i]);
            this.SFXNames.Add(SFXNames[i]);
        }
    }

    void SetAudioSource()
    {
        AudioSource[] temp;
        temp = gameObject.GetComponents<AudioSource>();
        BGMSource = temp[0];
        SFXSource = temp[1];
        BGMSource.loop = true;
    }

    public void Play(string soundName, float pitch = 1.0f)
    {
        if (BGMNames.Contains(soundName))
        {
            if(BGMSource.isPlaying)
            {
                BGMSource.Stop();
            }
            BGMSource.pitch = pitch;
            BGMSource.clip = BGMs[(int)((BGM)Enum.Parse(typeof(BGM), soundName))];
            BGMSource.Play();
        }
        else if(SFXNames.Contains(soundName))
        {
            SFXSource.pitch = pitch;
            SFXSource.PlayOneShot(SFXs[(int)((SFX)Enum.Parse(typeof(SFX), soundName))]);
        }
        else
        {
            Debug.Log("해당 사운드는 존재하지 않습니다");
        }
    }
}
