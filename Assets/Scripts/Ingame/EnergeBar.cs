using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergeBar : MonoBehaviour
{
    CharacterMgr characterMgr;
    Image EnergeBarImage;//체력바 이미지
    public float EnergeRate;//hp 비율

    // Start is called before the first frame update
    void Start()
    {
        characterMgr = GameObject.FindWithTag("Character").GetComponent<CharacterMgr>();//CharacterMgr 스크립트에서 변수 가져오기
        EnergeBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        EnergeRate = (float)characterMgr.playerEnerge / (float)characterMgr.playerFullEnerge;
        EnergeBarImage.fillAmount = EnergeRate;
    }
}
