using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    //TODO: remove health from here
    public Text HealthText;
    public int HealthRemaining = 100;

    public static LevelController Instance;

    public LevelController()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = HealthRemaining.ToString();
    }
}
