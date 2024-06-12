using UnityEngine;

public class Propeller : MonoBehaviour
{
   bool forstStart = false;
   [SerializeField] private string value;
    
    public void Frost()
    {
        forstStart = true;
    }

    void Update()
    {
        if (forstStart)
        {
            if (value == "left")
            {
                transform.Rotate(0, 0, 3, Space.Self);
            }
            else if (value == "right")
            {
                transform.Rotate(0, 0, -3, Space.Self);
            }
        }
       
    }
}
