using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//캐릭터 리스트 구조체
struct characterInfo
{
    Image characterImage;//이미지
    string characterName;//이름
    float characterHP, characterAttack;//체력, 공격력
    int characterType;//타입
    string characterAttribute;//속성
    string characterExplain;//설명
}

public class CharacterMgr : MonoBehaviour
{
    public Text playerHPText;//플레이어 HP텍스트
    public Text playerEnergeText;//플레이어 에너지 텍스트

    characterInfo[] chracterList = new characterInfo[20];//캐릭터 배열 선언 

    public int playerFullHP = 30000;//플레이어 총 HP;
    public int playerHP;//플레이어 HP
    public int playerFullEnerge = 90;//플레이어 에너지
    public int playerEnerge;//플레이어 에너지
    bool isPlayerBlood;//출혈 상태 인가?

    // Start is called before the first frame update
    void Start()
    {
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
            playerHP = Mathf.Max(playerHP - 2500, 0);
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
