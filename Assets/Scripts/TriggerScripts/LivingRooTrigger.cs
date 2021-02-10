using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRooTrigger : MonoBehaviour
{
    public GameObject LivingroomEntrance;
    public AudioClip[] livingroomSounds;
    public GameObject MonologueObj;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.level == 1)
        {
            if (GameManager.Instance.clothesOn)
            {
                UIManager.Instance.SetSubtitle("The bathroom is in the opposite direction.");
            }

        }

        if (GameManager.Instance.level == 2)
        {
            if (!GameManager.Instance.coffeeMachineFound)
            {
                UIManager.Instance.SetSubtitle("Find the coffeemachine!");
                GameManager.PlayAudio(MonologueObj, livingroomSounds, 0);
            }
        }
    }
}
