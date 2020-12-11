using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(HeroAttackController))]

public class Hero : MonoBehaviour
{
    public enum HeroType
    {
        Cleric,
        Ranger,
        Rogue,
        Warrior,
        Wizard,
        NotAHero
    }

    enum HeroState
    {
        Idle,
        Attacking,
        Cooldown,
        Healing
    }


    //External character resources
    public Sprite Image;
    public string Name;

    //Stats
    public int Damage;
    public int Range;
    public float AttackSpeed; //Number of attacks per second.
    public int Level;
    public int Experience;
    public int MaxHealth;
    public int Health;
    private int Armor = 0;
    private int HealAbility = 0;
    public float ReviveSpeed;

    //Connected Game Objects
    public StartableLocation Position;
    public GameObject myDetectionRing;
    
    //Internal Variables
    private int reviveCost = 50;
    private int levelCost = 100;
    private Animator animator;
    private WaitForSeconds cooldownTime;
    private const int monsterLayer = 8;

    //Universal Variables
    private static int gold;
    private static int EXP;

    //Booleans to determine accessibility to functions.
    private bool dead = false;
    private bool healLocked = false;

    private HeroState myState;
    public HeroType MyType;
    private HeroAttackController attackController;

    private void Start()
    {
        myDetectionRing.transform.localScale = new Vector3(Range, Range, 1);
        animator = GetComponent<Animator>();
        MaxHealth = Health;
        myState = HeroState.Idle;
        cooldownTime = new WaitForSeconds(1 / AttackSpeed);
        attackController = GetComponent<HeroAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MyType == HeroType.Cleric && myState == HeroState.Idle && healLocked == false)
        {
            TryHealing();
        }
    }

    void OnMouseDown()
    {
        if (LevelController.Instance.RunningLevel)
        {
            SideMenu.Instance.Set(this);
        }
        else if (Position == null)
        {
            HeroPlacement.Instance.gameObject.SetActive(true);
            HeroPlacement.Instance.CurrentHero = this;
        }
    }

    public int WhatIsReviveCost()
    {
        return reviveCost;
    }

    public int WhatIsLevelCost()
    {
        return levelCost;
    }

    public void HeroLevelUp()
    {
        levelCost += 50;
        reviveCost += 25;
        if(MyType == HeroType.Cleric)
        {
            MaxHealth += 10;
            Health += 10;
            Damage += 1;
            Level += 1;
            AttackSpeed += .25f;
            if(Level >= 1)
            {
                HealAbility += 5;
            }
        }
        else if(MyType == HeroType.Ranger)
        {
            MaxHealth += 5;
            Health += 5;
            Damage += 2;
            Level += 1;
            AttackSpeed += .75f;
            if (Level >= 1)
            {
                Range += 1;
                myDetectionRing.transform.localScale = new Vector3(Range, Range, 1);
            }
        }
        else if(MyType == HeroType.Warrior)
        {
            MaxHealth += 15;
            Health += 15;
            Damage += 2;
            Level += 1;
            AttackSpeed += .5f;
            if (Level >= 1)
            {
                MaxHealth += 10;
                Health += 10;
                Armor += 1;
                Damage += 2;
            }
        }
        else if(MyType == HeroType.Rogue)
        {
            MaxHealth += 3;
            Health += 3;
            Damage += 1;
            Level += 1;
            AttackSpeed += .25f;
            if (Level >= 1)
            {
                AttackSpeed += 1f;
            }
        }
        else if(MyType == HeroType.Wizard)
        {
            MaxHealth += 1;
            Health += 1;
            Level += 1;
            AttackSpeed += .25f;
            if (Level >= 1)
            {
                Damage += 15;
            }
        }

        cooldownTime = new WaitForSeconds(1 / AttackSpeed);
    }

    public void Revive()
    {
        Health = MaxHealth;
        dead = false;
        reviveCost += 5;
        animator.SetTrigger("Reviving");
    }
    public void Hit(int damage)
    {
        Health -= (damage - Armor);
        if (Health <= 0)
        {
            dead = true;
            Health = 0;
            animator.SetTrigger("Death");
            AudioController.Instance.PlayHeroDeath();
        }
    }

    //Begin Combat handling code.
    public void OnEnemyEnter()
    {
        if (myState != HeroState.Idle || dead == true)
        {
            return;
        }

        TryAttack();
    }
    private void TryAttack()
    {
        if (!attackController.TryAttack() || dead)
        {
            return;
        }
        animator.SetTrigger("Attack");
        myState = HeroState.Cooldown;
        StartCoroutine(CooldownTimer());
    }

    private void TryHealing()
    {
        Hero[] heroes = FindObjectsOfType<Hero>();
        for (int i = 0; i < heroes.Length; i++)
        {
            if (heroes[i].Health > 0 && heroes[i].Health < heroes[i].MaxHealth )
            {
                myState = HeroState.Healing;
                healLocked = true;
                heroes[i].Health += HealAbility;
                animator.SetTrigger("Interrupt");
                StartCoroutine(HealCooldownTimer());
            }
        }
    }

    private IEnumerator CooldownTimer()
    {
        yield return cooldownTime;
        myState = HeroState.Idle;
        healLocked = false;
        TryAttack();
    }    

    private IEnumerator HealCooldownTimer()
    {
        yield return cooldownTime;
        myState = HeroState.Idle;
        TryHealing();
    }

}
