using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//캐릭터 리스트 구조체
public struct characterInfo
{
    public string characterName;//이름
    public int characterHP, characterAttack, characterEnerge;//체력, 공격력, 에너지
    public int characterType;//타입
    public int characterAttribute;//속성
    string characterExplain;//설명

    public characterInfo(string characerName, int characterHP, int characterAttack, int characterEnerge, int characterType, int characterAttribute, string characterExplain)
    {
        this.characterName = characerName;
        this.characterHP = characterHP;
        this.characterAttack = characterAttack;
        this.characterEnerge = characterEnerge;
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

    MonsterMgr monsterMgr;
    TeamSelect teamSelect;
    Turn turn;

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
        playerFullEnerge = characterList[firstPlayer].characterEnerge + characterList[secondPlayer].characterEnerge + characterList[thirdPlayer].characterEnerge + characterList[fourthPlayer].characterEnerge;//풀 에너지
        playerHP = playerFullHP;//처음엔 풀피
        playerEnerge = playerFullEnerge;//처음엔 풀 에너지
        isPlayerBlood = true;//처음엔 정상 상태
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
}