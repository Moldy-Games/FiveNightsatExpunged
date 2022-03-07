using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    Animator animator;
    private string sceneDuplicate;
    public bool isFading = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void FadeToLevel(string level)
    {
        isFading = true;
        sceneDuplicate = level;
        animator.SetTrigger("fadeout");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneDuplicate);
    }
}
