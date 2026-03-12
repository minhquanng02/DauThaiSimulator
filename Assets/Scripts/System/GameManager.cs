using DG.Tweening;
using DG.Tweening.Core.Easing;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static CharacterData;
public class GameManager : MonoBehaviour
{
    [Header("Class")]
    public List<AgeEvent> ageEvents = new List<AgeEvent>();

    [SerializeField] private WheelUI wheelUI;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private UIManager uiManager;
    [SerializeField] public CharacterData character;
    [SerializeField] public CharacterDataUI characterDataUI;
    [SerializeField] public CharacterData myCharacterData;
    [SerializeField] public StatSystem statSystem;
    [SerializeField] public bool pauseAge = false;

    Coroutine ageCoroutine;

    public void ApplyStat()
    {
        uiManager.ChangeValueUI(character.maxAge, myCharacterData.maxAge, characterDataUI.maxAge);
        characterDataUI.job.text = myCharacterData.job;
        characterDataUI.gender.text = myCharacterData.gender;

        uiManager.ChangeValueUI(character.health, myCharacterData.health, characterDataUI.health);
        uiManager.ChangeValueUI(character.appearance, myCharacterData.appearance, characterDataUI.appearance);
        uiManager.ChangeValueUI(character.mirrage, myCharacterData.mirrage, characterDataUI.mirrage);
        uiManager.ChangeValueUI(character.stress, myCharacterData.stress, characterDataUI.stress);
        uiManager.ChangeValueUI(character.discipline, myCharacterData.discipline, characterDataUI.discipline);
        uiManager.ChangeValueUI(character.risk, myCharacterData.risk, characterDataUI.risk);
        uiManager.ChangeValueUI(character.iq, myCharacterData.iq, characterDataUI.iq);
        uiManager.ChangeValueUI(character.eq, myCharacterData.eq, characterDataUI.eq);
        uiManager.ChangeValueUI(character.finance, myCharacterData.finance, characterDataUI.finance);
        uiManager.ChangeValueUI(character.social, myCharacterData.social, characterDataUI.social);
        uiManager.ChangeValueUI(character.reputation, myCharacterData.reputation, characterDataUI.reputation);
        uiManager.ChangeValueUI(character.debt, myCharacterData.debt, characterDataUI.debt);

        ContinueGame();
        StartAgeLoop();
    }


    //Tang tuoi moi giay
    void StartAgeLoop()
    {
        if (ageCoroutine != null)
            StopCoroutine(ageCoroutine);

        ageCoroutine = StartCoroutine(AgeLoop());
    }

    IEnumerator AgeLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.2f);

            if (pauseAge) continue;
            CheckAgeEvent(myCharacterData.age);


            int startAge = myCharacterData.age;

            uiManager.ChangeValueUI(startAge, myCharacterData.age, characterDataUI.age);
            myCharacterData.age++;


        }
    }

    public void CheckAgeEvent(int age)
    {
        foreach (var e in ageEvents)
        {
            if (e.age == age)
            {
                PauseGame();

                wheelUI.CreateWheel(e.rateOption);
                wheelUI.SpinArrow();
                return;
            }
        }
    }

    public void ContinueGame()
    {
        pauseAge = false;
    }
    public void PauseGame()
    {
        pauseAge = true;
    }

    //UI tao tuoi tho
    public void GameStart()
    {
        NewCharacter();

        

        uiManager.ChangeValueUI(character.maxAge, myCharacterData.maxAge, uiManager.ageResult);

        uiManager.ShowPanel(uiManager.agePanel);
    }

    //Bam choi lai
    public void NewGame()
    {
        uiManager.HideMidPanel();

        uiManager.loadScene.SetActive(true);

        ResetDataUI();
    }

    void ResetDataUI()
    {
        characterDataUI.age.text = 0.ToString();
        characterDataUI.maxAge.text = 0.ToString();
        characterDataUI.gender.text = 0.ToString();
        characterDataUI.job.text = 0.ToString();
        characterDataUI.health.text = 0.ToString();
        characterDataUI.appearance.text = 0.ToString();
        characterDataUI.mirrage.text = 0.ToString();
        characterDataUI.stress.text = 0.ToString();
        characterDataUI.discipline.text = 0.ToString();
        characterDataUI.risk.text = 0.ToString();
        characterDataUI.iq.text = 0.ToString();
        characterDataUI.eq.text = 0.ToString();
        characterDataUI.finance.text = 0.ToString();
        characterDataUI.social.text = 0.ToString();
        characterDataUI.reputation.text = 0.ToString();
        characterDataUI.debt.text = 0.ToString();
    }

    //Tao data moi cho nhan vat
    public void NewCharacter()
        {
            character = new CharacterData();
            myCharacterData = new CharacterData();

            statSystem = new StatSystem(character, myCharacterData, uiManager, this);

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
            myCharacterData.maxAge = Random.Range(0, 21);
        else if (rate < 20)
            myCharacterData.maxAge = Random.Range(21, 51);
        else if (rate < 90)
            myCharacterData.maxAge = Random.Range(51, 101);
        else
            myCharacterData.maxAge = Random.Range(101, 201);

    }
}
