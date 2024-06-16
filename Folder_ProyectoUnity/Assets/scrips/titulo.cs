using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;  
using System.Collections;

public class titulo : MonoBehaviour
{
    public TextMeshProUGUI uiText; 
    public float shakeDuration = 1f;  
    public float shakeStrength = 10f;  

    void Start()
    {
       
        StartCoroutine(ShakeTextEvery5Seconds());
    }

    IEnumerator ShakeTextEvery5Seconds()
    {
        while (true)  
        {
            ShakeText();  
            yield return new WaitForSeconds(5);  
        }
    }

    void ShakeText()
    {
        uiText.rectTransform.DOShakePosition(shakeDuration, shakeStrength, 20, 90, false, true);
        
    }
}
