using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundingWorkaround : MonoBehaviour
{
    public static int Coinage;
    public static int Experience;

    private void Start()
    {
        Coinage = 0;
        Experience = 0;
    }

    public void setCoinage(int coins)
    {
        Coinage += coins;
    }

    public int collectCoinage ()
    {
        int moneySack = Coinage;
        Coinage = 0;
        return moneySack;
    }

    public void setExperience(int bottles)
    {
        Experience += bottles;
    }

    public int collectExperience()
    {
        int potions = Experience;
        Experience = 0;
        return potions;
    }
}
