using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        myState = HeroState.Idle;
    }

    public void OnEnemyEnter()
    {
        if(myState != HeroState.Idle)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
