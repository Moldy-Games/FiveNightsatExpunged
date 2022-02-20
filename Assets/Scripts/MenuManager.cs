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

    private void Start()
    {
        thingy.sprite = expungedSprites[Random.Range(0, expungedSprites.Length - 1)];
        nightText.text = $"(Night {PlayerPrefs.GetInt("Night")})";
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("Night", 1);
        OpenMenu("Loading");
        SceneManager.LoadScene("Game");
    }
    public void Continue()
    {
        OpenMenu("Loading");
        SceneManager.LoadScene("Game");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
}
