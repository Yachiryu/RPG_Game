using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cambiomapas : MonoBehaviour
{
    public TextMeshProUGUI nombreDelaEscena;
    public int tiempo;
    public GameObject imagen;
    
   IEnumerator images()
    {
        yield return new WaitForSeconds(tiempo);
        imagen.SetActive(false);
       
    }
    
    
  
}
