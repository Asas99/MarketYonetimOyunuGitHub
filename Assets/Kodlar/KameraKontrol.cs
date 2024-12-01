using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public float HareketH�z�, D�nmeH�z�, ZoomH�z�;
    public float MaxD�nme, MinD�nme, MaxOrtografikBoyut, MinOrtografikBoyut;

    // Update is called once per frame
    void Update()
    {
        Vector3 Hareket = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (gameObject.GetComponent<Camera>().orthographicSize < MinOrtografikBoyut)
        {
            gameObject.GetComponent<Camera>().orthographicSize = MinOrtografikBoyut;
        }
        if (gameObject.GetComponent<Camera>().orthographicSize > MaxOrtografikBoyut)
        {
            gameObject.GetComponent<Camera>().orthographicSize = MaxOrtografikBoyut;
        }

        if (gameObject.GetComponent<Camera>().transform.eulerAngles.x < 35)
        {
            transform.eulerAngles =new Vector3(35, transform.eulerAngles.y,transform.eulerAngles.z);
        }

        if (gameObject.GetComponent<Camera>().transform.eulerAngles.x > 85)
        {
            transform.eulerAngles = new Vector3(85, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        gameObject.transform.Translate(HareketH�z� * Time.deltaTime * Hareket);

        gameObject.transform.Rotate(Input.GetAxis("Turn") * Time.deltaTime * D�nmeH�z�, 0, 0);

        gameObject.GetComponent<Camera>().orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ZoomH�z� * Time.deltaTime;      
    }
}
