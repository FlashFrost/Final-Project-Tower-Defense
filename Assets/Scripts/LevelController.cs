using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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

    private List<Hero> _heroes;

    public LevelController()
    {
        Instance = this;
    }

    void Start()
    {
        StartButton.enabled = false;
        var random = new System.Random();
        _heroes = new List<Hero>();

        //generate cleric
        if (random.Next(2) == 1)
        {
            _heroes.Add(Instantiate(MaleCleric, new Vector3(-4, -8, -1), Quaternion.identity));
        }
        else
        {
            _heroes.Add(Instantiate(FemaleCleric, new Vector3(-4, -8, -1), Quaternion.identity));
        }

        //generate ranger
        if (random.Next(2) == 1)
        {
            _heroes.Add(Instantiate(MaleRanger, new Vector3(-2, -8, -1), Quaternion.identity));
        }
        else
        {
            _heroes.Add(Instantiate(FemaleRanger, new Vector3(-2, -8, -1), Quaternion.identity));
        }

        //generate rogue
        if (random.Next(2) == 1)
        {
            _heroes.Add(Instantiate(MaleRogue, new Vector3(0, -8, -1), Quaternion.identity));
        }
        else
        {
            _heroes.Add(Instantiate(FemaleRogue, new Vector3(0, -8, -1), Quaternion.identity));
        }

        //generate warrior
        if (random.Next(2) == 1)
        {
            _heroes.Add(Instantiate(MaleWarrior, new Vector3(2, -8, -1), Quaternion.identity));
        }
        else
        {
            _heroes.Add(Instantiate(FemaleWarrior, new Vector3(2, -8, -1), Quaternion.identity));
        }

        //generate wizard
        if (random.Next(2) == 1)
        {
            _heroes.Add(Instantiate(MaleWizard, new Vector3(4, -8, -1), Quaternion.identity));
        }
        else
        {
            _heroes.Add(Instantiate(FemaleWizard, new Vector3(4, -8, -1), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("MenuScene");
        }

        if (_heroes.Count(x => x.Position == null) == 0)
        {
            StartButton.enabled = true;
            StartButton.image.color = Color.yellow;
        }
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
