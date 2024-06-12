using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevetorSituation : MonoBehaviour
{

    [SerializeField] private Game_Manager _GameManager;
    [SerializeField] private Animator barrier;
    
    public void BarrierMovement()
    {
        // Asansör animasyonu sonuna bir event eklendi . asansör animasyonu bitimide bu metot çalýþýcak.
        barrier.Play("Barrier");
        
    }
    public void Finish()
    {
        // Picker objesini mevcut platform sýnýrýna geldiðinde durdurduðumuz için tekrar hareketini saðlýyoruz.
        _GameManager.motionStatus = true;
    }

}
