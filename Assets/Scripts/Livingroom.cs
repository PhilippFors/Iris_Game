using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livingroom : MonoBehaviour
{
    public Trigger livingroomTrigger;
    public GameObject coffeeMachine;
    public GameObject fridge;
    public GameObject cupboard;
    public GameObject kitchenUnit;

    public GameObject interact;

    public new GameObject colliderLivingroom;

    public GameObject MonologueObj;

    public AudioClip[] livingroomSounds;

    public void FindCoffeeMachine()
    {
        if (!GameManager.Instance.coffeeMachineFound)
        {
            GameManager.PlayAudio(MonologueObj, livingroomSounds, 4);
            GameManager.Instance.coffeeMachineFound = true;
        }
    }

    public void CupboardInterakt()
    {
        if (!GameManager.Instance.gotCereals)
        {
            GameManager.PlayAudio(MonologueObj, livingroomSounds, 8);
            GameManager.Instance.gotCereals = true;
        }

    }


    public void CheckObject(GameObject o)
    {

        if (o.CompareTag(GameManager.selectableTag))
        {
            if (GameManager.Instance.level == 2)
            {
                if (o.name.Equals(coffeeMachine.name))
                {
                    if (!GameManager.Instance.coffeeMachineFound)
                    {
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 3);
                        UIManager.Instance.SetSubtitle("You found the coffee machine! Get a cup to put your coffee in.");
                        UIManager.Instance.SetTask("Get a cup.");

                        Invoke("FindCoffeeMachine", 1f);
                    }
                    if (GameManager.Instance.gotCup && !GameManager.Instance.coffeeMade)
                    {
                        GameManager.PlayAudio(coffeeMachine);
                        UIManager.Instance.SetSubtitle("You made coffee!");
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 6);
                        GameManager.Instance.coffeeMade = true;
                    }
                    if (GameManager.Instance.coffeeMade)
                    {
                        UIManager.Instance.SetSubtitle("You already made coffee");
                    }
                }

                if (o.name.Equals(cupboard.name))
                {
                    if (GameManager.Instance.coffeeMachineFound && !GameManager.Instance.gotCup)
                    {
                        GameManager.StopAudio(MonologueObj);
                        GameManager.PlayAudio(cupboard, livingroomSounds, 1);
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 7);
                        UIManager.Instance.SetSubtitle("You got a cup! You can make coffee now.");
                        UIManager.Instance.SetTask("Make some coffee.");
                        GameManager.Instance.gotCup = true;
                    }
                    if (!GameManager.Instance.gotBowl && GameManager.Instance.gotCup)
                    {
                        UIManager.Instance.SetSubtitle("You already got a cup. You can make coffee now.");
                    }

                    if (GameManager.Instance.gotBowl & !GameManager.Instance.gotCereals)
                    {
                        UIManager.Instance.SetSubtitle("You found the cereals!");
                        GameManager.PlayAudio(cupboard, livingroomSounds, 1);
                        Invoke("CupboardInterakt", 2f);
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 11);
                    }

                }

                if (o.name.Equals(kitchenUnit.name))
                {
                    if (GameManager.Instance.coffeeMade && !GameManager.Instance.gotBowl)
                    {
                        UIManager.Instance.SetSubtitle("You got a bowl and a spoon! Get cereals now.");
                        UIManager.Instance.SetTask("Find the cereals.");
                        GameManager.PlayAudio(kitchenUnit, livingroomSounds, 1);
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 5);
                        GameManager.Instance.gotBowl = true;
                    }

                }
                if (o.name.Equals(fridge.name))
                {
                    if (GameManager.Instance.coffeeMade && GameManager.Instance.gotBowl)
                    {
                        GameManager.PlayAudio(fridge, livingroomSounds, 10);
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 12);
                        UIManager.Instance.SetSubtitle("You found the fridge and got milk!");
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 9);
                        Invoke("LevelEnd", 8f);
                    }
                }
            }
            else if (GameManager.Instance.level == 3)
            {

                if (o.name.Equals(coffeeMachine.name))
                {
                    if (!GameManager.Instance.coffeeMachineFound)
                    {
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 3);
                        UIManager.Instance.SetSubtitle("You found the coffee machine! Get a cup to put your coffee in.");
                        UIManager.Instance.SetTask("Get a cup.");

                        Invoke("FindCoffeeMachine", 1f);
                    }
                    if (GameManager.Instance.gotCup && !GameManager.Instance.coffeeMade)
                    {
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 0);
                        UIManager.Instance.SetSubtitle("You made coffee!");
                        StartCoroutine("Wait", 12f);
                    }
                    if (GameManager.Instance.coffeeMade)
                    {
                        UIManager.Instance.SetSubtitle("You already made coffee");
                    }
                }

                if (o.name.Equals(cupboard.name))
                {
                    if (GameManager.Instance.coffeeMachineFound && !GameManager.Instance.gotCup)
                    {
                        GameManager.StopAudio(MonologueObj);
                        GameManager.PlayAudio(cupboard);
                        GameManager.PlayAudio(MonologueObj, livingroomSounds, 7);
                        UIManager.Instance.SetSubtitle("You got a cup! You can make coffee now.");
                        UIManager.Instance.SetTask("Make some coffee.");
                        GameManager.Instance.gotCup = true;
                    }
                }
            }
        }

        //This is for playing audioclips when the player interacts with the wrong objects
        else if (o.CompareTag(GameManager.notSelectable))
        {
            //Debug.Log("It works");

            GameManager.PlayAudio(interact);
            if (o.name.Equals("BigClock"))
            {
                UIManager.Instance.SetSubtitle("That's the old clock. The living room is behind you");
            }

            // livingroom
            if (o.name.Equals("sofa"))
            {
                //Debug.Log("That's the couch.");
                UIManager.Instance.SetSubtitle("That's the sofa in the living room. Behind it is the kitchen unit.");
            }

            if (o.name.Equals("tinyTable"))
            {
                UIManager.Instance.SetSubtitle("That's the tine living room table beside the sofa.");
            }

            if (o.name.Equals("lampfoot") || o.name.Equals("Lamphead"))
            {
                UIManager.Instance.SetSubtitle("That's the lamp beside the living romm couch.");
            }

            if (o.name.Equals("TV") || o.name.Equals("TVtable"))
            {
                UIManager.Instance.SetSubtitle("That's the TV table in the living room.");
            }

            if (o.name.Equals("table 1"))
            {
                UIManager.Instance.SetSubtitle("That's the table.");
            }

            // kitchen
            /*if (o.name.Equals("kitchen"))
            {
                Debug.Log("That's the kitchen.");
            }*/

            if (o.name.Equals("oven 1"))
            {
                UIManager.Instance.SetSubtitle("The kitchen oven. The coffee machine is above it.");
            }

            if (o.name.Equals("sink 1"))
            {
                UIManager.Instance.SetSubtitle("That's the kitchen sink. The coffee machine is farther to the left.");
            }

            if (o.name.Equals("fridge"))
            {
                Debug.Log("That's the fridge.");
            }

            if (o.name.Equals("bin 1"))
            {
               // Debug.Log("That's the bin. The fridge is on the left side of it.");
                UIManager.Instance.SetSubtitle("That's the bin. The fridge is on the left side of it.");
            }

            if (o.name.Equals("microwave1"))
            {
                UIManager.Instance.SetSubtitle("That's the microwave. It is on the left end of the fridge.");
            }

            if (o.name.Equals("radio2"))
            {
                UIManager.Instance.SetSubtitle("That's the kitchen radio.");
            }

            if (o.name.Equals("veggy_bowl 1"))
            {
                UIManager.Instance.SetSubtitle("The fruit bowl. It is full with juicy apples and sweet bananas.");
            }

            /*if (o.name.Equals("coffee_machine"))
            {
                Debug.Log("That's the coffee machine.");
            }
            if (o.name.Equals("KitchenCupboard"))
            {
                Debug.Log("That's the cupboard with the bowls and cereals.");
            }
            if (o.name.Equals("KitchenDrawer"))
            {
                Debug.Log("That's drawer with the spoons.");
            }
            */
        }
    }

    public void LevelEnd()
    {
        GameManager.Instance.SetState(GameManager.GameState.newLevel);
    }
    IEnumerator Wait(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);
        if (GameManager.Instance.level == 2)
        {
            if (GameManager.Instance.coffeeMade)
            {
            }
        }
        
        if (GameManager.Instance.level == 3)
        {
            GameManager.StopAudio(MonologueObj);
            GameManager.Instance.coffeeMade = true;
            GameManager.PlayAudio(GameManager.Instance.monologueobj, GameManager.Instance.levelThree_Narration, 1);   
            //GameManager.Instance.monologeAudiosource.clip = GameManager.Instance.levelThree_Narration[1];
            //GameManager.Instance.monologeAudiosource.Play();
            yield return new WaitForSeconds(7f);
            GameManager.Instance.phoneRinging = true;
            GameManager.Instance.office.LevelStart();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!GameManager.Instance.coffeeMachineFound)
        {
            GameManager.StopAudio(MonologueObj);

        }
    }
}
