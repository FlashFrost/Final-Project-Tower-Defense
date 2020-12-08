using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public enum HeroType
    {
        Cleric,
        Ranger,
        Rogue,
        Warrior,
        Wizard
    }

    public HeroType MyType;

    public Sprite Image;
    public string Name;
    public int Damage;
    public int Range;
    public int AttackSpeed;
    public int Level;
    public int Experience;
    public int Health;
    public int AttackSplashRange;
    public int HealAbility = 0;
    private int reviveCost = 50;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        SideMenu.Instance.Set(this);
    }

    public void HeroLevelUp()
    {
        if(MyType == HeroType.Cleric)
        {
            Health += 15;
            Damage += 2;
            Range += 1;
            AttackSpeed += 1;
            Level += 1;
            if(Level >= 1)
            {
                HealAbility += 5;
            }
        }
        else if(MyType == HeroType.Ranger)
        {
            //Complete these.
        }
    }
}
