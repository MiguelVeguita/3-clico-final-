using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text texto; 
    [SerializeField] private float escalaMaxima = 1.5f; 
    [SerializeField] private float duracionAgrandar = 1f; 
    [SerializeField] private float duracionEncoger = 1f; 

    private void Start()
    {
       
        AgrandarYEncoger();
    }

    private void AgrandarYEncoger()
    {
        
        texto.transform.DOScale(escalaMaxima, duracionAgrandar)
            .SetEase(Ease.OutQuad) 
            .SetUpdate(true) 
            .OnComplete(() =>
            {
                EncogerTexto();
            });
    }

    private void EncogerTexto()
    {
        texto.transform.DOScale(1f, duracionEncoger)
            .SetEase(Ease.InQuad) 
            .SetUpdate(true)
            .OnComplete(() =>
            {
              
                AgrandarYEncoger();
            });
    }
}
