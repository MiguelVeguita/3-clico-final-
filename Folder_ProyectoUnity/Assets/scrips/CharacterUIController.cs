using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI characteristicsText;

    public void UpdateUI(EnemyStats character)
    {
        if (characterImage != null)
        {
            characterImage.sprite = character.imagenSprite; 

        }

        if (lifeText != null)
        {
            lifeText.text = "Vida: " + character.vida.ToString(); 

        }

        if (characteristicsText != null)
        {
            characteristicsText.text = "Características: " + character.caracteristicasTexto; 
        }
    }
}

