using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KameraKontrol : MonoBehaviour
{
    public float HareketHýzý, DönmeHýzý, ZoomHýzý;
    public float MaxDönme, MinDönme, MaxOrtografikBoyut, MinOrtografikBoyut;
    [Space(20)]
    public Slider ZoomSlider, AçýSlider;
    private Vector2 SonDokunmaKonumu,KesmeDelta;

    void Start()
    {
        ZoomSlider.maxValue = MaxOrtografikBoyut;
        ZoomSlider.minValue = MinOrtografikBoyut;
        ZoomSlider.value = 5;

        AçýSlider.maxValue = MaxDönme;
        AçýSlider.minValue = MinDönme;
        AçýSlider.value = transform.eulerAngles.x;
    }
    // Update is called once per frame
    void Update()
    {
        #region PC için
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

                    if (gameObject.GetComponent<Camera>().transform.eulerAngles.x < MinDönme)
                    {
                        transform.eulerAngles =new Vector3(MinDönme, transform.eulerAngles.y,transform.eulerAngles.z);
                    }

                    if (gameObject.GetComponent<Camera>().transform.eulerAngles.x > MaxDönme)
                    {
                        transform.eulerAngles = new Vector3(MaxDönme, transform.eulerAngles.y, transform.eulerAngles.z);
                    }

                    #region HareketKodlarý
                        gameObject.transform.Translate(HareketHýzý * Time.deltaTime * Hareket);

                        gameObject.transform.Rotate(Input.GetAxis("Turn") * Time.deltaTime * DönmeHýzý, 0, 0);

                        gameObject.GetComponent<Camera>().orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ZoomHýzý * Time.deltaTime;
        #endregion

#endif
        #endregion

        #region Telefon için
        if (Input.touchCount > 1)
        {
            Touch touch = Input.GetTouch(0);

            // Eðer bir UI elemanýna dokunuluyorsa iþlemi durdur
            if (IsPointerOverUI(touch))
                return;

            if (touch.phase == TouchPhase.Moved)
            {
                if (KesmeDelta.magnitude > 5)
                {
                    Vector2 MevcutDokunmaKonumu = touch.position;
                    KesmeDelta = MevcutDokunmaKonumu - SonDokunmaKonumu;

                    // Son pozisyonu güncelle
                    SonDokunmaKonumu = MevcutDokunmaKonumu;
                }

            }
            else if (touch.phase == TouchPhase.Began)
            {
                SonDokunmaKonumu = touch.position; // Ýlk pozisyonu kaydet
            }
        }


        transform.GetComponent<Camera>().orthographicSize = ZoomSlider.value;
        transform.eulerAngles = new Vector3(AçýSlider.value, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position -= new Vector3(KesmeDelta.x * HareketHýzý, 0, KesmeDelta.y * HareketHýzý) * Time.deltaTime;
        #endregion
    }
    private bool IsPointerOverUI(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }

}
