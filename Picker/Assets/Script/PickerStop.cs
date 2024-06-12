using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerStop : MonoBehaviour
{
    [SerializeField] private Game_Manager _GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickerBorder"))
        {
            _GameManager.AtTheBorder();
       
        }
    }
}
