using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    Data_Manager dataManager;

    //퀘스트 로드
    public void QuestLoad()
    {
        dataManager.Add_Quest( 1, 1, 1, 1, 1, 1, 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
        QuestLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
