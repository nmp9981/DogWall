using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text playerHPText;//플레이어 HP텍스트
    public Text playerEnergeText;//플레이어 에너지 텍스트

    //캐릭터 배열 선언 
    public static List<characterInfo> characterList = new List<characterInfo>
    {
        new characterInfo("INTJ",8100,13000,27,1,1,"1단계"),
        new characterInfo("ENFP",7200,15000,28,1,2,"2단계"),
        new characterInfo("ENFJ",8000,11500,33,2,2,"3단계"),
        new characterInfo("ISFJ",7700,11500,30,3,1,"4단계"),
        new characterInfo("INFP",8400,12500,25,0,3,"5단계"),
        new characterInfo("INTP",8800,10500,24,4,3,"6단계"),
        new characterInfo("ESFP",9000,8500,11,0,1,"7단계"),
        new characterInfo("ESTJ",5200,18000,18,2,3,"8단계")
    };

    SaveDataClass saveDataClass;
    MonsterMgr monsterMgr;
    TeamSelect teamSelect;
    Turn turn;

    //캐릭터 상태 이상
    public GameObject characterCondition1;
    public GameObject characterCondition2;
    public GameObject characterCondition3;
    public GameObject characterCondition4;

    public Image characterConditionImage1;
    public Image characterConditionImage2;
    public Image characterConditionImage3;
    public Image characterConditionImage4;

    public int playerFullHP;//플레이어 총 HP;
    public int firstPlayer;//첫번째 플레이어 
    public int secondPlayer;//두번째 플레이어 
    public int thirdPlayer;//세번째 플레이어 
    public int fourthPlayer;//네번째 플레이어 

    public int playerHP;//플레이어 HP
    public int playerFullEnerge;//플레이어 에너지
    public int playerEnerge;//플레이어 에너지
    bool isPlayerBlood;//출혈 상태 인가?

    // Start is called before the first frame update
    void Start()
    {
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기
        teamSelect = GameObject.FindWithTag("TeamSelect").GetComponent<TeamSelect>();//TeamSelect 스크립트에서 변수 가져오기
        turn = GameObject.FindWithTag("TurnMgr").GetComponent<Turn>();//Trun 스크립트에서 변수 가져오기

        //캐릭터 결정
        firstPlayer = teamSelect.selectedTeamNumber[0];
        secondPlayer = teamSelect.selectedTeamNumber[1];
        thirdPlayer = teamSelect.selectedTeamNumber[2];
        fourthPlayer = teamSelect.selectedTeamNumber[3];

        playerFullHP = characterList[firstPlayer].characterHP + characterList[secondPlayer].characterHP + characterList[thirdPlayer].characterHP + characterList[fourthPlayer].characterHP;//풀피 설정
        playerHP = playerFullHP;//처음엔 풀피
        isPlayerBlood = true;//처음엔 정상 상태

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
        //playerHP = saveDataClass.list[0].HP;//Json데이너 불러오기
        //playerEnergeText.text = playerEnerge.ToString("0");//화면에 보이게
    }
    //플레이어 출혈 데미지
    public void PlayerBloodDamage()
    {
        if (isPlayerBlood)//출혈시에만 데미지를 입는다.
        {
            playerHP = Mathf.Max(playerHP - (monsterMgr.monsterAttackDamage * turn.skillCount) / 4, 0);
        }
    }
    //플레이어 체력 체크
    public bool IsPlayerDie()
    {
        if (playerHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
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
            case 1:
                if (attack == characterList[skillIndex / 4].characterAttack)//변동없음
                {
                    characterCondition1.SetActive(false);

                }else if(attack > characterList[skillIndex / 4].characterAttack)//증가
                {
                    characterCondition1.SetActive(true);
                    characterConditionImage1.color = Color.blue;//파란색
                }
                else if (attack < characterList[skillIndex / 4].characterAttack)//감소
                {
                    characterCondition1.SetActive(true);
                    characterConditionImage1.color = Color.red;//빨간색
                }
                break;
            case 2:
                if (attack == characterList[skillIndex / 4].characterAttack)//변동없음
                {
                    characterCondition2.SetActive(false);

                }
                else if (attack > characterList[skillIndex / 4].characterAttack)//증가
                {
                    characterCondition2.SetActive(true);
                    characterConditionImage2.color = Color.blue;//파란색
                }
                else if (attack < characterList[skillIndex / 4].characterAttack)//감소
                {
                    characterCondition2.SetActive(true);
                    characterConditionImage2.color = Color.red;//빨간색
                }
                break;
            case 3:
                if (attack == characterList[skillIndex / 4].characterAttack)//변동없음
                {
                    characterCondition3.SetActive(false);

                }
                else if (attack > characterList[skillIndex / 4].characterAttack)//증가
                {
                    characterCondition3.SetActive(true);
                    characterConditionImage3.color = Color.blue;//파란색
                }
                else if (attack < characterList[skillIndex / 4].characterAttack)//감소
                {
                    characterCondition3.SetActive(true);
                    characterConditionImage3.color = Color.red;//빨간색
                }
                break;
            case 4:
                if (attack == characterList[skillIndex / 4].characterAttack)//변동없음
                {
                    characterCondition4.SetActive(false);

                }
                else if (attack > characterList[skillIndex / 4].characterAttack)//증가
                {
                    characterCondition4.SetActive(true);
                    characterConditionImage4.color = Color.blue;//파란색
                }
                else if (attack < characterList[skillIndex / 4].characterAttack)//감소
                {
                    characterCondition4.SetActive(true);
                    characterConditionImage4.color = Color.red;//빨간색
                }
                break;
        }
    }
}