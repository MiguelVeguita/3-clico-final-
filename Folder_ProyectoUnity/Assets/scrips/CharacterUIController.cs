using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI comportamiento;
    public TextMeshProUGUI characteristicsText;
    public TextMeshProUGUI nombre;
    public TextMeshProUGUI habilidades;

    public void UpdateUI(EnemyStats character)
    {
        if (characterImage != null)
        {
            characterImage.sprite = character.imagenSprite; 

        }

        if (comportamiento != null)
        {
            comportamiento.text = character.comportamiento; 

        }

        if (characteristicsText != null)
        {
            characteristicsText.text = character.descripcion; 
        }
        if (nombre != null)
        {
            nombre.text = character.nombre;
        }
        if (habilidades != null)
        {
            habilidades.text = character.habilidades;
        }
    }
}

