using UnityEngine;

public class ErzakManager : MonoBehaviour
{
    public ÜrünBilgi[] ÜrünBilgi;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class ÜrünBilgi
{
    public string Ýsim;
    public int Adet;
}
