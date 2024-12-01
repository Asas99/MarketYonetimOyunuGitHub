using Unity.VisualScripting;
using UnityEngine;

public class RafManager : MonoBehaviour
{
    private ErzakManager erzakManager;
    private bool ÜrünBulundu;
    public string MevcutÜrün;
    public int ÜrünAdedi;

    void Awake()
    {
        erzakManager = FindFirstObjectByType<ErzakManager>();   
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < erzakManager.ÜrünBilgi.Length; i++)
        {
            if (MevcutÜrün == erzakManager.ÜrünBilgi[i].Ýsim)
            {
                ÜrünBulundu = true;
                break;
            }
        }
        if (!ÜrünBulundu)
        {
            print(gameObject.name + " adlý objede" + MevcutÜrün + " erzakta bulunmamaktadýr. Lütfen erzakta bulunan bir ürün seçiniz.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
