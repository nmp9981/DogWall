using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    Effect effect;
    [SerializeField]
    UISetting UISetting;

    public IEnumerator PlayerSkill()
    {
        int maxNum = DataManager.singleTon.saveData.inGameData.playerData.Count;
        for (int i = 0; i < maxNum; i++)
        {
            DataManager.singleTon.saveData.inGameData.selectPlayer = i;
            UISetting.PlayerUISetting();
            StartCoroutine(effect.StartEffect(1)); // 이펙트
            yield return new WaitForSeconds(1.5f);
            PlayerSkill(i);// 실질적인 스킬 실행
            UISetting.MonsterUISetting(); // 적 채력에 변동이 생겼음으로 바꿔줌
            MonsterCheck(i); // 적이 죽었는지 확인
            yield return new WaitForSeconds(1f);
            UISetting.MonsterLocationUISetting();
        }

        StartCoroutine(MonsterSkill());
    }

    public void PlayerSkill(int num)
    {
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill1 == 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill1);
        }
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill2 == 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill2);
        }
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill3 == 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill3);
        }
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill4 == 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill4);
        }
    }

    public void PlayerIndexSkill(int idx)
    {
        CharacterSkillIndexDataClass data = DataManager.singleTon.saveData.CharacterSkillIndex[idx];

        int damage;
        int heal;

        if (data.Attack != 0) // 공격 스킬이라면
        {
            damage = data.Attack * data.CharacterAttack;
        }

        if (data.DecreaseDamage != 0) // 받는 피해량 관련이라면
        {

        }
        
        if (data.HealHP != 0) // 회복 스킬이라면
        {
            heal = data.CharacterAttack * data.HealHP;
        }

        if (data.Provocation) // 도발 스킬이 맞다면
        {

        }

        if (data.NotAction) // 자신에게 행동 불능을 건다면
        {

        }

        if (data.Blood != 0) // 출혈 스킬이 맞다면
        {

        }
        else // 출혈 스킬이 아니라면
        {

        }

        if (data.AllTargets) // 전체 공격이라면
        {
            
        }
        else // 전체 공격이 아니라면
        {

        }

        if (data.HeartLink != 0) // 하트 링크가 있다면
        {

        }

        if (data.DeathLink != 0) // 데스 링크가 있다면
        {

        }

        if (data.Energy != 0) // 에너지 증가 감소가 있다면
        {

        }
    }

    public IEnumerator MonsterSkill()
    {
        int maxNum = DataManager.singleTon.saveData.inGameData.monsterCount;
        for (int i = 0; i < maxNum; i++)
        {
            DataManager.singleTon.saveData.inGameData.selectPlayer = 0;
            UISetting.PlayerUISetting();
            StartCoroutine(effect.StartEffect(1)); // 이펙트
            yield return new WaitForSeconds(1.5f);
            // 실질적인 스킬 실행
            PlayerCheck();
            yield return new WaitForSeconds(1f);
        }

        SkillEnd();
    }

    public void SkillEnd()
    {
        DataManager.singleTon.saveData.inGameData.isTurn = true;
    }

    public void MonsterCheck(int num)
    {
        if (DataManager.singleTon.saveData.inGameData.crruentMonster[DataManager.singleTon.saveData.inGameData.targetMonster[num]].Hp <= 0)
        {
            UISetting.MonsterFadeOut(DataManager.singleTon.saveData.inGameData.targetMonster[num]);
            DataManager.singleTon.saveData.inGameData.crruentMonster.RemoveAt(DataManager.singleTon.saveData.inGameData.targetMonster[num]);
            DataManager.singleTon.saveData.inGameData.monsterCount--;
        }
    }

    public void PlayerCheck()
    {
        if (DataManager.singleTon.saveData.inGameData.hp <= 0)
        {
            // 플레이어 사망
            GameOver();
        }
    }

    public void GameOver()
    {
        // 게임 오버
    }
}