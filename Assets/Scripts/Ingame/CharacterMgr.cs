﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//캐릭터 리스트 구조체
public struct characterInfo
{
    public string characterName;//이름
    public int characterHP, characterAttack;//체력, 공격력, 에너지
    //public int characterEnerge;//에너지
    public int characterType;//타입
    public int characterAttribute;//속성
    string characterExplain;//설명

    public characterInfo(string characerName, int characterHP, int characterAttack, int characterEnerge, int characterType, int characterAttribute, string characterExplain)
    {
        this.characterName = characerName;
        this.characterHP = characterHP;
        this.characterAttack = characterAttack;
        //this.characterEnerge = characterEnerge;
        this.characterType = characterType;
        this.characterAttribute = characterAttribute;
        this.characterExplain = characterExplain;
    }
}

public class CharacterMgr : MonoBehaviour
{
    Data_Manager dataManager;
    private DataManager Data;
    SaveDataClass saveDataClass;
    MonsterMgr monsterMgr;
    TeamSelect teamSelect;
    Turn turn;

    public Text playerHPText;//플레이어 HP텍스트
    public Text playerEnergeText;//플레이어 에너지 텍스트
    
    //캐릭터 상태 이상
    public GameObject characterCondition1;
    public GameObject characterCondition2;
    public GameObject characterCondition3;
    public GameObject characterCondition4;

    Image characterConditionImage1;
    Image characterConditionImage2;
    Image characterConditionImage3;
    Image characterConditionImage4;

   
    public int playerFullHP;//플레이어 총 HP;
    public int firstPlayer;//첫번째 플레이어 
    public int secondPlayer;//두번째 플레이어 
    public int thirdPlayer;//세번째 플레이어 
    public int fourthPlayer;//네번째 플레이어 

    public int playerHP;//플레이어 HP
    public int playerFullEnerge;//플레이어 에너지
    public int playerEnerge;//플레이어 에너지

