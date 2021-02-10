using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    // Start is called before the first frame update
    int i = 0;
    void Start()
    {

        animator.Play("Intro2");
    }

    // Update is called once per frame
    void QuitApp()
    {
        Application.Quit();
    }

    public Animator animator;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;

    public void Next()
    {
        OutroAnim(i);
        i++;
    }

    void OutroAnim(int i)
    {
        switch (i)
        {
            case 0:
                image1.SetActive(false);
                break;
            case 1:
                StartCoroutine(OutroStart());
                break;
        }
    }

    public IEnumerator OutroStart()
    {
        //animator.Play("Intro");
        //yield return new WaitForSeconds(7.5f);
        //image1.SetActive(false);
        //animator.Play("Intro");
        animator.Play("Intro4");
        yield return new WaitForSeconds(1.2f);
        image2.SetActive(false);
        yield return new WaitForSeconds(5f);
        animator.Play("Intro4");
        yield return new WaitForSeconds(1.2f);
        image3.SetActive(false);
        yield return new WaitForSeconds(5f);
        animator.Play("Intro4");
        yield return new WaitForSeconds(1.2f);
        image4.SetActive(false);
        yield return new WaitForSeconds(5f);
        animator.Play("Intro3");
        Invoke("QuitApp", 2f);
    }
}
