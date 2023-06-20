using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDaño : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemis" && !other.GetComponent<EmeraldAI.EmeraldAISystem>().IsDead)
        {
            other.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(5, null, transform);
        }
    }
}