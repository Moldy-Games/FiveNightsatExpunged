using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    Animator animator;
    private string sceneDuplicate;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeToLevel(string level)
    {
        sceneDuplicate = level;
        animator.SetTrigger("fadeout");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneDuplicate);
    }
}
