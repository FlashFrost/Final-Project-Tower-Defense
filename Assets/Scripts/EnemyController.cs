using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static bool _gameOver = false;
    private Target _currentTarget = null;
    private bool started = false;

    public SpriteRenderer sr;
    private Animator animator;

    public bool Attacking = false;
    private bool _attackLock = false;
    public int CurrentSpeed = 5;

    public int Health = 25;
    public int MovementSpeed = 5;
    public int AttackSpeed = 10;
    public int EXPValue = 10;
    public int Damage = 5;
    public int goldValue = 1;

    public Hero CurrentHero = null;
    private FundingWorkaround resources;

    private bool delaying = false;
    private int delayTimer = 10;
    private int currentDelay = 0;
    private WaitForSeconds deathTimer;

    //Variables for this class to handle but which other classes need to know.
    public bool dead;
    private bool drained = false;
    private int targetsPassed = 0;
    private float percentToNextWaypoint = 0;

    void Start()
    {
        resources = GetComponent<FundingWorkaround>();
        animator = GetComponent<Animator>();
        deathTimer = new WaitForSeconds(1.5f);
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver) return;

        if(dead)
        {
            StartCoroutine(DeathTimer());
        }
        if (delaying)    // Let the Hero die before continuing to move.
        {
            currentDelay++;
            if(currentDelay >= delayTimer)
            {
                CurrentSpeed = MovementSpeed;
                delaying = false;
                currentDelay = 0;
            }
        }
        //start it off targeting the first target node
        if (_currentTarget == null)
        {
            if (!started)
            {
                _currentTarget = PathingController.Path[0];
                started = true;
            }
            else
            {
                return;
            }
        }

        if (CurrentHero != null)
        {
            if (CurrentHero.Health <= 0)
            {
                Attacking = false;
                delaying = true;
            }
        }

        

        var vectorToGo = (_currentTarget.transform.position - gameObject.transform.position);

        if (vectorToGo.magnitude < 0.1)
        {
            _currentTarget = PathingController.GetNextTarget(_currentTarget);
            targetsPassed++;
            if (_currentTarget == null)
            {
                _gameOver = true;
                LevelController.Instance.EndGame();
                return;
            }

            vectorToGo = (_currentTarget.transform.position - gameObject.transform.position);
        }
        percentToNextWaypoint = vectorToGo.magnitude;

        gameObject.transform.position += Vector3.Normalize(vectorToGo) * (CurrentSpeed / 1000f);
        sr.flipX = vectorToGo.x < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Detection") return;

        CurrentHero = collision.GetComponent<Hero>();
        
        if (CurrentHero.Health != 0)
        {
            CurrentSpeed = 0;

            Attacking = true;
            StartCoroutine(AttackSequence());
        }
        else
        {
            CurrentHero = null;
        }
    }

    IEnumerator AttackSequence()
    {
        if (_attackLock) yield break;
        _attackLock = true;

        while (Attacking)
        {
            if (CurrentHero == null || CurrentHero.Health == 0)
            {
                Attacking = false;
                delaying = true;
            }
            else
            {
                CurrentHero.Hit(Damage);
                animator.SetTrigger("Attack");
                if (AttackSpeed <= 0)
                {
                    Attacking = false;
                }
                else
                {
                    yield return new WaitForSeconds(20 / AttackSpeed);
                }
            }
        }

        _attackLock = false;
    }

    public static EnemyController CompareGreaterPathProgress(EnemyController enemy1, EnemyController enemy2)
    {
        if (enemy1.targetsPassed > enemy2.targetsPassed)
            return enemy1;
        if (enemy1.targetsPassed < enemy2.targetsPassed)
            return enemy2;
        if (enemy1.percentToNextWaypoint < enemy2.percentToNextWaypoint)
            return enemy1;
        if (enemy1.percentToNextWaypoint > enemy2.percentToNextWaypoint)
            return enemy2;
        else
            return enemy1;
    }

    public void getAttacked(int heroDamage)
    {
        Health -= heroDamage;
        Debug.Log("I took " + heroDamage + " Damage and have " + Health + " Health left.");
        if (Health <= 0)
        {
            dead = true;
            CurrentSpeed = 0;
            animator.SetTrigger("Death");
            StartCoroutine(DeathTimer());
            if (!drained)
            {
                resources.setCoinage(goldValue);      //I don't know why this is broken, but no matter the workaround, 
                resources.setExperience(EXPValue);
                drained = true;            
            }
        }
    }

    private IEnumerator DeathTimer()
    {
        yield return deathTimer;
        Destroy(gameObject);
    }
}
