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
        
        Picture.sprite = h.Image;
        Name.text = h.Name;

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
