using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public Button StartButton;
    public bool RunningLevel = false;

    [Header("Heroes")]
    public Hero MaleCleric;
    public Hero FemaleCleric;
    public Hero MaleRanger;
    public Hero FemaleRanger;
    public Hero MaleRogue;
    public Hero FemaleRogue;
    public Hero MaleWarrior;
    public Hero FemaleWarrior;
    public Hero MaleWizard;
    public Hero FemaleWizard;

    [Header("Enemies")]
    public EnemyController Orc;

    public LevelController()
    {
        Instance = this;
    }

    void Start()
    {
        var random = new System.Random();

        //generate cleric
        if (random.Next(2) == 1)
        {
            Instantiate(MaleCleric, new Vector3(-4, -8, -1), Quaternion.identity);
        }
        else
        {
            Instantiate(FemaleCleric, new Vector3(-4, -8, -1), Quaternion.identity);
        }

        //generate ranger
        if (random.Next(2) == 1)
        {
            Instantiate(MaleRanger, new Vector3(-2, -8, -1), Quaternion.identity);
        }
        else
        {
            Instantiate(FemaleRanger, new Vector3(-2, -8, -1), Quaternion.identity);
        }

        //generate rogue
        if (random.Next(2) == 1)
        {
            Instantiate(MaleRogue, new Vector3(0, -8, -1), Quaternion.identity);
        }
        else
        {
            Instantiate(FemaleRogue, new Vector3(0, -8, -1), Quaternion.identity);
        }

        //generate warrior
        if (random.Next(2) == 1)
        {
            Instantiate(MaleWarrior, new Vector3(2, -8, -1), Quaternion.identity);
        }
        else
        {
            Instantiate(FemaleWarrior, new Vector3(2, -8, -1), Quaternion.identity);
        }

        //generate wizard
        if (random.Next(2) == 1)
        {
            Instantiate(MaleWizard, new Vector3(4, -8, -1), Quaternion.identity);
        }
        else
        {
            Instantiate(FemaleWizard, new Vector3(4, -8, -1), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        StartButton.gameObject.SetActive(false);
        RunningLevel = true;
        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (true)
        {
            if (RunningLevel)
            {
                Instantiate(Orc, PathingController.Path[0].gameObject.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(3f);
            }            
        }
        yield break;
    }
}
