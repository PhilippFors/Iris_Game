using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Office : MonoBehaviour
{

    public GameObject MonologueObj;

    public GameObject phone;
    public GameObject computer;
    public GameObject window;
    public GameObject jacket;
    public GameObject keyboard;
    public GameObject mouse;
    public GameObject door;

    public GameObject interact;

    public GameObject keyboardCollider1;
    public GameObject keyboardCollider2;
    public GameObject keyboardCollider3;

    public bool voicemail = false;

    public int Noise = 0;

    public AudioClip[] officeSounds;

    public void CheckObject(GameObject o)
    {
        if (o.CompareTag(GameManager.selectableTag))
        {

            if (o.name.Equals(phone.name) & !GameManager.Instance.phoneRinging)
            {
                UIManager.Instance.SetSubtitle("That's the phone. I don't need it right now.");

            }

            if (o.name.Equals(phone.name) & GameManager.Instance.phoneRinging)
            {

                GameManager.StopAudio(phone);
                GameManager.Instance.pickedUpPhone = true;
                GameManager.PlayAudio(MonologueObj, officeSounds, 12);
                StartCoroutine(Wait(false, 1f));
            }

            if (o.name.Equals(computer.name) & !GameManager.Instance.workFinished & !GameManager.Instance.computerOn & GameManager.Instance.coffeeMade)
            {
                GameManager.Instance.computerOn = true;
                GameManager.StopAudio(MonologueObj);
                GameManager.PlayAudio(MonologueObj, officeSounds, 3);
                UIManager.Instance.SetSubtitle("Now I need to finish my research and sent it via email. Let's start typing!");
                UIManager.Instance.SetTask("Start writing your article.");
                window.GetComponent<AudioSource>().volume = 1.2f;
                StartCoroutine(Wait(false, 10f));
            }

            if (o.name.Equals(computer.name) & GameManager.Instance.emailSent & GameManager.Instance.computerOn)
            {
                GameManager.Instance.computerOn = false;
                UIManager.Instance.invoke = 15.0f;
                GameManager.PlayAudio(MonologueObj, officeSounds, 15);
                GameManager.StopAudio(computer);
                UIManager.Instance.SetSubtitle("Ok, let's put on our jacket. It should be on the cloth rack next to the door. If I can find the drawer outside the office, I will know where to go next.");
                UIManager.Instance.SetTask("Find the cloth rack and put on your jacket.");

            }
            if (o.name.Equals(computer.name) & GameManager.Instance.emailSent & !GameManager.Instance.computerOn)
            {
                UIManager.Instance.SetSubtitle("Ok, let's put on our jacket. It should be on the cloth rack next to the door. If I can find the drawer outside the office, I will know where to go next.");
                UIManager.Instance.SetTask("Find the cloth rack and put on your jacket.");

            }

            if (o.name.Equals(keyboard.name) & GameManager.Instance.computerOn & !GameManager.Instance.windowClosed & Noise == 0)
            {
                keyboardCollider1.SetActive(true);
                keyboardCollider2.SetActive(true);
                keyboardCollider3.SetActive(true);
                GameManager.PlayAudio(keyboard);
                Noise++;

            }

            if (o.name.Equals(keyboard.name) & !GameManager.Instance.windowClosed & !GameManager.Instance.computerOn)
            {
                UIManager.Instance.SetSubtitle("That's the keyboard. I need to switch on the computer first.");

            }

            if (o.name.Equals(keyboard.name) & GameManager.Instance.workFinished)
            {
                UIManager.Instance.SetSubtitle("That's the keyboard. I need to send the email and switch off the computer.");

            }

            if (o.name.Equals(keyboard.name) & GameManager.Instance.windowClosed & Noise == 3 & GameManager.Instance.mumCalled == true)
            {
                keyboardCollider1.SetActive(true);
                keyboardCollider2.SetActive(true);
                keyboardCollider3.SetActive(true);
                GameManager.PlayAudio(keyboard);
                GameManager.Instance.workFinished = true;
                StartCoroutine(Wait(false, 9f));

            }

            if (o.name.Equals(window.name) & Noise == 2)
            {
                GameManager.Instance.windowClosed = true;
                GameManager.StopAudio(MonologueObj);
                GameManager.PlayAudio(MonologueObj, officeSounds, 8);
                GameManager.StopAudio(window);
                UIManager.Instance.SetSubtitle("Finally some quiet.");
                GameManager.Instance.phoneRinging = true;
                StartCoroutine(Wait(false, 3f));

            }

            if (o.name.Equals(window.name) & Noise != 2)
            {
                UIManager.Instance.SetSubtitle("That's the window.");
            }

            if (o.name.Equals(mouse.name))
            {

                if (GameManager.Instance.workFinished & Noise == 5 & !GameManager.Instance.emailSent)
                {
                    GameManager.PlayAudio(MonologueObj, officeSounds, 11);
                    GameManager.Instance.emailSent = true;
                    UIManager.Instance.SetSubtitle("Let's switch off the computer.");
                    UIManager.Instance.SetTask("Switch off the computer.");
                }
                else
                {
                    UIManager.Instance.SetSubtitle("That's the mouse.");
                    GameManager.PlayAudio(mouse);
                }

            }

            Debug.Log(o.name.Equals(jacket.name) +",  " + o.name);
            
            Debug.Log(GameManager.Instance.emailSent);
            if (o.name.Equals(jacket.name) & GameManager.Instance.emailSent)
            {
                GameManager.Instance.jacketOn = true;
                GameManager.PlayAudio(jacket);
                UIManager.Instance.SetSubtitle("Alright let's answer the door.");
                UIManager.Instance.SetTask("Answer the door.");

            }

            if (o.name.Equals(door.name) & GameManager.Instance.jacketOn)
            {
                GameManager.PlayAudio(door);
                Invoke("LevelEnd", 1f);
            }

        }
        else if (o.CompareTag(GameManager.notSelectable))
        {
            GameManager.PlayAudio(interact);
            if (o.name.Equals("drawer"))
            {
                if (GameManager.Instance.emailSent)
                {
                    UIManager.Instance.SetSubtitle("The clothe rack is down the corridor on the left side of the drawer.");
                }
                else
                {
                    UIManager.Instance.SetSubtitle("That's the drawer with the tiny cactus on it. The office door is on it's left.");
                }
            }

            if (o.name.Equals("shoeRack"))
            {
                UIManager.Instance.SetSubtitle("That's the shoe rack.");
            }

            if (o.name.Equals("cupboard"))
            {
                UIManager.Instance.SetSubtitle("That's the office cuboard. I store all my stationery in it. Right now I don't need anything from it.");
            }

            if (o.name.Equals("desk"))
            {
                UIManager.Instance.SetSubtitle("That's my desk. On it are my computer and the phone.");
            }

            if (o.name.Equals("OfficeChair"))
            {
                UIManager.Instance.SetSubtitle("That's the office chair in front of the desk.");
            }

            if (o.name.Equals("Screen"))
            {
                UIManager.Instance.SetSubtitle("That's the computer screen.");
            }

        }
    }

    public void LevelStart()
    {

        if (GameManager.Instance.level == 3)
        {
            if (GameManager.Instance.phoneRinging)
            {
                GameManager.PlayAudio(phone);
                UIManager.Instance.SetSubtitle("Pick up the phone!");
                UIManager.Instance.SetTask("Find the office and pick up the phone.");

                StartCoroutine(Wait(false, 4f));

            }
        }

    }


    public void LevelEnd()
    {
        GameManager.Instance.SetState(GameManager.GameState.newLevel);
    }


    IEnumerator Wait(bool status, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (voicemail & GameManager.Instance.phoneRinging & Noise == 0)
        {
            GameManager.PlayAudio(MonologueObj, officeSounds, 0);
        }
        if (!GameManager.Instance.phoneRinging & !GameManager.Instance.computerOn)
        {
            GameManager.StopAudio(MonologueObj);
            GameManager.PlayAudio(MonologueObj, officeSounds, 2);
            UIManager.Instance.SetSubtitle("Alright, let's turn on the computer.");
            UIManager.Instance.SetTask("Turn on the computer.");
        }
        if (GameManager.Instance.computerOn & Noise == 0)
        {
            GameManager.PlayAudio(computer);
        }
        if (!GameManager.Instance.windowClosed & Noise == 1)
        {
            Noise++;
            keyboardCollider1.SetActive(false);
            keyboardCollider2.SetActive(false);
            keyboardCollider3.SetActive(false);
            UIManager.Instance.SetSubtitle("Damn, these kids. I should close the window!");
            UIManager.Instance.SetTask("Close the window.");
            GameManager.StopAudio(MonologueObj);
            GameManager.StopAudio(keyboard);
            GameManager.PlayAudio(computer);
            GameManager.PlayAudio(MonologueObj, officeSounds, 5);
            yield return new WaitForSeconds(12f);
            if (!GameManager.Instance.windowClosed)
            {
                GameManager.PlayAudio(MonologueObj, officeSounds, 6);
            }
            yield return new WaitForSeconds(12f);
            if (!GameManager.Instance.windowClosed)
            {
                GameManager.PlayAudio(MonologueObj, officeSounds, 7);
            }


        }
        if (GameManager.Instance.workFinished & Noise == 3)
        {
            GameManager.PlayAudio(MonologueObj, officeSounds, 9);
            Noise++;
            yield return new WaitForSeconds(3f);
            GameManager.PlayAudio(MonologueObj, officeSounds, 10);
            yield return new WaitForSeconds(6f);
            keyboardCollider1.SetActive(false);
            keyboardCollider2.SetActive(false);
            keyboardCollider3.SetActive(false);
            Noise++;
            UIManager.Instance.SetSubtitle("That's done! Let's send the email and and switch off the computer.");
            UIManager.Instance.SetTask("Send the email.");

        }
        if (GameManager.Instance.windowClosed & Noise == 2)
        {
            Noise++;
            GameManager.PlayAudio(phone);
            yield return new WaitForSeconds(1f);
            UIManager.Instance.SetSubtitle("Who could that be?");
            UIManager.Instance.SetTask("Pick up the phone.");
        }

        if (GameManager.Instance.phoneRinging & GameManager.Instance.pickedUpPhone == true)
        {
            GameManager.Instance.phoneRinging = false;
            GameManager.StopAudio(MonologueObj);
            GameManager.PlayAudio(MonologueObj, officeSounds, 4);
            yield return new WaitForSeconds(6f);
            GameManager.PlayAudio(MonologueObj, officeSounds, 13);
            yield return new WaitForSeconds(2f);
            GameManager.PlayAudio(MonologueObj, officeSounds, 14);
            GameManager.Instance.mumCalled = true;
            UIManager.Instance.SetSubtitle("Ok. Let's finish the article.");
            UIManager.Instance.SetTask("Finish the article.");
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.phoneRinging)
        {
            GameManager.Instance.phoneRinging = false;
            GameManager.StopAudio(phone);
            GameManager.StopAudio(MonologueObj);
            GameManager.PlayAudio(MonologueObj, officeSounds, 1);
            UIManager.Instance.SetSubtitle("Oh no, it went to voicemail.");
            window.GetComponent<AudioSource>().volume = 0.2f;
            GameManager.PlayAudio(window);
            voicemail = true;
            StartCoroutine(Wait(false, 17f));
        }
    }

}
