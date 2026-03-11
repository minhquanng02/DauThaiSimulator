using DG.Tweening;
using DG.Tweening.Core.Easing;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static CharacterData;
public class GameManager : MonoBehaviour
{
    [Header("Class")]
    [SerializeField] public RateOption rateOption;
    [SerializeField] private WheelUI wheelUI;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private UIManager uiManager;
    [SerializeField] public CharacterData character;
    [SerializeField] public CharacterDataUI characterDataUI;

    [SerializeField] public CharacterData myCharacterData;

    [SerializeField] public StatSystem statSystem;





    public void GameStart()
    {
        //myCharacterData = new CharacterData();
        NewCharacter();


        myCharacterData.health = 60;
        myCharacterData.appearance = 50;
        myCharacterData.mirrage = 0;
        myCharacterData.stress = 50;
        myCharacterData.discipline = 50;
        myCharacterData.risk = 40;
        myCharacterData.iq = 50;
        myCharacterData.eq = 50;
        myCharacterData.finance = 50;
        myCharacterData.social = 40;
        myCharacterData.reputation = 20;
        myCharacterData.debt = 0;
        myCharacterData.luck = 0;
        myCharacterData.job = "Chưa có";
        int g = Random.Range(1, 2);
        if (g == 1)
            myCharacterData.gender = "Nam";
        else myCharacterData.gender = "Nữ";

        uiManager.ShowPanel(uiManager.agePanel);
        wheelUI.CreateAge();
    }

    public void NewCharacter()
    {
        character = new CharacterData();
        myCharacterData = new CharacterData();

        statSystem = new StatSystem(character, myCharacterData, uiManager);
    }

    public void ApplyWheelResult(int index)
    {
        RewardItem reward = rateOption.items[index];

        Debug.Log("Reward: " + reward.optionName);

        statSystem.ApplyReward(reward);
    }

    public void NewLife()
    {
        wheelUI.CreateWheel(rateOption);
        wheelUI.SpinArrow();
    }

    public void NewStat()
    {
        //uiManager.ChangeValueUI(character.age, myCharacterData.age, characterDataUI.age);
        //characterDataUI.job.text = myCharacterData.job;
        //.gender.text = myCharacterData.gender;

        //uiManager.ChangeValueUI(character.health, myCharacterData.health, characterDataUI.health);
        //uiManager.ChangeValueUI(character.appearance, myCharacterData.appearance, characterDataUI.appearance);
        //uiManager.ChangeValueUI(character.mirrage, myCharacterData.mirrage, characterDataUI.mirrage);
        //uiManager.ChangeValueUI(character.stress, myCharacterData.stress, characterDataUI.stress);
        //uiManager.ChangeValueUI(character.discipline, myCharacterData.discipline, characterDataUI.disipline);
        //uiManager.ChangeValueUI(character.risk, myCharacterData.risk, characterDataUI.risk);
        //uiManager.ChangeValueUI(character.iq, myCharacterData.iq, characterDataUI.iq);
        //uiManager.ChangeValueUI(character.eq, myCharacterData.eq, characterDataUI.eq);
        //uiManager.ChangeValueUI(character.finance, myCharacterData.finance, characterDataUI.finance);
        //uiManager.ChangeValueUI(character.social, myCharacterData.social, characterDataUI.social);
        //uiManager.ChangeValueUI(character.reputation, myCharacterData.reputation, characterDataUI.reputation);
        //uiManager.ChangeValueUI(character.debt, myCharacterData.debt, characterDataUI.dept);
    }

    public void ChangeValue()
    {

    }
}
