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
            Damage += 2;
            Range += 1;
            AttackSpeed += 1;
        }
        else if(MyType == HeroType.Ranger)
        {
            //Complete these.
        }
    }
}
