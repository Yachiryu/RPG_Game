using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarTrampa : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            //other.

        }
    }
}
