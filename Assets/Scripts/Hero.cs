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
    public int MaxHealth;
    public int Health;
    public float AttackSplashRange;
    public int HealAbility = 0;

    public StartableLocation Position;
    public GameObject myDetectionRing;
    
    private int reviveCost = 50;
    private Animator animator;
    private bool Gender;
    private bool attacking;
    private const int monsterLayer = 8;

    private void Start()
    {
        myDetectionRing.transform.localScale = new Vector3(Range, Range, 1);
        animator = GetComponent<Animator>();
        MaxHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            CheckNearObjects();
        }

        if (attacking)
        {

        }
    }

    void CheckNearObjects()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, Range);

        foreach (Collider unit in objects)
        {
            if (unit.tag == "Enemy")
            {
                //There are enemies in range, Choose a target to attack.
            }
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

    public void HeroLevelUp()
    {
        if(MyType == HeroType.Cleric)
        {
            MaxHealth += 10;
            Health += 10;
            Damage += 1;
            Level += 1;
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
            if (Level >= 1)
            {
                AttackSplashRange += 0.25f;
                MaxHealth += 10;
                Health += 10;
                Damage += 2;
            }
        }
        else if(MyType == HeroType.Rogue)
        {
            MaxHealth += 3;
            Health += 3;
            Damage += 1;
            Level += 1;
            if (Level >= 1)
            {
                AttackSpeed += 3;
            }
        }
        else if(MyType == HeroType.Wizard)
        {
            MaxHealth += 1;
            Health += 1;
            Level += 1;
            if (Level >= 1)
            {
                Damage += 10;
                AttackSplashRange += 1;
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