    // Start is called before the first frame update
    void Start()
    {
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        teamSelect = GameObject.FindWithTag("TeamSelect").GetComponent<TeamSelect>();//TeamSelect 스크립트에서 변수 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기
        dataManager = GameObject.FindWithTag("DBManager").GetComponent<Data_Manager>();//Data_Manager 스크립트에서 변수 가져오기
        Data = GameObject.Find("Data_Managers").gameObject.GetComponent<DataManager>();//데이터 가져오기

        //캐릭터 결정
        firstPlayer = teamSelect.selectedTeamNumber[0];
        secondPlayer = teamSelect.selectedTeamNumber[1];
        thirdPlayer = teamSelect.selectedTeamNumber[2];
        fourthPlayer = teamSelect.selectedTeamNumber[3];

        //풀피 설정
        playerFullHP = (Data.saveData.my_characterlist[firstPlayer].HP + Data.saveData.my_characterlist[secondPlayer].HP + Data.saveData.my_characterlist[thirdPlayer].HP + Data.saveData.my_characterlist[fourthPlayer].HP)*1000;
        playerHP = playerFullHP;//처음엔 풀피

        //풀 에너지 설정
        playerFullEnerge = Data.saveData.my_characterlist[firstPlayer].Energe + Data.saveData.my_characterlist[secondPlayer].Energe + Data.saveData.my_characterlist[thirdPlayer].Energe + Data.saveData.my_characterlist[fourthPlayer].Energe;
        playerEnerge = playerFullEnerge;//처음엔 풀 에너지

        //캐릭터 상태이상 초기상태
        characterCondition1.SetActive(false);
        characterCondition2.SetActive(false);
        characterCondition3.SetActive(false);
        characterCondition4.SetActive(false);

        characterConditionImage1 = characterCondition1.GetComponent<Image>();
        characterConditionImage2 = characterCondition2.GetComponent<Image>();
        characterConditionImage3 = characterCondition3.GetComponent<Image>();
        characterConditionImage4 = characterCondition4.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHPManage();
        PlayerEnergeManage();
    }
    void PlayerHPManage()
    {
        playerHPText.text = "HP : " + playerHP.ToString("0");//화면에 보이게

    }
    void PlayerEnergeManage()
    {
        playerEnergeText.text = playerEnerge.ToString("0");//화면에 보이게
    }
    //플레이어 출혈 데미지
    public void PlayerBloodDamage(int teamIndex,int hitDamage)
    {
        int mobBloodDamage = hitDamage;//원래 피격데미지
        int characterIndex = teamSelect.selectedTeamNumber[teamIndex];//캐릭터 위치
        //하트링크 데스링크 검사
        if (Data.saveData.my_characterlist[characterIndex].heartLink)
        {
            mobBloodDamage -= Data.saveData.MonsterSkillData[0].HeartLink;//일단 임의 몬스터
        }
        if (Data.saveData.my_characterlist[characterIndex].deathLink)
        {
            mobBloodDamage += Data.saveData.MonsterSkillData[0].DeathLink;//일단 임의 몬스터
        }
        //피격데미지
        playerHP = Mathf.Max(playerHP - mobBloodDamage, 0);
    }
    //플레이어 체력 체크
    public void PlayerDie()
    {
        if (playerHP <= 0)
        {
            SceneManager.LoadScene("Home");//홈으로
        }
    }
    //속성체크(몹<->캐릭터)
    public int CheckAttribute(int mobAttribute, int playerAttribute)
    {
        if ((mobAttribute - playerAttribute + 3) % 3 == 1)
        {
            return 50;
        }
        else if ((mobAttribute - playerAttribute + 3) % 3 == 2)
        {
            return 150;
        }
        else if ((mobAttribute == 3 && playerAttribute == 4) || (mobAttribute == 4 && playerAttribute == 3))
        {
            return 50;
        }
        else
        {
            return 100;
        }
    }
    //상태이상별 색상
    public void ColorCondition(int attack,int index,int skillIndex)
    {
        switch (index)
        {
            case 0:
                if (attack == Data.saveData.my_characterlist[skillIndex / 5].ATK)//변동없음
                {
                    characterCondition1.SetActive(false);

                }else if(attack > Data.saveData.my_characterlist[skillIndex / 5].ATK)//증가
                {
                    characterCondition1.SetActive(true);
                    characterConditionImage1.color = Color.blue;//파란색
                }
                else if (attack < Data.saveData.my_characterlist[skillIndex / 5].ATK)//감소
                {
                    characterCondition1.SetActive(true);
                    characterConditionImage1.color = Color.red;//빨간색
                }
                break;
            case 1:
                if (attack == Data.saveData.my_characterlist[skillIndex / 5].ATK)//변동없음
                {
                    characterCondition2.SetActive(false);

                }
                else if (attack > Data.saveData.my_characterlist[skillIndex / 5].ATK)//증가
                {
                    characterCondition2.SetActive(true);
                    characterConditionImage2.color = Color.blue;//파란색
                }
                else if (attack < Data.saveData.my_characterlist[skillIndex / 5].ATK)//감소
                {
                    characterCondition2.SetActive(true);
                    characterConditionImage2.color = Color.red;//빨간색
                }
                break;
            case 2:
                if (attack == Data.saveData.my_characterlist[skillIndex / 5].ATK)//변동없음
                {
                    characterCondition3.SetActive(false);

                }
                else if (attack > Data.saveData.my_characterlist[skillIndex / 5].ATK)//증가
                {
                    characterCondition3.SetActive(true);
                    characterConditionImage3.color = Color.blue;//파란색
                }
                else if (attack < Data.saveData.my_characterlist[skillIndex / 5].ATK)//감소
                {
                    characterCondition3.SetActive(true);
                    characterConditionImage3.color = Color.red;//빨간색
                }
                break;
            case 3:
                if (attack == Data.saveData.my_characterlist[skillIndex / 5].ATK)//변동없음
                {
                    characterCondition4.SetActive(false);

                }
                else if (attack > Data.saveData.my_characterlist[skillIndex / 5].ATK)//증가
                {
                    characterCondition4.SetActive(true);
                    characterConditionImage4.color = Color.blue;//파란색
                }
                else if (attack < Data.saveData.my_characterlist[skillIndex / 5].ATK)//감소
                {
                    characterCondition4.SetActive(true);
                    characterConditionImage4.color = Color.red;//빨간색
                }
                break;
        }
    }
}