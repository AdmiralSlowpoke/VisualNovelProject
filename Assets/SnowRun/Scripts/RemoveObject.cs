using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    public GameObject helpButton;
    private void OnTriggerStay(Collider other)
    {
        helpButton.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            helpButton.SetActive(false);
            Destroy(transform.parent.gameObject);
        }
    }
}
