using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject doors;
    public GameObject weather1, weather2;
    private void OnTriggerEnter(Collider other)
    {
        doors.SetActive(true);
        weather1.SetActive(false);
        weather2.SetActive(false);
    }
}
