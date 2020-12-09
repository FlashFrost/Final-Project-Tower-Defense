﻿using System.Collections;
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
    public int MaxHealth;
    public int Health;
    public int AttackSplashRange;
    public int HealAbility = 0;

    private int reviveCost = 50;
    private Animator animator;
    private bool Gender;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
            MaxHealth += 15;
            Health += 15;
            Damage += 2;
            Level += 1;
            if(Level >= 1)
            {
                HealAbility += 5;
            }
        }
        else if(MyType == HeroType.Ranger)
        {
            MaxHealth += 15;
            Health += 15;
            Damage += 2;
            AttackSpeed += 1;
            Level += 1;
            if (Level >= 1)
            {
                Range += 1;
            }
        }
        else if(MyType == HeroType.Warrior)
        {
            MaxHealth += 15;
            Health += 15;
            Damage += 2;
            Range += 1;
            AttackSpeed += 1;
            Level += 1;
            if (Level >= 1)
            {
                HealAbility += 5;
            }
        }
        else if(MyType == HeroType.Rogue)
        {
            MaxHealth += 15;
            Health += 15;
            Damage += 2;
            Range += 1;
            AttackSpeed += 1;
            Level += 1;
            if (Level >= 1)
            {
                HealAbility += 5;
            }
        }
        else if(MyType == HeroType.Wizard)
        {
            MaxHealth += 15;
            Health += 15;
            Damage += 2;
            Range += 1;
            AttackSpeed += 1;
            Level += 1;
            if (Level >= 1)
            {
                HealAbility += 5;
            }
        }
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            animator.SetTrigger("Death");
        }
    }
}
