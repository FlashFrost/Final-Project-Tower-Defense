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
    public Text Health;

    public GameObject RangeTracker;

    private Hero SelectedHero;

    public SideMenu()
    {
        Instance = this;
    }
    
    void Update()
    {
        Picture.sprite = SelectedHero.Image;
        Name.text = SelectedHero.Name;

        Damage.text = "Damage:\t\t\t" + SelectedHero.Damage.ToString();
        Range.text = "Range:\t\t\t\t" + SelectedHero.Range.ToString();
        AttackSpeed.text = "Attack Speed:\t" + SelectedHero.AttackSpeed.ToString();
        Health.text = "\t\t\t" + SelectedHero.Health.ToString();

        Level.text = "Level:\t\t\t\t" + SelectedHero.Level.ToString();

        RangeTracker.transform.position = SelectedHero.transform.position;

        RangeTracker.transform.localScale -= RangeTracker.transform.localScale;
        RangeTracker.transform.localScale += new Vector3(SelectedHero.Range, SelectedHero.Range, 1);
    }

    public void Set(Hero h)
    {
        SelectedHero = h;

        if (SelectedHero == null)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
    }

}
