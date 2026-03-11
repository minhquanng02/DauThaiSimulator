using System;
using System.Collections.Generic;
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
            {
                StatType.Health, v =>
                {
                    character.health += v;
                    uiManager.ChangeValueUI(character.health, myCharacterData.health, gameManager.characterDataUI.health);
            Debug.Log("reward");    
                }
            },
            { StatType.Appearance, v => character.appearance += v },
            { StatType.Stress, v => character.stress += v },
            { StatType.Discipline, v => character.discipline += v },
            { StatType.Risk, v => character.risk += v },

            { StatType.IQ, v => character.iq += v },
            { StatType.EQ, v => character.eq += v },
            { StatType.Finance, v => character.finance += v },
            { StatType.Social, v => character.social += v },
            { StatType.Reputation, v => character.reputation += v },

            { StatType.Debt, v => character.debt += v },
            { StatType.Luck, v => character.luck += v }
        };
    }

    public void ApplyReward(RewardItem reward)
    {
        foreach (var stat in reward.statTypes)
        {
            if (statMap.TryGetValue(stat.stat, out var action))
            {
                action(stat.value);
            }
        }
    }
}
