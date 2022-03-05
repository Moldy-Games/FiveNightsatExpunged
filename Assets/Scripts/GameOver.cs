using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GoBack());
    }
    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
