using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenu : MonoBehaviour
{
    public static SideMenu Instance;

    public Image Picture;

    public Sprite RogueSprite;

    public SideMenu()
    {
        Instance = this;
    }
    
    void Update()
    {
        
    }

    public void SetRogue()
    {
        Picture.sprite = RogueSprite;
    }
}
