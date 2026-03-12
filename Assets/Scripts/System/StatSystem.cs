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

    public StatSystem(CharacterData data, CharacterData baseData, UIManager ui)
    {
        
        character = data;
        myCharacterData = baseData;
        uiManager = ui;

        statMap = new Dictionary<StatType, Action<int>>()
        {
            { StatType.Health, v =>
            {
                myCharacterData.health += v;
                uiManager.ChangeValueUI(character.health, myCharacterData.health, gameManager.characterDataUI.health);

            }},
            { StatType.Appearance, v => myCharacterData.appearance += v },
            { StatType.Stress, v => myCharacterData.stress += v },
            { StatType.Discipline, v => myCharacterData.discipline += v },
            { StatType.Risk, v => myCharacterData.risk += v },

            { StatType.IQ, v => myCharacterData.iq += v },
            { StatType.EQ, v => myCharacterData.eq += v },
            { StatType.Finance, v => myCharacterData.finance += v },
            { StatType.Social, v => myCharacterData.social += v },
            { StatType.Reputation, v => myCharacterData.reputation += v },

            { StatType.Debt, v => myCharacterData.debt += v },
            { StatType.Luck, v => myCharacterData.luck += v }
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
