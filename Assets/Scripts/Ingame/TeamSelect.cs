using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelect : MonoBehaviour
{
    const int maxCharacterNum = 10;//최대 캐릭터 수
    const int selectedSize = 4;//선택 캐릭터 수
    public bool[] isSelect;//팀선택 조합
    public int[] selectedTeamNumber;//선택한 번호

    // Update is called once per frame
    void Start()
    {
        selectedTeamNumber = new int[selectedSize];//배열 초기화
        isSelect = new bool[maxCharacterNum];
        SelectNum();//팀 번호 선택
    }
    //선택
    public void SelectNum()
    {
        int selectedCount = 0;//총 선택 개수
        
        while (selectedCount < selectedSize)
        {
            selectedTeamNumber[selectedCount] = Random.Range(0, maxCharacterNum);//번호 결정
            if (isSelect[selectedTeamNumber[selectedCount]] == false)//아직 미선택
            {
                isSelect[selectedTeamNumber[selectedCount]] = true;//선택 체크
                selectedCount++;
            }
        }
    }
}