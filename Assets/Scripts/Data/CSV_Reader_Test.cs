using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV_Reader_Test : MonoBehaviour
{
    //테스팅용, Read할 때 csv파일을 메모장으로 열어서 UTF-8로 다시 인코딩해줘야 한글 깨지지 않음!!

    //CSV_READER스크립트에 경로를 Resources폴더의 Data폴더 안의 특정 파일을 읽도록 설정함!! Read안에 파일의 이름을 string으로 적어주면 됨!!

    //Read함수를 이용하여 saveData클래스에 한번에 저장할 계획, 현재 캐릭터 클래스나 몬스터 클래스에 맞도록 데이터셋 생성 필요!!
    
    DataManager DM;
    string path = "";
    void Start()
    {
        Read("CSV_TEST");
        DM = GameObject.Find("Manager").GetComponent<DataManager>();
    }

    void Read(string file)
    {
        List<Dictionary<string, object>> data = CSV_Reader.Read(file);

        for(var i = 0; i < data.Count; i++)
        {
            Debug.Log("이름 = " + data[i]["이름"] + ", HP = " + data[i]["HP"].ToString() + ", 공격력 = " + data[i]["공격력"]);
        }
    }

    void Character_Read(string file)
    {
        List<Dictionary<string, object>> data = CSV_Reader.Read(file);
        for(var i = 0; i < data.Count; i++)
        {
            DM.saveData.list.Add(new Character());
        }
    }
}
