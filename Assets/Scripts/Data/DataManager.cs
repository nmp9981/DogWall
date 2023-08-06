using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//게임 시작 및 씬을 넘나들 때 사용하는 스크립트
//매우중요!!!!!!!!!!!!!!!!!!!
//왜냐면은 씬을 넘나들 때는 dont destroy on load를 사용하게 된다.
//그렇기 때문에 single ton 이란 스킬을 사용하게 된다.
//single ton은 똑같은 객체가 여러 개 생기는 걸 방지하는 스킬이다.
//gameManager은 전체 게임에서 객체가 단 하나만 존재햐아 한다. 왜냐면은 씬을 넘나들면서 여러개가 생길 수 있는데, 그러면은 관리자가 여러개가 되니까 그건안된다.
//세이브데이터 관리를 여기서 한다.
public class DataManager : MonoBehaviour
{
    //public List<GameObject> itemPrefabs;
    JsonManager jsonManager;
    public SaveDataClass saveData;
    public static DataManager singleTon;
    public string story;

    void Awake()
    {
        if (singleTon == null)
        {
            singleTon = this;
            DontDestroyOnLoad(gameObject);
            //싱글톤이 지정되어있지 않다면. 이 객체(this)를 지정;
        }
        else
        {
            Destroy(gameObject);
            //싱글톤이 이미 지정되어있다면,(한번이라도 위의 코드가 발동했다는 의미) 게임오브젝트 파괴
        }
        //제이슨매니저 할당.
        jsonManager = new JsonManager();
        //load는 세이브데이터 로드다.
        saveData = new SaveDataClass();


        Load();
        Save();
    }

    //세이브데이터 세이브
    public void Save()
    {
        //제이슨에서 세이브제이슨. 아니 이럴거면 함수를 왜만들어요??
        //그러게요....혹시나 뭐가 늘어날지도 모르잖아요.
        jsonManager.SaveJson(saveData);
    }

    //데이터 로드
    public void Load()
    {
        //제이슨에서 세이브데이터 로드. 아니 이럴거면 함수를 왜만들어요??
        //그러게요....혹시나 뭐가 늘어날지도 모르잖아요.
        saveData = jsonManager.LoadSaveData();
        saveData.SetImgforOnce("Character");
    }

    public void DataInitialize()
    {
        jsonManager.DataInitialize();
    }
}
