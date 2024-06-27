using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/Stats")]
public class EnemyStats : ScriptableObject
{
    public string nombre;
    public string descripcion;
    public string comportamiento;
    public string habilidades;
    public int e;
    public Sprite imagenSprite;

}
