using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Menu[] menus;

    public Sprite[] expungedSprites;
    public SpriteRenderer thingy;

    public TMP_Text nightText;
    public Fade fadeThingy;

    private void Start()
    {
        nightText.text = $"(Night {PlayerPrefs.GetInt("Night")})";
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("Night", 1);
        fadeThingy.FadeToLevel("Game");
    }
    public void Continue()
    {
        fadeThingy.FadeToLevel("Game");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void ChangeNight(int night)
    {
        PlayerPrefs.SetInt("Night", night);
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
}
