using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarPick : MonoBehaviour
{

    public DoorOpen open;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            open.crowbarPicked = true;
            Destroy(this.gameObject);
        }
    }
}
