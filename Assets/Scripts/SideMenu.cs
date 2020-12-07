using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenu : MonoBehaviour
{
    public static SideMenu Instance;

    public Image Picture;
    public Text Name;
    public Text Damage;
    public Text Range;
    public Text AttackSpeed;
    public Text Level;
    public Text Experience;

    [Header("Sprites")]
    public Sprite ClericSprite;
    public Sprite RangerSprite;
    public Sprite RogueSprite;
    public Sprite WarriorSprite;
    public Sprite WizardSprite;

    public GameObject RangeTracker;

    private Hero SelectedHero;

    public SideMenu()
    {
        Instance = this;
    }
    
    void Update()
    {
        
    }

    public void Set(Hero h)
    {
        SelectedHero = h;

        if (h == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        if (h.MyType == Hero.HeroType.Rogue)
        {
            Picture.sprite = RogueSprite;
            Name.text = "Rogue";
        }
        else if (h.MyType == Hero.HeroType.Ranger)
        {
            Picture.sprite = RangerSprite;
            Name.text = "Ranger";
        }
        else if (h.MyType == Hero.HeroType.Cleric)
        {
            Picture.sprite = ClericSprite;
            Name.text = "Cleric";
        }
        else if (h.MyType == Hero.HeroType.Warrior)
        {
            Picture.sprite = WarriorSprite;
            Name.text = "Warrior";
        }
        else if (h.MyType == Hero.HeroType.Wizard)
        {
            Picture.sprite = WizardSprite;
            Name.text = "Wizard";
        }

        Damage.text = "Damage:\t\t\t" + h.Damage.ToString();
        Range.text = "Range:\t\t\t\t" + h.Range.ToString();
        AttackSpeed.text = "Attack Speed:\t" + h.AttackSpeed.ToString();

        Level.text = "Level:\t\t\t\t" + h.Level.ToString();
        Experience.text = "Experience:\t\t" + h.Experience.ToString();

        RangeTracker.transform.position = h.transform.position;

        RangeTracker.transform.localScale -= RangeTracker.transform.localScale;
        RangeTracker.transform.localScale += new Vector3(h.Range, h.Range, 1);
    }

}
