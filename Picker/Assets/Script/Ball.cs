using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Game_Manager _GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BallCount"))
        {
            _GameManager.source[4].Play();
            _GameManager.CountBall();
        }
    }

}
