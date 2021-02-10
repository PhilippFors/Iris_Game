
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : MonoBehaviour
{
    public static int counter;
    public static int clockcounter;

    public CharacterController Iris;

    public new GameObject collider;

    public GameObject MonologueObj;
    public GameObject clock;
    public GameObject closet;
    public GameObject bed;

    public GameObject interact;

    public AudioClip[] bedroomSounds;

    public Animator bedCameraBody;


    public void CheckObject(GameObject o)
    {
        if (o.CompareTag(GameManager.selectableTag))
        {

            if (o.name.Equals(clock.name))
            {
                StopAlarm(o);
            }

            if (o.name.Equals(closet.name))
            {

                if (GameManager.Instance.alarmClockState)
                {
                    UIManager.Instance.SetSubtitle("That's the closet. The clock is next to the bed on the windowside.");

                }
                else if (!GameManager.Instance.clothesOn)
                {
                    GameManager.PlayAudio(closet, bedroomSounds, 12);
                    Invoke("GetDressed", 3f);
                }

            }

        }
        else if (o.CompareTag(GameManager.notSelectable))
        {
            GameManager.PlayAudio(interact);
            if (o.name.Equals("aquarium"))
            {
                UIManager.Instance.SetSubtitle("That's the aquarium. The closet is behind you.");

            }

            if (o.name.Equals("lamp"))
            {
                if (GameManager.Instance.alarmClockState)
                {
                    UIManager.Instance.SetSubtitle("That's the lamp. The clock should be next to it.");
                }
                else
                {
                    UIManager.Instance.SetSubtitle("That's the lamp. The closet is behind you.");
                }

            }
            if (o.name.Equals("nightStand"))
            {
                if (GameManager.Instance.alarmClockState)
                {
                    UIManager.Instance.SetSubtitle("That's the night stand. The clock is on top of it");
                }
                else
                {
                    UIManager.Instance.SetSubtitle("That's the night stand. The closet is behind you.");
                }

            }

            if (o.name.Equals("bett_bettgestell"))
            {
                if (GameManager.Instance.alarmClockState)
                {
                    UIManager.Instance.SetSubtitle("That's the bed. The clock is on the windowside.");
                }
                else
                {
                    UIManager.Instance.SetSubtitle("That's the bed. The closet is next to the door.");
                }

            }

            if (o.name.Equals("vitrine"))
            {
                UIManager.Instance.SetSubtitle("That's the showcase. The closet is next to the door.");
            }

            if (o.name.Equals("schlafzimmerfensterGlass"))
            {
                UIManager.Instance.SetSubtitle("That's the window.");

            }

        }
    }

    public void LevelStart()
    {

        if (GameManager.Instance.level == 1)
        {
            if (GameManager.Instance.alarmClockState)
            {
                Debug.Log("Alarm");
                GameManager.PlayAudio(clock, clock.GetComponent<AudioSource>().clip);
                StartCoroutine("Wait", 4f);
                UIManager.Instance.SetSubtitle("Put out the alarm!");
                UIManager.Instance.SetTask("Put the alarm out.");


            }
        }
        else if (GameManager.Instance.level == 2)
        {
            if (GameManager.Instance.alarmClockState)
            {
                GameManager.PlayAudio(clock);
                StartCoroutine("Wait", 4f);
                UIManager.Instance.SetSubtitle("Put out the alarm.");
                UIManager.Instance.SetTask("Put the alarm out.");

            }
        }

        else if (GameManager.Instance.level == 3)
        {
            GameManager.PlayAudio(clock);
            UIManager.Instance.SetSubtitle("Go to the kitchen to get some coffee.");
            UIManager.Instance.SetTask("Go to the kitchen to get some coffee.");

        }
    }

    public void StopAlarm(GameObject o)
    {

        if (GameManager.Instance.level == 1)
        {

            if (GameManager.Instance.alarmClockState)
            {
                GameManager.StopAudio(MonologueObj);

                UIManager.Instance.SetSubtitle("Alarm has stopped.");
                GameManager.Instance.alarmClockState = false;
                GameManager.StopAudio(o);
                GameManager.PlayAudio(bed);
                GameManager.PlayAudio(MonologueObj, bedroomSounds, 6);
                StartCoroutine("Wait", 4f);

                bedCameraBody.applyRootMotion = false;
                bedCameraBody.Play("wakeup");
                Invoke("SwitchCamera", 1.8f);

            }

        }

        if (GameManager.Instance.level == 2)
        {

            if (GameManager.Instance.alarmClockState)
            {
                GameManager.StopAudio(MonologueObj);

                UIManager.Instance.SetSubtitle("Alarm has stopped.");
                GameManager.Instance.alarmClockState = false;
                GameManager.StopAudio(o);
                GameManager.PlayAudio(bed);
                GameManager.PlayAudio(MonologueObj, bedroomSounds, 11);
                UIManager.Instance.SetSubtitle("Now get some breakfast.");
                UIManager.Instance.SetTask("Get breakfast.");

                bedCameraBody.applyRootMotion = false;
                bedCameraBody.Play("wakeup");

                Invoke("SwitchCamera", 1.8f);
                StopCoroutine("Wait");
            }

        }

        if (GameManager.Instance.level == 3)
        {

            if (GameManager.Instance.alarmClockState)
            {
                GameManager.Instance.alarmClockState = false;
                GameManager.StopAudio(o);
                GameManager.PlayAudio(bed);
                GameManager.PlayAudio(MonologueObj, bedroomSounds, 11);

                bedCameraBody.applyRootMotion = false;
                bedCameraBody.Play("wakeup");

                Invoke("SwitchCamera", 1.8f);

            }

        }
    }

    void SwitchCamera()
    {
        GameManager.Instance.ToggleCamera(3);
        bedCameraBody.applyRootMotion = true;
    }

    public void GetDressed()
    {
        if (GameManager.Instance.level == 1)
        {
            if (!GameManager.Instance.clothesOn & !GameManager.Instance.alarmClockState)
            {
                UIManager.Instance.SetSubtitle("You got dressed.");
                collider.GetComponent<BoxCollider>().isTrigger = true;
                GameManager.Instance.faucetState = true;
                GameManager.Instance.clothesOn = true;
                GameManager.PlayAudio(MonologueObj, bedroomSounds, 8);
                UIManager.Instance.SetSubtitle("Alright. Let's wash our face.");
                UIManager.Instance.SetTask("Go to the bathroom and wash your face.");
            }
        }
    }

    int once = 0;
    IEnumerator Wait(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);
        if (GameManager.Instance.level == 1)
        {
            if (GameManager.Instance.alarmClockState)
            {
                // GameManager.PlayAudio(MonologueObj, bedroomSounds, 2);

                yield return new WaitForSeconds(12f);
                if (GameManager.Instance.alarmClockState)
                {
                    //GameManager.PlayAudio(MonologueObj, bedroomSounds, 3);
                }

                yield return new WaitForSeconds(12f);
                if (GameManager.Instance.alarmClockState)
                {
                    //GameManager.PlayAudio(MonologueObj, bedroomSounds, 4);
                }

                yield return new WaitForSeconds(12f);
                if (GameManager.Instance.alarmClockState)
                {
                    //GameManager.PlayAudio(MonologueObj, bedroomSounds, 5);
                }

            }

            if (!GameManager.Instance.alarmClockState && once == 0)
            {
                GameManager.StopAudio(MonologueObj);
                UIManager.Instance.SetSubtitle("Now get dressed! The closet is behind you.");
                UIManager.Instance.SetTask("Find the closet and get dressed.");
                GameManager.PlayAudio(MonologueObj, bedroomSounds, 7);
                once++;
            }
        }

        if (GameManager.Instance.level == 2)
        {
            if (GameManager.Instance.alarmClockState)
            {
                GameManager.PlayAudio(MonologueObj, bedroomSounds, 9);

                yield return new WaitForSeconds(8f);
                if (GameManager.Instance.alarmClockState)
                {
                    GameManager.PlayAudio(MonologueObj, bedroomSounds, 10);
                    UIManager.Instance.SetSubtitle("Put out the alarm!");
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (GameManager.Instance.level == 1)
        {
            if (!GameManager.Instance.clothesOn)
            {
                UIManager.Instance.SetSubtitle("Are you sure you are finished?");
            }

            if (GameManager.Instance.clothesOn)
            {
                GameManager.PlayAudio(MonologueObj, bedroomSounds, 14);
            }
        }
    }

}