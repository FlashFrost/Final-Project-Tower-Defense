using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Heroes")]
    public List<Hero> Heroes;

    public GameObject HowToPlayMenu;

    // Start is called before the first frame update
    void Start()
    {
        List<Hero> tempHeroes = Heroes.ToList();
        var random = new System.Random();

        int next = random.Next(tempHeroes.Count);
        Hero h = Instantiate(tempHeroes[next], new Vector3(-5, -2.5f, -1), Quaternion.identity);
        h.transform.Find("Detection Range").gameObject.SetActive(false);
        tempHeroes.RemoveAt(next);

        next = random.Next(tempHeroes.Count);
        h = Instantiate(tempHeroes[next], new Vector3(-6.5f, -2.5f, -1), Quaternion.identity);
        h.transform.Find("Detection Range").gameObject.SetActive(false);
        tempHeroes.RemoveAt(next);

        next = random.Next(tempHeroes.Count);
        h = Instantiate(tempHeroes[next], new Vector3(-8f, -2.5f, -1), Quaternion.identity);
        h.transform.Find("Detection Range").gameObject.SetActive(false);
        tempHeroes.RemoveAt(next);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HowToPlay()
    {
        HowToPlayMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        HowToPlayMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
