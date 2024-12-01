using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KameraKontrol : MonoBehaviour
{
    public float HareketH�z�, D�nmeH�z�, ZoomH�z�;
    public float MaxD�nme, MinD�nme, MaxOrtografikBoyut, MinOrtografikBoyut;
    [Space(20)]
    public Slider ZoomSlider, A��Slider;
    private Vector2 SonDokunmaKonumu,KesmeDelta;

    void Start()
    {
        ZoomSlider.maxValue = MaxOrtografikBoyut;
        ZoomSlider.minValue = MinOrtografikBoyut;
        ZoomSlider.value = 5;

        A��Slider.maxValue = MaxD�nme;
        A��Slider.minValue = MinD�nme;
        A��Slider.value = transform.eulerAngles.x;
    }
    // Update is called once per frame
    void Update()
    {
        #region PC i�in
            #if UNITY_EDITOR
            Vector3 Hareket = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (gameObject.GetComponent<Camera>().orthographicSize < MinOrtografikBoyut)
                    {
                        gameObject.GetComponent<Camera>().orthographicSize = MinOrtografikBoyut;
                    }
                    if (gameObject.GetComponent<Camera>().orthographicSize > MaxOrtografikBoyut)
                    {
                        gameObject.GetComponent<Camera>().orthographicSize = MaxOrtografikBoyut;
                    }

                    if (gameObject.GetComponent<Camera>().transform.eulerAngles.x < MinD�nme)
                    {
                        transform.eulerAngles =new Vector3(MinD�nme, transform.eulerAngles.y,transform.eulerAngles.z);
                    }

                    if (gameObject.GetComponent<Camera>().transform.eulerAngles.x > MaxD�nme)
                    {
                        transform.eulerAngles = new Vector3(MaxD�nme, transform.eulerAngles.y, transform.eulerAngles.z);
                    }

                    #region HareketKodlar�
                        gameObject.transform.Translate(HareketH�z� * Time.deltaTime * Hareket);

                        gameObject.transform.Rotate(Input.GetAxis("Turn") * Time.deltaTime * D�nmeH�z�, 0, 0);

                        gameObject.GetComponent<Camera>().orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ZoomH�z� * Time.deltaTime;
        #endregion

#endif
        #endregion

        #region Telefon i�in
        if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(0);

            // E�er bir UI eleman�na dokunuluyorsa i�lemi durdur
            if (IsPointerOverUI(touch))
                return;

            if (touch.phase == TouchPhase.Moved)
            {
                if (KesmeDelta.magnitude > 5)
                {
                    Vector2 MevcutDokunmaKonumu = touch.position;
                    KesmeDelta = MevcutDokunmaKonumu - SonDokunmaKonumu;

                    // Son pozisyonu g�ncelle
                    SonDokunmaKonumu = MevcutDokunmaKonumu;
                }

            }
            else if (touch.phase == TouchPhase.Began)
            {
                SonDokunmaKonumu = touch.position; // �lk pozisyonu kaydet
            }
        }


        transform.GetComponent<Camera>().orthographicSize = ZoomSlider.value;
        transform.eulerAngles = new Vector3(A��Slider.value, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position -= new Vector3(KesmeDelta.x * HareketH�z�, 0, KesmeDelta.y * HareketH�z�) * Time.deltaTime;
        #endregion
    }
    private bool IsPointerOverUI(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

}
