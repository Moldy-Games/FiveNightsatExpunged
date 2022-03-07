using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Menu[] menus;

    public TMP_Text nightText;
    public Fade fadeThingy;

    public TMP_InputField difficulty;

    public Animator newGame;
    public GameObject customNightButton;

    private void Start()
    {
        nightText.text = $"(Night {PlayerPrefs.GetInt("Night")})";
        if(PlayerPrefs.GetInt("GameComplete") == 1)
        {
            customNightButton.SetActive(true);
        }
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("Night", 1);
        PlayerPrefs.SetInt("CustomNight", 0);
        StartCoroutine(NewGameSequence());
    }
    public void Continue()
    {
        PlayerPrefs.SetInt("CustomNight", 0);
        fadeThingy.FadeToLevel("Game");
    }
    public void CustomNight()
    {
        PlayerPrefs.SetInt("CustomNight", 1);
        PlayerPrefs.SetInt("Difficulty", int.Parse(difficulty.text));
        fadeThingy.FadeToLevel("Game");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Delete) && !fadeThingy.isFading)
        {
            PlayerPrefs.SetInt("Night", 1);
            PlayerPrefs.SetFloat("MouseSens", 1);
            PlayerPrefs.SetInt("PostProcessing", 1);
            PlayerPrefs.SetInt("GameComplete", 0);
            fadeThingy.FadeToLevel("Menu");
        }
    }
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                OpenMenu(menus[i]);
            }
            else if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }
    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if(menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
    IEnumerator NewGameSequence()
    {
        newGame.SetTrigger("newGame");
        yield return new WaitForSeconds(6);
        fadeThingy.FadeToLevel("Game");
    }
}
