using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{

    public Trigger bathroomTrigger;

    public GameObject MonologObj;
    public GameObject Faucet;
    public GameObject Toothbrush;

    public GameObject interact;

    public AudioClip[] bathroomSounds;

    public void washFace()
    {

        if (!GameManager.Instance.faceWashed)

        {
            if (GameManager.Instance.level == 1)
            {

                UIManager.Instance.SetSubtitle("You washed your face! Now brush your teeth. The toothbrush is on the left side of the sink.");
                UIManager.Instance.SetTask("Brush your teeth.\nThe toothbrush is on the left side of the sink.");
                GameManager.PlayAudio(MonologObj, bathroomSounds, 3);
                GameManager.Instance.faucetState = false;
                GameManager.Instance.faceWashed = true;
            }

        }

    }

    public void brushTeeth()
    {
        if (GameManager.Instance.faceWashed)
        {
            if (GameManager.Instance.level == 1)
            {
                UIManager.Instance.SetSubtitle("You brushed your teeth. You are now ready to start your day.");
                while (Toothbrush.GetComponent<AudioSource>().isPlaying) { 

                }
                GameManager.Instance.SetState(GameManager.GameState.newLevel);


                if (GameManager.Instance.faucetState)
                {

                    GameManager.Instance.faucetState = false;
                    GameManager.Instance.faceWashed = true;

                }

            }

        }

    }

    public void CheckObject(GameObject o)
    {
        if (o.CompareTag(GameManager.selectableTag))

        {
            if (o.name.Equals("Sink"))
            {

                if (!GameManager.Instance.faceWashed)
                {
                    GameManager.PlayAudio(Faucet, bathroomSounds, 0);

                    Invoke("washFace", 6f);
                }

            }

            if (o.name.Equals("Toothbrush"))
            {

                if (GameManager.Instance.faceWashed)
                {
                    
                    GameManager.PlayAudio(o, bathroomSounds, 2);
                    StartCoroutine(Wait(false, 1f));
                   

                } else {
                    UIManager.Instance.SetSubtitle("That's the toothbrush but you need to wash you face first.");

                    GameManager.PlayAudio(MonologObj, bathroomSounds, 4);
                }

            }


        }

        //This is for playing audioclips when the player interacts with the wrong objects
        else if (o.CompareTag(GameManager.notSelectable))
        {
            GameManager.PlayAudio(interact);
            if (o.name.Equals("BigClock"))
            {
                UIManager.Instance.SetSubtitle("That's the old clock. The bathroom door is on the left.");
                GameManager.PlayAudio(MonologObj, bathroomSounds, 5); 
            }

            if (o.name.Equals("Shower"))
            {
                UIManager.Instance.SetSubtitle("That's the shower. The sink is on your left.");
                
            }

            if (o.name.Equals("SmallShelf"))
            {
                UIManager.Instance.SetSubtitle("That's the small shelf next to the shower.");
                
                if (GameManager.Instance.faceWashed)
                {
                    GameManager.PlayAudio(MonologObj, bathroomSounds, 7);
                }
                else
                {
                    GameManager.PlayAudio(MonologObj, bathroomSounds, 6);
                }
            }

            if (o.name.Equals("BathroomWindow"))
            {
                UIManager.Instance.SetSubtitle("That's the bathroom window.");
                
            }

            if (o.name.Equals("Toilet"))
            {
                UIManager.Instance.SetSubtitle("That's the toilet.");
                
            }

            if (o.name.Equals("SmallCupboard"))
            {
                UIManager.Instance.SetSubtitle("That's a small cupboard. I dont need anything from it.");
                
               
            }

            if (o.name.Equals("Hairbrush"))
            {
                UIManager.Instance.SetSubtitle("That's my hairbrush. I dont need it right now.");
                GameManager.PlayAudio(o);
            }

            if (o.name.Equals("Soap"))
            {
                UIManager.Instance.SetSubtitle("That's the soap. I dont need it right now.");
                GameManager.PlayAudio(o);
            }

            if (o.name.Equals("Shampoo"))
            {
                UIManager.Instance.SetSubtitle("That's the Shampoo. I dont need it right now.");
                
                if (GameManager.Instance.faceWashed)
                {
                    GameManager.PlayAudio(o);
                }
                else {
                    GameManager.PlayAudio(MonologObj, bathroomSounds, 4);
                }
                
            }
        }
    }

    IEnumerator Wait(bool status, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        GameManager.PlayAudio(Toothbrush, bathroomSounds, 1);

        yield return new WaitForSeconds(4f);
        GameManager.PlayAudio(Toothbrush, bathroomSounds, 8);
        yield return new WaitForSeconds(3f);
        brushTeeth();

    }

}

