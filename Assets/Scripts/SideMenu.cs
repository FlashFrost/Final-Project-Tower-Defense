using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenu : MonoBehaviour
{
    public static SideMenu Instance;

    public Image Picture;

    [Header("Sprites")]
    public Sprite ClericSprite;
    public Sprite RangerSprite;
    public Sprite RogueSprite;
    public Sprite WarriorSprite;
    public Sprite WizardSprite;

    public SideMenu()
    {
        Instance = this;
    }
    
    void Update()
    {
        
    }

    public void Set(Hero h)
    {
        if (h.MyType == Hero.HeroType.Rogue)
        {
            Picture.sprite = RogueSprite;
        }
        else if (h.MyType == Hero.HeroType.Ranger)
        {
            Picture.sprite = RangerSprite;
        }
        else if (h.MyType == Hero.HeroType.Cleric)
        {
            Picture.sprite = ClericSprite;
        }
        else if (h.MyType == Hero.HeroType.Warrior)
        {
            Picture.sprite = WarriorSprite;
        }
        else if (h.MyType == Hero.HeroType.Wizard)
        {
            Picture.sprite = WizardSprite;
        }
    }
}
