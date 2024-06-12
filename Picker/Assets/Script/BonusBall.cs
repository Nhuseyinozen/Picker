using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBall : MonoBehaviour
{
    [SerializeField] private Game_Manager _GameManager;
    [SerializeField]  private int bonusBallIndex;
    [SerializeField] private string value;
    [SerializeField] private ParticleSystem ItemEffect;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorder"))
        {
            if(value =="Propeller")
            {
                ItemEffect.transform.position = gameObject.transform.position;
                ItemEffect.Play();
                _GameManager.source[1].Play();
                _GameManager.PickerPropellerActive();
                gameObject.SetActive(false);
            }
            else if (value == "Ball") 
            {
                ItemEffect.transform.position = gameObject.transform.position;
                ItemEffect.Play();
                _GameManager.source[1].Play();
                _GameManager.BonusBall(bonusBallIndex);
                gameObject.SetActive(false);
            }

         
        }
    }


}
