using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroAttackController : MonoBehaviour
{
    private Collider2D attackRadius;
    protected HeroTargettingController HeroController;
    private void Awake()
    {
        attackRadius = transform.Find("Detection Range").GetComponent<Collider2D>();
        HeroController = GetComponent<HeroTargettingController>();
    }

    public bool TryAttack()
    {
        var enemyCollidersInRange = new List<Collider2D>();
        var enemyFilter = new ContactFilter2D { layerMask = 1 << 8, useLayerMask = true };
        var numEnemiesInRange = attackRadius.OverlapCollider(enemyFilter, enemyCollidersInRange);
        if (numEnemiesInRange <= 0)
        {
            return false;
        }
        var enemiesInRange = enemyCollidersInRange.Select(enemy => enemy.GetComponent<EnemyController>()).ToList();
        EnemyController targetEnemy = DetermineTarget(enemiesInRange);
        Attack(targetEnemy);
        return true;
    }

    private EnemyController DetermineTarget(List<EnemyController> enemies)
    {
        EnemyController furthestEnemy = enemies[0];
        for(int i = 1; i < enemies.Count; i++)
        {
            furthestEnemy = EnemyController.CompareGreaterPathProgress(furthestEnemy, enemies[i]);
        }
        return furthestEnemy;
    }

    protected void Attack(EnemyController targetEnemy)
    {

    }
}
