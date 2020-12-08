using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Target _currentTarget = null;
    private bool started = false;

    public SpriteRenderer sr;
    public Animator animator;

    public bool Attacking = false;

    public int BaseDamage = 0;
    public int Health = 25;
    public int movementSpeed = 5;
    public int AttackSpeed = 10;
    public int EXPValue = 10;
    public int HeroDamage = 5;
    public int goldValue = 1;

    public Hero CurrentTarget = null;

    // Update is called once per frame
    void Update()
    {
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
                //than hit the end
                LevelController.Instance.HitBase(BaseDamage);
                return;
            }
            vectorToGo = (_currentTarget.transform.position - gameObject.transform.position);
        }

        gameObject.transform.position += Vector3.Normalize(vectorToGo) * (movementSpeed / 1000f);
        sr.flipX = vectorToGo.x < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movementSpeed = 0;

        //Debug.Log(collision.GetComponent<Collider>().gameObject.name);

        Attacking = true;
        StartCoroutine(StartAttackSequence());
    }

    IEnumerator StartAttackSequence()
    {
        while (Attacking)
        {
            //TODO: actually deal damage to hero here
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
}
