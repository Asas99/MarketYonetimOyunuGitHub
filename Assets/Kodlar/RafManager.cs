using Unity.VisualScripting;
using UnityEngine;

public class RafManager : MonoBehaviour
{
    private ErzakManager erzakManager;
    private bool �r�nBulundu;
    public string Mevcut�r�n;
    public int �r�nAdedi;

    void Awake()
    {
        erzakManager = FindFirstObjectByType<ErzakManager>();   
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < erzakManager.�r�nBilgi.Length; i++)
        {
            if (Mevcut�r�n == erzakManager.�r�nBilgi[i].�sim)
            {
                �r�nBulundu = true;
                break;
            }
        }
        if (!�r�nBulundu)
        {
            print(gameObject.name + " adl� objede" + Mevcut�r�n + " erzakta bulunmamaktad�r. L�tfen erzakta bulunan bir �r�n se�iniz.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
