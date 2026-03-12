using System;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class StatSystem : MonoBehaviour
{
    CharacterData character;
    CharacterData myCharacterData;
    UIManager uiManager;
    [SerializeField] private GameManager gameManager;

    Dictionary<StatType, Action<int>> statMap;

    public StatSystem(CharacterData data, CharacterData baseData, UIManager ui, GameManager gm)
    {
        
        character = data;
        myCharacterData = baseData;
        uiManager = ui;
        gameManager = gm;

        statMap = new Dictionary<StatType, Action<int>>()
        {
            { StatType.MaxAge, v =>
            {
                int i = myCharacterData.maxAge;
                myCharacterData.maxAge += v;
                uiManager.ChangeValueUI(i, myCharacterData.maxAge, gameManager.characterDataUI.maxAge);
            }},
            { StatType.Health, v =>
            {
                int i = myCharacterData.health;
                myCharacterData.health += v;
                uiManager.ChangeValueUI(i, myCharacterData.health, gameManager.characterDataUI.health);
            }},
            { StatType.Appearance, v =>
            {
                int i = myCharacterData.appearance;
                myCharacterData.appearance += v;
                uiManager.ChangeValueUI(i, myCharacterData.appearance, gameManager.characterDataUI.appearance);
            }},
            { StatType.Stress, v =>
            {
                int i = myCharacterData.stress;
                myCharacterData.stress += v;
                uiManager.ChangeValueUI(i, myCharacterData.stress, gameManager.characterDataUI.stress);

            }},
            { StatType.Discipline, v =>
            {
                int i = myCharacterData.discipline;
                myCharacterData.discipline += v;
                uiManager.ChangeValueUI(i, myCharacterData.discipline, gameManager.characterDataUI.discipline);

            }},
            { StatType.Risk, v =>
            {
                int i = myCharacterData.risk;
                myCharacterData.risk += v;
                uiManager.ChangeValueUI(i, myCharacterData.risk, gameManager.characterDataUI.risk);

            }},

            { StatType.IQ, v =>
            {
                int i = myCharacterData.iq;
                myCharacterData.iq += v;
                uiManager.ChangeValueUI(i, myCharacterData.iq, gameManager.characterDataUI.iq);

            }},
            { StatType.EQ, v =>
            {
                int i = myCharacterData.eq;
                myCharacterData.eq += v;
                uiManager.ChangeValueUI(i, myCharacterData.eq, gameManager.characterDataUI.eq);

            }},
            { StatType.Finance, v =>
            {
                int i = myCharacterData.finance;
                myCharacterData.finance += v;
                uiManager.ChangeValueUI(i, myCharacterData.finance, gameManager.characterDataUI.finance);

            }},
            { StatType.Social, v =>
            {
                int i = myCharacterData.social;
                myCharacterData.social += v;
                uiManager.ChangeValueUI(i, myCharacterData.social, gameManager.characterDataUI.social);

            }},
            { StatType.Reputation, v =>
            {
                int i = myCharacterData.reputation;
                myCharacterData.reputation += v;
                uiManager.ChangeValueUI(i, myCharacterData.reputation, gameManager.characterDataUI.reputation);

            }},

            { StatType.Debt, v =>
            {
                int i = myCharacterData.debt;
                myCharacterData.debt += v;
                uiManager.ChangeValueUI(i, myCharacterData.debt, gameManager.characterDataUI.debt);

            }},
            { StatType.Luck, v =>
            {
                int i = myCharacterData.luck;
                myCharacterData.luck += v;
                //uiManager.ChangeValueUI(i, myCharacterData.luck, gameManager.characterDataUI.luck);
            }}
        };
    }

    public void ApplyReward(RewardItem reward)
    {
        foreach (var stat in reward.stats)
        {
            if (statMap.TryGetValue(stat.stat, out var action))
            {
                action(stat.value);
            }
            else
            {
                Debug.LogWarning("Stat không tồn tại: " + stat.stat);
            }
        }
    }
}
