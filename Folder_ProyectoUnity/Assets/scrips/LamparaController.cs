using UnityEngine;
using DG.Tweening;

public class LamparaController : MonoBehaviour
{
    [SerializeField] private Transform lamparaTransform;
    [SerializeField] private float anguloMaximo; 
    [SerializeField] private float duracionRotacion = 1f;

    private Quaternion rotacionInicial;

    private void Start()
    {
        rotacionInicial = lamparaTransform.rotation;
        RotarLampara();
    }

    private void RotarLampara()
    {
        lamparaTransform.DORotate(new Vector3(0f, anguloMaximo, 0f), duracionRotacion)
            .OnComplete(() =>
            {
                lamparaTransform.DORotate(rotacionInicial.eulerAngles, duracionRotacion)
                    .OnComplete(() =>
                    {
                        RotarLampara();
                    });
            });
    }
}
