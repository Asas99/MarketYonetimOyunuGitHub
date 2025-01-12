using UnityEngine;
using UnityEngine.UI;

public class AyarlarPanelSaklaGöster : MonoBehaviour
{
    public bool SaklıMı;
    private int ŞimdikiIterasyon;
    public int MaxIterasyon, AdımBaşıHareket;
    public GameObject AyarlarPanel;

    private void Awake()
    {
        ŞimdikiIterasyon = MaxIterasyon;
    }

    public void DeğeriDeğiştir()
    {
        SaklıMı = !SaklıMı;
        ŞimdikiIterasyon = 0;
        AdımBaşıHareket = -AdımBaşıHareket;
    }

    // Update is called once per frame
    void Update()
    {
        if (ŞimdikiIterasyon < MaxIterasyon)
        {
            AyarlarPanel.GetComponent<RectTransform>().anchoredPosition += new Vector2(AdımBaşıHareket,0);
            ŞimdikiIterasyon++;
        }
    }
}
