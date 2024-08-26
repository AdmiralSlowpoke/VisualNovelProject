using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarPick : MonoBehaviour
{

    public DoorOpen open;
    private bool inZone = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&inZone)
        {
            open.crowbarPicked = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>() != null)
        {
            inZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMovement>() != null)
        {
            inZone = false;
        }
    }
}
