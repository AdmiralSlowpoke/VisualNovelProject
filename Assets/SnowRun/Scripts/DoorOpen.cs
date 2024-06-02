using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    [System.NonSerialized]
    public bool crowbarPicked = false;

    private void OnTriggerStay(Collider other)
    {
        if (crowbarPicked)
        {
            anim.Play("OpenDoors");
        }
    }

}
