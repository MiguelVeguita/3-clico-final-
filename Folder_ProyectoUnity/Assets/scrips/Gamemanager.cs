using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;



public class GameManager : MonoBehaviour
{
    public CharacterUIController uiController;
    public EnemyStats[] characters;
    public float puntitos=0f;
    public TextMeshProUGUI puntitostexto;
    public float life = 10;
    public Slider vi;
    public AudioClip pie;
    private AudioSource sour;
    public static event Action yes;
    public GameObject COCHESITO;
    public GameObject enemy;
    public static event Action morir;

    private int currentCharacterIndex = 0;
    void OnEnable()
    {
        engranaje.yep += total;
        EnemyMover.ataque += vidita;
        BeizerMovement.ataque2 += vidita;
    }

    void OnDisable()
    {
        engranaje.yep -= total;
        EnemyMover.ataque -= vidita;
        BeizerMovement.ataque2 -= vidita;

    }
    private void Start()
    {
        sour= GetComponent<AudioSource>();
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
            COCHESITO.SetActive(true);
            enemy.SetActive(true);
           // Cursor.lockState = CursorLockMode.Confined;
            yes?.Invoke();
           // SceneManager.LoadScene("ganaste");
        }
        puntitostexto.text = "Engranajes X:"+puntitos.ToString();

    }
    public void vidita()
    {
        life = life - 1;
        vi.value=life;
        if(life <= 0)
        {
            SceneManager.LoadScene("perdiste");
            morir?.Invoke();
        }
      
    }
   
}
