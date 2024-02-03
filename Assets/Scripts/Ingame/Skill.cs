using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
            Debug.Log($"{i + 1}번 캐릭터 스킬 실행");
            DataManager.singleTon.saveData.inGameData.selectPlayer = i;
            UISetting.PlayerUISetting();
            StartCoroutine(effect.StartEffect(DataManager.singleTon.saveData.CharacterSkill[DataManager.singleTon.saveData.inGameData.playerData[i].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[i]].Type)); // 이펙트
            yield return new WaitForSeconds(1.5f);
            PlayerSkill(i);// 실질적인 스킬 실행
            UISetting.MonsterUISetting(); // 적 채력에 변동이 생겼음으로 바꿔줌
            MonsterCheck(i); // 적이 죽었는지 확인 // 수정 필요
            yield return new WaitForSeconds(1f);
            UISetting.MonsterLocationUISetting();
            Debug.Log($"{i + 1}번 캐릭터 스킬 종료");
        }

        StartCoroutine(MonsterSkill());
    }

    public void PlayerSkill(int num)
    {
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill1 != 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill1);
        }
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill2 != 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill2);
        }
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill3 != 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill3);
        }
        if (DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill4 != 0)
        {
            PlayerIndexSkill(DataManager.singleTon.saveData.CharacterSkill[(DataManager.singleTon.saveData.inGameData.playerData[num].idx * 4 + DataManager.singleTon.saveData.inGameData.selectSkill[num])].Skill4);
        }
    }

    public void PlayerIndexSkill(int idx)
    {
        CharacterSkillIndexDataClass data = DataManager.singleTon.saveData.CharacterSkillIndex[idx];

        float damage;
        float heal;
        float characterAttack = 1; // 최종 공격력 버프

        // 버프
        if (data.DecreaseDamage != 100) // 피해량 감소 버프
        {
            if(data.DecreaseDamage > 100)
            {
                if(data.AllTargets)
                {
                    for(int i = 0; i < DataManager.singleTon.saveData.inGameData.playerData.Count; i++)
                    {
                        foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDefenceBuff[i])
                        {
                            if (buff.value < 100)
                            {
                                DataManager.singleTon.saveData.inGameData.playerDefenceBuff[i].Clear();
                                Debug.Log($"{i+1}번 캐릭터의 받는 데미지 감소 버프가 상쇄되었다!");
                                break;
                            }
                        }
                        DataManager.singleTon.saveData.inGameData.playerDefenceBuff[i].Add(new Buff(data.TurnCount, data.DecreaseDamage));
                        Debug.Log($"{i+1}번 캐릭터에게 {data.TurnCount}턴의 {data.DecreaseDamage} 받는 데미지 증가 버프를 걸었다");
                    }
                }
                else
                {
                    foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDefenceBuff[DataManager.singleTon.saveData.inGameData.selectPlayer])
                    {
                        if (buff.value < 100)
                        {
                            DataManager.singleTon.saveData.inGameData.playerDefenceBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Clear();
                            Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터의 받는 데미지 감소 버프가 상쇄되었다!");
                            break;
                        }
                    }
                    DataManager.singleTon.saveData.inGameData.playerDefenceBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Add(new Buff(data.TurnCount, data.DecreaseDamage));
                    Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터에게 {data.TurnCount}턴의 {data.DecreaseDamage} 받는 데미지 증가 버프를 걸었다");
                }
            }
            else
            {
                if (data.AllTargets)
                {
                    for (int i = 0; i < DataManager.singleTon.saveData.inGameData.playerData.Count; i++)
                    {
                        foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDefenceBuff[i])
                        {
                            if (buff.value > 100)
                            {
                                DataManager.singleTon.saveData.inGameData.playerDefenceBuff[i].Clear();
                                Debug.Log($"{i + 1}번 캐릭터의 받는 데미지 증가 버프가 상쇄되었다!");
                                break;
                            }
                        }
                        DataManager.singleTon.saveData.inGameData.playerDefenceBuff[i].Add(new Buff(data.TurnCount, data.DecreaseDamage));
                        Debug.Log($"{i + 1}번 캐릭터에게 {data.TurnCount}턴의 {data.DecreaseDamage} 받는 데미지 감소 버프를 걸었다");
                    }
                }
                else
                {
                    foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDefenceBuff[DataManager.singleTon.saveData.inGameData.selectPlayer])
                    {
                        if (buff.value > 100)
                        {
                            DataManager.singleTon.saveData.inGameData.playerDefenceBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Clear();
                            Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터의 받는 데미지 증가 버프가 상쇄되었다!");
                            break;
                        }
                    }
                    DataManager.singleTon.saveData.inGameData.playerDefenceBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Add(new Buff(data.TurnCount, data.DecreaseDamage));
                    Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터에게 {data.TurnCount}턴의 {data.DecreaseDamage} 받는 데미지 감소 버프를 걸었다");
                }
            }
        }

        if (data.CharacterAttack != 100) // 피해량 증가 버프
        {
            if (data.CharacterAttack > 100)
            {
                if(data.AllTargets)
                {
                    for (int i = 0; i < DataManager.singleTon.saveData.inGameData.playerData.Count; i++)
                    {
                        foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDamageBuff[i])
                        {
                            if (buff.value < 100)
                            {
                                DataManager.singleTon.saveData.inGameData.playerDamageBuff[i].Clear();
                                Debug.Log($"{i + 1}번 캐릭터의 데미지 감소 버프가 상쇄되었다!");
                                break;
                            }
                        }
                        DataManager.singleTon.saveData.inGameData.playerDamageBuff[i].Add(new Buff(data.TurnCount, data.CharacterAttack));
                        Debug.Log($"{i + 1}번 캐릭터에게 {data.TurnCount}턴의 {data.CharacterAttack} 데미지 증가 버프를 걸었다");
                    }
                }
                else
                {
                    foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDamageBuff[DataManager.singleTon.saveData.inGameData.selectPlayer])
                    {
                        if (buff.value < 100)
                        {
                            DataManager.singleTon.saveData.inGameData.playerDamageBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Clear();
                            Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터의 데미지 감소 버프가 상쇄되었다!");
                            break;
                        }
                    }
                    DataManager.singleTon.saveData.inGameData.playerDamageBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Add(new Buff(data.TurnCount, data.CharacterAttack));
                    Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터에게 {data.TurnCount}턴의 {data.CharacterAttack} 데미지 증가 버프를 걸었다");
                }
            }
            else
            {
                if (data.AllTargets)
                {
                    for (int i = 0; i < DataManager.singleTon.saveData.inGameData.playerData.Count; i++)
                    {
                        foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDamageBuff[i])
                        {
                            if (buff.value > 100)
                            {
                                DataManager.singleTon.saveData.inGameData.playerDamageBuff[i].Clear();
                                Debug.Log($"{i + 1}번 캐릭터의 데미지 증가 버프가 상쇄되었다!");
                                break;
                            }
                        }
                        DataManager.singleTon.saveData.inGameData.playerDamageBuff[i].Add(new Buff(data.TurnCount, data.CharacterAttack));
                        Debug.Log($"{i + 1}번 캐릭터에게 {data.TurnCount}턴의 {data.CharacterAttack} 데미지 감소 버프를 걸었다");
                    }
                }
                else
                {
                    foreach (Buff buff in DataManager.singleTon.saveData.inGameData.playerDamageBuff[DataManager.singleTon.saveData.inGameData.selectPlayer])
                    {
                        if (buff.value > 100)
                        {
                            DataManager.singleTon.saveData.inGameData.playerDamageBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Clear();
                            Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터의 데미지 증가 버프가 상쇄되었다!");
                            break;
                        }
                    }
                    DataManager.singleTon.saveData.inGameData.playerDamageBuff[DataManager.singleTon.saveData.inGameData.selectPlayer].Add(new Buff(data.TurnCount, data.CharacterAttack));
                    Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터에게 {data.TurnCount}턴의 {data.CharacterAttack} 데미지 감소 버프를 걸었다");
                }
            }
        }

        if (data.Provocation) // 도발 스킬이 맞다면
        {
            DataManager.singleTon.saveData.inGameData.playerTaunt[DataManager.singleTon.saveData.inGameData.selectPlayer] = new Buff(data.TurnCount, 1);
            Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터에게 {data.TurnCount}턴의 도발 버프를 걸었다.");
        }

        int buffSum = 100;
        foreach(Buff buff in DataManager.singleTon.saveData.inGameData.playerDamageBuff[DataManager.singleTon.saveData.inGameData.selectPlayer])
        {
            buffSum += (buff.value - 100);
        }

        characterAttack = buffSum / 100f;
        Debug.Log($"{DataManager.singleTon.saveData.inGameData.selectPlayer + 1}번 캐릭터의 데미지 버프 총합은 {buffSum}% 이다");

        if (data.Attack != 0) // 공격 스킬이라면
        {
            damage = data.Attack * characterAttack;
            if(data.AllTargets)
            {
                foreach(MonstersDataClass monster in DataManager.singleTon.saveData.inGameData.crruentMonster)
                {
                    monster.Hp -= (int)damage;
                    Debug.Log($"{monster.Character}에게 {damage}만큼 데미지를 주었다.");
                }
            }
            else
            {
                DataManager.singleTon.saveData.inGameData.crruentMonster[DataManager.singleTon.saveData.inGameData.targetMonster[DataManager.singleTon.saveData.inGameData.selectPlayer]].Hp -= (int)damage;
                Debug.Log($"{DataManager.singleTon.saveData.inGameData.crruentMonster[DataManager.singleTon.saveData.inGameData.targetMonster[DataManager.singleTon.saveData.inGameData.selectPlayer]].Character}에게 {damage}만큼 데미지를 주었다.");
            }
        }
        

        if (data.HealHP != 0) // 회복 스킬이라면
        {
            heal = data.Attack * data.HealHP;
            DataManager.singleTon.saveData.inGameData.hp += (int)heal;
            Debug.Log($"{heal}만큼 힐을 했다.");
        }


        if (data.NotAction) // 자신에게 행동 불능을 건다면
        {
            DataManager.singleTon.saveData.inGameData.playerCanActive[DataManager.singleTon.saveData.inGameData.selectPlayer] = new Buff(data.TurnCount, 1);
            Debug.Log($"{data.TurnCount}턴의 행동 불가 버프를 걸었다");
        }

        if (data.Blood != 0) // 출혈 스킬이 맞다면
        {
            if (data.AllTargets)
            {
                for(int i = 0; i < DataManager.singleTon.saveData.inGameData.crruentMonster.Count; i++)
                {
                    DataManager.singleTon.saveData.inGameData.monsterBlood[i].Add(new Buff(data.TurnCount, data.Blood));
                    Debug.Log($"{i + 1}번 몬스터에게 {data.TurnCount}턴의 {data.Blood} 출혈 데미지를 걸었다");
                }
            }
            else
            {
                DataManager.singleTon.saveData.inGameData.monsterBlood[DataManager.singleTon.saveData.inGameData.targetMonster[DataManager.singleTon.saveData.inGameData.selectPlayer]].Add(new Buff(data.TurnCount, data.Blood));
                Debug.Log($"{DataManager.singleTon.saveData.inGameData.targetMonster[DataManager.singleTon.saveData.inGameData.selectPlayer]}번 몬스터에게 {data.TurnCount}턴의 {data.Blood} 출혈 데미지를 걸었다");
            }
        }

        if (data.HeartLink != 0) // 하트 링크가 있다면
        {
            // 패스
        }

        if (data.DeathLink != 0) // 데스 링크가 있다면
        {
            // 패스
        }

        if (data.Energy != 0) // 에너지 증가 감소가 있다면
        {
            DataManager.singleTon.saveData.inGameData.energy += data.Energy;
            if(data.Energy >= 0)
                Debug.Log($"{data.Energy}의 에너지를 회복했다.");
            else
                Debug.Log($"{data.Energy}의 에너지를 소비했다.");
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
            MonsterSkill(i); // 실질적인 스킬 실행
            PlayerCheck();
            yield return new WaitForSeconds(1f);
        }

        SkillEnd();
    }

    public void MonsterSkill(int num)
    {
        int monsterTurn = DataManager.singleTon.saveData.inGameData.turn % DataManager.singleTon.saveData.inGameData.crruentMonster[num].TurnCount;


        string[] generalTurnIdx = DataManager.singleTon.saveData.inGameData.crruentMonster[num].turn_general.Split(',')[monsterTurn].Split('-');
        string[] specialTurnIdx = DataManager.singleTon.saveData.inGameData.crruentMonster[num].turn_general.Split(',')[monsterTurn].Split('-');

        foreach(string idx in generalTurnIdx)
        {
            if (idx != "" && idx != "0")
                MonsterIndexSkill(int.Parse(idx));
        }
        foreach (string idx in specialTurnIdx)
        {
            if (idx != "" && idx != "0")
                MonsterIndexSkill(int.Parse(idx));
        }
    }

    public void MonsterIndexSkill(int idx)
    {
        Debug.Log(idx);
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