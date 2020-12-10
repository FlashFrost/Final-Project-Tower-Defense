using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideMenu : MonoBehaviour
{
    public static SideMenu Instance;

    public Image Picture;
    public Image Backdrop;
    public Text Name;
    public Text Damage;
    public Text Range;
    public Text AttackSpeed;
    public Text Level;
    public Text Health;

    public static int gameGold;
    public static int gameEXP;

    public GameObject ReviveButton;
    public GameObject fundsVoidbox;
    public Text GoldResourceText;
    public Text EXPResourceText;
    public GameObject RangeTracker;
    public TextMeshProUGUI levelupButton;
    public TextMeshProUGUI reviveButtonText;

    private Hero SelectedHero;
    private FundingWorkaround funds;

    private int moneymoney = 0;
    private int Knawledge = 0;

    public SideMenu()
    {
        Instance = this;
    }

    private void Start()
    {
        funds = GetComponent<FundingWorkaround>();
        ReviveButton.SetActive(false);
        fundsVoidbox.SetActive(false);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (SelectedHero.Health > 0)
        {
            Backdrop.color = new Color(34/255f, 139/255f, 34/255f, 255/255f); //forest green
            ReviveButton.SetActive(false);
        }
        else
        {
            Backdrop.color = Color.red;
            ReviveButton.SetActive(true);
        }

        Picture.sprite = SelectedHero.Image;
        Name.text = SelectedHero.Name;

        Damage.text = "Damage:\t\t\t" + SelectedHero.Damage.ToString();
        Range.text = "Range:\t\t\t\t" + SelectedHero.Range.ToString();
        AttackSpeed.text = "Attack Speed:\t" + SelectedHero.AttackSpeed.ToString();
        Health.text = "\t\t\t" + SelectedHero.Health.ToString();

        Level.text = "Level:\t\t\t\t" + SelectedHero.Level.ToString();

        RangeTracker.transform.position = SelectedHero.transform.position;

        RangeTracker.transform.localScale -= RangeTracker.transform.localScale;
        RangeTracker.transform.localScale += new Vector3(SelectedHero.Range, SelectedHero.Range, 1);


        changeFunds();
    }

    public void Set(Hero h)
    {
        SelectedHero = h;

        if (SelectedHero == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
    }
    public void TryToLevel()
    {
        if (gameEXP < SelectedHero.WhatIsLevelCost())
        {
            fundsVoidbox.SetActive(true);
            return;
        }
        else
        {
            gameEXP -= SelectedHero.WhatIsLevelCost();
            EXPResourceText.text = gameEXP.ToString();
            SelectedHero.HeroLevelUp();
            levelupButton.text = SelectedHero.WhatIsLevelCost().ToString();
            reviveButtonText.text = SelectedHero.WhatIsReviveCost().ToString();
        }
    }

    public void TryToRevive()
    {
        if (gameGold < SelectedHero.WhatIsReviveCost())
        {
            fundsVoidbox.SetActive(true);
            return;
        }
        else
        {
            gameGold -= SelectedHero.WhatIsReviveCost();
            GoldResourceText.text = gameGold.ToString();
            SelectedHero.Revive();
            reviveButtonText.text = SelectedHero.WhatIsReviveCost().ToString();
        }
    }

    public void CloseFundbox()  //Closed by an external object so multiple instances are not attempted to be closed.
    {
        fundsVoidbox.SetActive(false);
    }

    public void CloseSidemenu()
    {
        gameObject.SetActive(false);
    }

    public void moneyCheat()
    {
        moneymoney++;
        Debug.Log("The chest got clicked on, you have clicked on it " + moneymoney + " times.");
        if(moneymoney == 5)
        {
            addGold(5000);
            GoldResourceText.text = gameGold.ToString();
        }
    }
    
    public void EXPCheat()
    {
        Knawledge++;
        if(Knawledge == 7)
        {
            addEXP(5000);
            EXPResourceText.text = gameEXP.ToString();
        }
    }

    public void addGold(int gold)
    {
        gameGold += gold;
    }

    public void addEXP(int EXP)
    {
        gameEXP += EXP;
    }

    private void changeFunds()
    {
        gameGold += funds.collectCoinage();
        gameEXP += funds.collectExperience();

        GoldResourceText.text = gameGold.ToString();
        EXPResourceText.text = gameEXP.ToString();
    }
}
