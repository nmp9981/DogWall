using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//캐릭터 리스트 구조체
public struct characterInfo
{
    string characterName;//이름
    public int characterHP, characterAttack,characterEnerge;//체력, 공격력, 에너지
    int characterType;//타입
    int characterAttribute;//속성
    string characterExplain;//설명

    public characterInfo(string characerName,int characterHP,int characterAttack,int characterEnerge,int characterType,int characterAttribute,string characterExplain)
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
    static List<characterInfo> characterList = new List<characterInfo>
    {
        new characterInfo("INTJ",8100,13000,27,1,1,"1단계"),
        new characterInfo("ENFP",7200,15000,28,1,2,"2단계"),
        new characterInfo("ENFJ",8000,11500,33,2,2,"3단계"),
        new characterInfo("ISFJ",7700,11500,30,3,1,"4단계"),
        new characterInfo("INFP",8400,12500,25,2,3,"5단계")
    };

    MonsterMgr monsterMgr;
    public int playerFullHP;//플레이어 총 HP;
    public int playerAttack;//캐릭터 공격력
    public int playerHP;//플레이어 HP
    public int playerFullEnerge;//플레이어 에너지
    public int playerEnerge;//플레이어 에너지
    bool isPlayerBlood;//출혈 상태 인가?

    // Start is called before the first frame update
    void Start()
    {
        monsterMgr = GameObject.FindWithTag("Monster").GetComponent<MonsterMgr>();//MonsterMgr 스크립트에서 변수 가져오기

        playerFullHP = characterList[0].characterHP+ characterList[1].characterHP+ characterList[2].characterHP+characterList[3].characterHP;//풀피 설정
        playerFullEnerge = characterList[0].characterEnerge + characterList[1].characterEnerge + characterList[2].characterEnerge + characterList[3].characterEnerge;//풀 에너지
        playerAttack = characterList[0].characterAttack + characterList[1].characterAttack + characterList[2].characterAttack + characterList[3].characterAttack;//캐릭터 공격력
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
        playerHPText.text = "HP : "+playerHP.ToString("0");//화면에 보이게

    }
    void PlayerEnergeManage()
    {
        playerEnergeText.text = playerEnerge.ToString("0");//화면에 보이게
    }
    //플레이어 출혈 데미지
    public void  PlayerBloodDamage()
    {
        if (isPlayerBlood)//출혈시에만 데미지를 입는다.
        {
            playerHP = Mathf.Max(playerHP - monsterMgr.monsterAttackDamage, 0);
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
}
