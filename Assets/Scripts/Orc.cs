using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    private Target _currentTarget = null;
    private bool started = false;

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
            if (_currentTarget == null) return;
            vectorToGo = (_currentTarget.transform.position - gameObject.transform.position);
        }

        gameObject.transform.position += Vector3.Normalize(vectorToGo) * 0.005f;
    }
}
