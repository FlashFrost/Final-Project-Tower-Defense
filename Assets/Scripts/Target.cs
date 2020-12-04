using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int SequenceNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        PathingController.AddTarget(this);
    }
}
