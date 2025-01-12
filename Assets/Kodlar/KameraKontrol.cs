using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KameraKontrol : MonoBehaviour
{
    public float HareketH�z�, D�nmeH�z�, ZoomH�z�;
    public float MaxD�nme, MinD�nme, MaxFOV, MinFOV;
    [Space(20)]
    public Slider ZoomSlider, A��Slider;
    private Vector2 SonDokunmaKonumu,KesmeDelta;
    public GameObject CameraLookOrigin;

    void Start()
    {
        ZoomSlider.maxValue = MaxFOV;
        ZoomSlider.minValue = MinFOV;
        ZoomSlider.value = 15;

        A��Slider.maxValue = MaxD�nme;
        A��Slider.minValue = MinD�nme;
        A��Slider.value = -20;

        ZoomSlider.onValueChanged.AddListener(FOVG�ncelle);
        A��Slider.onValueChanged.AddListener(A��y�G�ncelle);
    }
    // Update is called once per frame
    void Update()
    {
        #region PC i�in
            #if UNITY_EDITOR
            Vector3 Hareket = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (gameObject.GetComponent<Camera>().orthographicSize < MinFOV)
                    {
                        gameObject.GetComponent<Camera>().fieldOfView = MinFOV;
                    }
                    if (gameObject.GetComponent<Camera>().fieldOfView > MaxFOV)
                    {
                        gameObject.GetComponent<Camera>().fieldOfView = MaxFOV;
                    }

        //if (CameraLookOrigin.transform.eulerAngles.x < MinD�nme)
        //{
        //    CameraLookOrigin.transform.eulerAngles = new Vector3(MinD�nme, 0, 0);
        //}

        //if (CameraLookOrigin.transform.eulerAngles.x > MaxD�nme)
        //{
        //    CameraLookOrigin.transform.eulerAngles = new Vector3(MaxD�nme, 0, 0);
        //}

        #region HareketKodlar�
        gameObject.transform.Translate(HareketH�z� * Time.deltaTime * Hareket);

        gameObject.transform.Rotate(Input.GetAxis("Turn") * Time.deltaTime * D�nmeH�z�, 0, 0);

        gameObject.GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ZoomH�z� * Time.deltaTime;
        #endregion

#endif
        #endregion

        #region Telefon i�in
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // E�er bir UI eleman�na dokunuluyorsa i�lemi durdur
            if (IsPointerOverUI(touch))
                return;


            // Sa�a ve sola kesme i�lemleri i�in
            if (touch.phase == TouchPhase.Moved)
            {   
                print("Moving");
                Vector2 MevcutDokunmaKonumu = touch.position;

                KesmeDelta = MevcutDokunmaKonumu - SonDokunmaKonumu;
                // Son pozisyonu g�ncelle
                SonDokunmaKonumu = MevcutDokunmaKonumu;

                if (Mathf.Abs(KesmeDelta.magnitude) > 5)
                {
                    transform.position -= new Vector3(KesmeDelta.x * HareketH�z�, 0, KesmeDelta.y * HareketH�z�) * Time.deltaTime;
                }

            }
            else if (touch.phase == TouchPhase.Began)
            {
                print("TOUCHED");
                SonDokunmaKonumu = touch.position; // �lk pozisyonu kaydet
            }
        }

        #endregion
    }

    public void FOVG�ncelle(float value)
    {
        value = ZoomSlider.value;
        transform.GetComponent<Camera>().fieldOfView = value;
    }

    public void A��y�G�ncelle(float value)
    {
        value = A��Slider.value;
        print(value);
        CameraLookOrigin.transform.eulerAngles = new Vector3(value, 0, 0);
    }

    private bool IsPointerOverUI(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

}
