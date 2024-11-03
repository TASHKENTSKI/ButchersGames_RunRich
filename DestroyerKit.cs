using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerKit : MonoBehaviour
{
    [SerializeField] private GameObject Door1;
    [SerializeField] private GameObject Door2;
    
    public void DisableKit()
    {
        Door1.SetActive(false);
        Door2.SetActive(false);
    }
    public void ActivateKit()
    {
        Door1.SetActive(true);
        Door2.SetActive(true);
    }
}
