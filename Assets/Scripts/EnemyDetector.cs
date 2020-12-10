using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    Hero myController;

    private void Awake()
    {
        myController = GetComponentInParent<Hero>();
        if(myController == null)
        {
            myController = transform.parent.gameObject.AddComponent<Hero>();
            Debug.LogWarning("Warning! No controller on parent so default is added", gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.layer != 8)
       {
            return;
       }

        myController.OnEnemyEnter();
    }


}
