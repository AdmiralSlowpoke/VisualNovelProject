using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Destroy(transform.parent.gameObject);
             Destroy(transform.gameObject);
        }
    }
}
