using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevetorSituation : MonoBehaviour
{

    [SerializeField] private Game_Manager _GameManager;
    [SerializeField] private Animator barrier;
    
    public void BarrierMovement()
    {
        // Asans�r animasyonu sonuna bir event eklendi . asans�r animasyonu bitimide bu metot �al���cak.
        barrier.Play("Barrier");
        
    }
    public void Finish()
    {
        // Picker objesini mevcut platform s�n�r�na geldi�inde durdurdu�umuz i�in tekrar hareketini sa�l�yoruz.
        _GameManager.motionStatus = true;
    }

}
