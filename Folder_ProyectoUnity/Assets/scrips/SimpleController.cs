using UnityEngine;
using DG.Tweening;

public class SimpleController : MonoBehaviour
{
    [SerializeField] private Transform TargetPosition;
    [SerializeField] private Vector3 TargetVector;
    [SerializeField] private float duration;

    [Header("Ease animation")]
    [SerializeField] private Ease EaseValue = Ease.Linear;

    private Vector3 _objective;

    private void Start()
    {
        _objective = TargetPosition == null ? TargetVector : TargetPosition.position;

        transform.DOMove(_objective, duration).SetEase(EaseValue);
    }

    public void SequenceBehaviour()
    {
        //Declaraci�n del una Sequencia de interpolaciones.
        Sequence mySequence = DOTween.Sequence();

        //A�adir una Interpolaci�n a la secuencia. Se a�ade al final.
        mySequence.Append(transform.DOMove(_objective, duration).SetEase(EaseValue));
        mySequence.Append(transform.DORotate(new Vector3(90f, 90f, 90f), duration).SetEase(EaseValue));

        //Insertar una Interpolaci�n en tiempo de la Sequencia.
        mySequence.Insert(duration, transform.DOScale(Vector3.one * 2.5f, duration).SetEase(EaseValue));

        mySequence.onComplete += Shrink;

        mySequence.OnComplete(Shrink);
    }

    private void Shrink()
    {
        transform.DOScale(Vector3.zero, duration).SetEase(EaseValue);
    }
}
