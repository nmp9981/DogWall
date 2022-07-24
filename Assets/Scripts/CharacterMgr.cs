using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMgr : MonoBehaviour
{
    public Text playerHPText;//플레이어 HP텍스트
    public Text playerEnergeText;//플레이어 에너지 텍스트

    public int playerFullHP = 30000;//플레이어 총 HP;
    public int playerHP;//플레이어 HP
    public int playerFullEnerge = 90;//플레이어 에너지
    public int playerEnerge;//플레이어 에너지

    // Start is called before the first frame update
    void Start()
    {
        playerHP = playerFullHP;//처음엔 풀피
        playerEnerge = playerFullEnerge;//처음엔 풀 에너지
    }

    // Update is called once per frame
    void Update()
    {
        playerHP -= 1;//테스트 코드
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
}
