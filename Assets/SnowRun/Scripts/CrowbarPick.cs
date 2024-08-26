using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowbarPick : MonoBehaviour
{

    public GameObject helpButton;
    public DoorOpen open;
    private bool inZone = false;

    private void Start()
    {
        helpButton.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&inZone)
        {
            open.crowbarPicked = true;
            helpButton.SetActive(false);
            Destroy(this.gameObject);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>() != null)
        {
            inZone = true;
            helpButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMovement>() != null)
        {
            inZone = false;
            helpButton.SetActive(false);
        }
    }
}
