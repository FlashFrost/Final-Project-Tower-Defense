using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartableLocation : MonoBehaviour
{
    public Hero CurrentHero;

    public void OnMouseDown()
    {
        HeroPlacement.Instance.Place(this);        
    }    
}
