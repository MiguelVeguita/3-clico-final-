using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterUIController uiController;
    public EnemyStats[] characters;

    private int currentCharacterIndex = 0;

    private void Start()
    {
        uiController.UpdateUI(characters[currentCharacterIndex]);
    }

    public void NextCharacter()
    {
        currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
        uiController.UpdateUI(characters[currentCharacterIndex]);
    }

    public void PreviousCharacter()
    {
        currentCharacterIndex = (currentCharacterIndex - 1 + characters.Length) % characters.Length;
        uiController.UpdateUI(characters[currentCharacterIndex]);
    }
}
