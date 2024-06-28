using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public CharacterUIController uiController;
    public EnemyStats[] characters;
    public float puntitos=0f;
    public TextMeshProUGUI puntitostexto;


    private int currentCharacterIndex = 0;
    void OnEnable()
    {
        engranaje.yep += total;
    }

    void OnDisable()
    {
        engranaje.yep -= total;
    }
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
    public void total()
    {
        puntitos ++;

    }
    private void Update()
    {
        if (puntitos >= 3)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("ganaste");
        }
        puntitostexto.text = "Engranajes X:"+puntitos.ToString();

    }
}
