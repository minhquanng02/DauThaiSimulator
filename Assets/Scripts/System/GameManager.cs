using DG.Tweening;
using DG.Tweening.Core.Easing;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Animation;
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

    public void NewLife()
    {
        wheelUI.CreateWheel(rateOption);
        wheelUI.SpinArrow();
    }

    public void NewStat()
    {
        uiManager.ChangeValueUI(character.age, myCharacterData.age, characterDataUI.age);
        characterDataUI.job.text = myCharacterData.job;
        characterDataUI.gender.text = myCharacterData.gender;

        uiManager.ChangeValueUI(character.health, myCharacterData.health, characterDataUI.health);
        uiManager.ChangeValueUI(character.appearance, myCharacterData.appearance, characterDataUI.appearance);
        uiManager.ChangeValueUI(character.mirrage, myCharacterData.mirrage, characterDataUI.mirrage);
        uiManager.ChangeValueUI(character.stress, myCharacterData.stress, characterDataUI.stress);
        uiManager.ChangeValueUI(character.discipline, myCharacterData.discipline, characterDataUI.disipline);
        uiManager.ChangeValueUI(character.risk, myCharacterData.risk, characterDataUI.risk);
        uiManager.ChangeValueUI(character.iq, myCharacterData.iq, characterDataUI.iq);
        uiManager.ChangeValueUI(character.eq, myCharacterData.eq, characterDataUI.eq);
        uiManager.ChangeValueUI(character.finance, myCharacterData.finance, characterDataUI.finance);
        uiManager.ChangeValueUI(character.social, myCharacterData.social, characterDataUI.social);
        uiManager.ChangeValueUI(character.reputation, myCharacterData.reputation, characterDataUI.reputation);
        uiManager.ChangeValueUI(character.debt, myCharacterData.debt, characterDataUI.dept);
    }

    //UI tao tuoi tho
    public void GameStart()
    {
        uiManager.ChangeValueUI(character.age, myCharacterData.age, uiManager.ageResult);

        uiManager.ShowPanel(uiManager.agePanel);
    }

    //Bam choi lai
    public void NewGame()
    {
        NewCharacter();

        uiManager.HideMidPanel();

        uiManager.loadScene.SetActive(true);
    }

    //Tao data moi cho nhan vat
    public void NewCharacter()
    {
        character = new CharacterData();
        myCharacterData = new CharacterData();

        statSystem = new StatSystem(character, myCharacterData, uiManager);

        NewChaData();
    }

    void NewChaData()
    {
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

        CreateAge();
    }


    //Khoi tao tuoi tho
    public void CreateAge()
    {
        int rate = Random.Range(1, 100);
        if (rate <= 1)
            myCharacterData.age = Random.Range(0, 21);
        else if (rate < 20)
            myCharacterData.age = Random.Range(21, 51);
        else if (rate < 90)
            myCharacterData.age = Random.Range(51, 101);
        else
            myCharacterData.age = Random.Range(101, 201);

    }
}
