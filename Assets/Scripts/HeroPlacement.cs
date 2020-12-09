using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlacement : MonoBehaviour
{
    public static HeroPlacement Instance;

    public List<GameObject> Locations;
    public Hero CurrentHero;

    public HeroPlacement()
    {
        Instance = this;
    }

    public void Place(StartableLocation sl)
    {
        gameObject.SetActive(false);

        CurrentHero.transform.position = sl.transform.position;
        sl.CurrentHero = CurrentHero;
        sl.gameObject.SetActive(false);
        CurrentHero.Position = sl;
        CurrentHero = null;
    }
}
