using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class BallGroundTechnical
{
    [Header("---Check Point---")]
    public Animator ballGround;
    public TextMeshProUGUI countText;
    public int balltobeThrown; // At�lmas� gereken top.
    public GameObject[] balls;

}


public class Game_Manager : MonoBehaviour
{
    [Header("---Picker---")]
    [SerializeField] private GameObject pickerObject;
    [SerializeField] private GameObject[] pickerPropeller;
    bool activePropeller;

    [Header("---Ball---")]
    [SerializeField] private GameObject ballControlObejct; // Colider .
    [SerializeField] private List<BallGroundTechnical> _ballGroundTechnicals = new List<BallGroundTechnical>();
    public bool motionStatus = true; // Picker objesi ilerleme kontrol�.
    int ballCame; // At�lan top say�s�.
    int totalCheckPoint; // Sahnedeki toplam check point.
    int availableCheckPoint; // Mevcut check point.
    [SerializeField] private GameObject[] bonusBalls;


    [Header("---Effect---")]
    [SerializeField] private GameObject[] locationEffect;
    [SerializeField] private ParticleSystem checkPointEfect;


    [Header("---UI-AudioSource---")]
    public AudioSource[] source;

    [SerializeField] private GameObject[] panel;
    [SerializeField] private TextMeshProUGUI[] levelText;

    float fingerPozX;

    void Start()
    {
        motionStatus = true;

        //Oyun ba�lad���nda at�lmas� gereken top say�s�n� verir.
        for (int i = 0; i < _ballGroundTechnicals.Count; i++)
        {
            _ballGroundTechnicals[i].countText.text = ballCame + "/" + _ballGroundTechnicals[i].balltobeThrown;
        }

        totalCheckPoint = _ballGroundTechnicals.Count - 1;
    }

    void Update()
    {
        if (motionStatus)
        {
            if (Time.timeScale != 0)
            {
                pickerObject.transform.position += +4f * Time.deltaTime * pickerObject.transform.forward;

                if (Input.touchCount > 0)
                {

                    Touch touch = Input.GetTouch(0);

                    Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                    switch (touch.phase)
                    {

                        case TouchPhase.Began:

                            fingerPozX = TouchPosition.x - pickerObject.transform.position.x;
                            break;

                        case TouchPhase.Moved:

                            if (TouchPosition.x-fingerPozX>-1.15&& TouchPosition.x - fingerPozX<1.15)
                            {
                                Vector3 PozNew = new Vector3(TouchPosition.x - fingerPozX, pickerObject.transform.position.y, pickerObject.transform.position.z);

                                pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position, PozNew, 3f);
                            }

                            break;
                    }
                }
                #region Tu� kontrol
                

                //if (Input.GetKey(KeyCode.LeftArrow))
                //{

                //    Vector3 PozNEW = new Vector3(pickerObject.transform.position.x - .05f, pickerObject.transform.position.y, pickerObject.transform.position.z);

                //    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position, PozNEW, 0.5f);
                //}

                //if (Input.GetKey(KeyCode.RightArrow))
                //{

                //    Vector3 PozNEW = new Vector3(pickerObject.transform.position.x + .05f, pickerObject.transform.position.y, pickerObject.transform.position.z);

                //    pickerObject.transform.position = Vector3.Lerp(pickerObject.transform.position, PozNEW, 0.5f);


                //}

                #endregion
            }
        }
    }

    public void buttons(string value)
    {
        if (value == "quit")
        {
            Application.Quit();
        }
        else if (value == "paused")
        {
            Time.timeScale = 0;
            panel[0].SetActive(true);
        }
        else if (value == "resume")
        {
            panel[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (value == "again")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (value == "next")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void BonusBall(int bonusBallIndex)
    {
        bonusBalls[bonusBallIndex].SetActive(true);
    }

    public void PickerPropellerActive()
    {
        activePropeller = true;
        pickerPropeller[0].SetActive(true);
        pickerPropeller[1].SetActive(true);
    }


    // Picker objesi asans�re geldi�inde �al���cak metot. //PickerStop.cs
    public void AtTheBorder()
    {
        // S�n�ra geldi Picker objesini durdur.
        motionStatus = false;

        if (activePropeller)
        {
            pickerPropeller[0].SetActive(false);
            pickerPropeller[1].SetActive(false);
        }

        //Picker objesi i�inde bir colider olu�turur ve colider i�indeki toplara addforce metodu ile g�� uygular.
        Collider[] hitControl = Physics.OverlapBox(ballControlObejct.transform.position, ballControlObejct.transform.localScale / 2, Quaternion.identity);
        int Num = 0;
        while (Num < hitControl.Length)
        {
            hitControl[Num].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.7f), ForceMode.Impulse);
            Num++;
        }

        //Picker objesinin durma yerine geldi�inde toplar�n yere de�me s�resi oldu�u i�in Invoke kullland�k.
        Invoke("StageControl", 2f);
    }

    // Top asans�r zeminine temasta �al���cak metot. // Ball.cs
    public void CountBall()
    {
        ballCame++;
        _ballGroundTechnicals[availableCheckPoint].countText.text = ballCame + "/" + _ballGroundTechnicals[availableCheckPoint].balltobeThrown;
    }

    // Picker objesi asans�r objesine geldi�inde at�lan topu kontrol edicek. AtTheBorder.
    public void StageControl()
    {
        // At�lan top say�s� ,at�lmas� gereken top say�s�na e�it yada b�y�kse bu i�lemleri yap.
        if (ballCame >= _ballGroundTechnicals[availableCheckPoint].balltobeThrown)
        {
            checkPointEfect.transform.position = locationEffect[availableCheckPoint].transform.position;
            checkPointEfect.Play();

            _ballGroundTechnicals[availableCheckPoint].ballGround.Play("Asans�r");

            //Asans�r animasyonu �al��t�ktan sonra mevcut platformun toplar�n� false yapar.
            foreach (var item in _ballGroundTechnicals[availableCheckPoint].balls)
            {
                item.SetActive(false);
            }

            // Mevcut platform ile toplam platform say�s� e�itse oyunu bitir.
            if (availableCheckPoint == totalCheckPoint)
            {
                PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
                levelText[1].text = "Level : " + SceneManager.GetActiveScene().buildIndex.ToString();
                panel[2].SetActive(true);
                source[3].Play();
                Time.timeScale = 0;
            }
            else
            {
                //de�ilse mevcut platformu 1 artt�r.
                //at�lan top say�s�n� s�f�rla her platform i�in ayr� de�er var.
                availableCheckPoint++;
                ballCame = 0;
                source[0].Play();
                if (activePropeller)
                {
                    pickerPropeller[0].SetActive(true);
                    pickerPropeller[1].SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("Lose");
            Time.timeScale = 0;
            panel[1].SetActive(true);
            levelText[0].text = "Level : " + SceneManager.GetActiveScene().buildIndex.ToString();
            source[2].Play();
        }


    }

    // TOP KONTROL OBJEM�Z�N BOYUTUNDA B�R K�P ALANI ��ZER.
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(ballControlObejct.transform.position, ballControlObejct.transform.localScale);
    //}

}
