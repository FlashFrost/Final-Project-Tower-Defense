﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
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

    private bool delaying = false;
    private int delayTimer = 10;
    private int currentDelay = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(delaying)    // Let the Hero die before continuing to move.
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

        var vectorToGo = (_currentTarget.transform.position - gameObject.transform.position);

        if (vectorToGo.magnitude < 0.1)
        {
            _currentTarget = PathingController.GetNextTarget(_currentTarget);
            if (_currentTarget == null)
            {
                //Player looses, code needs to go here
                return;
            }

            vectorToGo = (_currentTarget.transform.position - gameObject.transform.position);
        }

        gameObject.transform.position += Vector3.Normalize(vectorToGo) * (CurrentSpeed / 1000f);
        sr.flipX = vectorToGo.x < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") return;

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
}
