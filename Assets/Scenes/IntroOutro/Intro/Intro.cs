using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Animator animator;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public Animation text;

    private bool cont = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroStart());
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (cont)
            {
                animator.Play("Intro3");
                Invoke("ChangeScene", 3f);
            }
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public IEnumerator IntroStart()
    {
        animator.Play("Intro");
        yield return new WaitForSeconds(7.5f);
        image1.SetActive(false);
        animator.Play("Intro");
        yield return new WaitForSeconds(7.5f);
        image2.SetActive(false);
        animator.Play("Intro2");
        text.Play();
        yield return new WaitForSeconds(2f);
        cont = true;
    }
}
