using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public enum HeroType
    {
        Cleric,
        Ranger,
        Rogue,
        Warrior,
        Wizard
    }

    public HeroType MyType;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        SideMenu.Instance.Set(this);
    }
}
