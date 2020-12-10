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
    public int HealAbility = 0;
    public float ReviveSpeed;

    //Connected Game Objects
    public StartableLocation Position;
    public GameObject myDetectionRing;
    public TextMeshProUGUI levelupCostText;
    public TextMeshProUGUI GoldresourceText;
    public TextMeshProUGUI EXPResourceText;
    public TextMeshProUGUI reviveCostText;
    public GameObject reviveButton;
    public GameObject fundsVoidbox;
    
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
    private bool healing = false;
    private bool attacking = false;
    private bool healLocked = false;

    private HeroState myState;
    public HeroType MyType;
    private HeroAttackController attackController;
    private EnemyController attackedEnemy;

    private void Start()
    {
        myDetectionRing.transform.localScale = new Vector3(Range, Range, 1);
        animator = GetComponent<Animator>();
        MaxHealth = Health;
        myState = HeroState.Idle;
        cooldownTime = new WaitForSeconds(1 / AttackSpeed);
        attackController = GetComponent<HeroAttackController>();
        reviveButton.SetActive(false);
        fundsVoidbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            reviveButton.SetActive(true);
        }
        else
        {
            reviveButton.SetActive(false);
        }
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

    public void CloseFundbox()
    {
        fundsVoidbox.SetActive(false);
    }

    public void TryToLevel()
    {
        if(EXP < levelCost)
        {
            fundsVoidbox.SetActive(true);
            return;
        }
        else
        {
            EXP -= levelCost;
            EXPResourceText.text = EXP.ToString();
            HeroLevelUp();
        }
    }
    public void TryToRevive()
    {
        if(gold < reviveCost)
        {
            fundsVoidbox.SetActive(true);
            return;
        }
        else
        {
            gold -= reviveCost;
            GoldresourceText.text = gold.ToString();
            Revive();
        }
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
    }

    private void Revive()
    {
        Health = MaxHealth;
        dead = false;
        reviveCost += 5;
        animator.SetTrigger("Reviving");
    }
    public void Hit(int damage)
    {
        Health -= damage;
        dead = true;
        if (Health <= 0)
        {
            Health = 0;
            animator.SetTrigger("Death");
        }
    }

    //Begin Combat handling code.
    public void OnEnemyEnter()
    {
        if (myState != HeroState.Idle || Health <= 0 || dead == true)
        {
            return;
        }

        TryAttack();
    }
    private void TryAttack()
    {
        attackedEnemy = attackController.TryAttack();
        if (attackedEnemy == null || dead)
        {
            return;
        }
        animator.SetTrigger("Attack");
        myState = HeroState.Cooldown;
        if(attackedEnemy.IsDead())
        {
            gold += attackedEnemy.goldValue;
            EXP += attackedEnemy.EXPValue;
        }
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
                healing = true;
                healLocked = true;
                heroes[i].Health += HealAbility;
                StartCoroutine(CooldownTimer());
            }
            else
            {
                healing = false;
            }
        }
    }

    private IEnumerator CooldownTimer()
    {
        yield return cooldownTime;
        myState = HeroState.Idle;
        if (attacking)
        {
            TryAttack();
            healLocked = false;
        }
        else if (healing)
        {
            TryHealing();
        }
    }
}
