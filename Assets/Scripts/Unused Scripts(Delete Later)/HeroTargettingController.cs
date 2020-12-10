using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HeroAttackController))]
public class HeroTargettingController : MonoBehaviour
{
    // Start is called before the first frame update

    enum HeroState
        {
        Idle,
        Attacking,
        Cooldown
    }

    private HeroState myState;
    HeroAttackController attackController;
    private WaitForSeconds cooldownTime;
    private Hero chosenOne;

    void Start()
    {
        myState = HeroState.Idle;
        cooldownTime = new WaitForSeconds(1 / chosenOne.AttackSpeed);
        chosenOne = GetComponent<Hero>();
    }

    public void OnEnemyEnter()
    {
        if(myState != HeroState.Idle || chosenOne.Health == 0)
        {
            return;
        }

        TryAttack();
    }

    private void TryAttack()
    {
        if(!attackController.TryAttack())
        {
            return;
        }
        myState = HeroState.Cooldown;
        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        yield return cooldownTime;
        myState = HeroState.Idle;
        TryAttack();
    }
}
