using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KameraKontrol : MonoBehaviour
{
    public float HareketHýzý, DönmeHýzý, ZoomHýzý;
    public float MaxDönme, MinDönme, MaxFOV, MinFOV;
    [Space(20)]
    public Slider ZoomSlider, AçýSlider;
    private Vector2 SonDokunmaKonumu,KesmeDelta;
    public GameObject CameraLookOrigin;

    void Start()
    {
        ZoomSlider.maxValue = MaxFOV;
        ZoomSlider.minValue = MinFOV;
        ZoomSlider.value = 15;

        AçýSlider.maxValue = MaxDönme;
        AçýSlider.minValue = MinDönme;
        AçýSlider.value = -20;

        ZoomSlider.onValueChanged.AddListener(FOVGüncelle);
        AçýSlider.onValueChanged.AddListener(AçýyýGüncelle);
    }
    // Update is called once per frame
    void Update()
    {
        #region PC için
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

        //if (CameraLookOrigin.transform.eulerAngles.x < MinDönme)
        //{
        //    CameraLookOrigin.transform.eulerAngles = new Vector3(MinDönme, 0, 0);
        //}

        //if (CameraLookOrigin.transform.eulerAngles.x > MaxDönme)
        //{
        //    CameraLookOrigin.transform.eulerAngles = new Vector3(MaxDönme, 0, 0);
        //}

        #region HareketKodlarý
        gameObject.transform.Translate(HareketHýzý * Time.deltaTime * Hareket);

        gameObject.transform.Rotate(Input.GetAxis("Turn") * Time.deltaTime * DönmeHýzý, 0, 0);

        gameObject.GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ZoomHýzý * Time.deltaTime;
        #endregion

#endif
        #endregion

        #region Telefon için
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Eðer bir UI elemanýna dokunuluyorsa iþlemi durdur
            if (IsPointerOverUI(touch))
                return;


            // Saða ve sola kesme iþlemleri için
            if (touch.phase == TouchPhase.Moved)
            {   
                print("Moving");
                Vector2 MevcutDokunmaKonumu = touch.position;

                KesmeDelta = MevcutDokunmaKonumu - SonDokunmaKonumu;
                // Son pozisyonu güncelle
                SonDokunmaKonumu = MevcutDokunmaKonumu;

                if (Mathf.Abs(KesmeDelta.magnitude) > 5)
                {
                    transform.position -= new Vector3(KesmeDelta.x * HareketHýzý, 0, KesmeDelta.y * HareketHýzý) * Time.deltaTime;
                }

            }
            else if (touch.phase == TouchPhase.Began)
            {
                print("TOUCHED");
                SonDokunmaKonumu = touch.position; // Ýlk pozisyonu kaydet
            }
        }

        #endregion
    }

    public void FOVGüncelle(float value)
    {
        value = ZoomSlider.value;
        transform.GetComponent<Camera>().fieldOfView = value;
    }

    public void AçýyýGüncelle(float value)
    {
        value = AçýSlider.value;
        print(value);
        CameraLookOrigin.transform.eulerAngles = new Vector3(value, 0, 0);
    }

    private bool IsPointerOverUI(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

}
