using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Target _currentTarget = null;
    private bool started = false;

    public SpriteRenderer sr;

    public int BaseDamage = 0;
    public int Health = 25;
    public float movementSpeed = 0.05f;
    public int EXPValue = 10;

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

        gameObject.transform.position += Vector3.Normalize(vectorToGo) * 0.005f;
        sr.flipX = vectorToGo.x < 0;
    }
}
