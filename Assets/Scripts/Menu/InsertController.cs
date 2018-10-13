using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsertController : MonoBehaviour{

    public Text text;
    public string nick;
    private GameController gameController;
    private int score = 0;
    private bool done = false;

    // Inicjalizacja
    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        score = gameController.GetScore();

        Destroy(gameController);
    }

    // Sprawdzanie podanych danych
    private void Update () {
        nick = text.text;
        if (Input.GetButtonDown("Submit") && nick != string.Empty && !done)
        {
            if (score == 0){
                SceneManager.LoadScene(0);
            } else {
                done = true;
                StartCoroutine(AddScore(nick));
            }
        }
	}

    // Dodanie rekordu
    IEnumerator AddScore(string nick)
    {
        WWWForm formW = new WWWForm();

        formW.AddField("postNick", nick);
        formW.AddField("postScore", score);
        
        WWW www = new WWW("http://localhost/project/InsertData.php", formW);

        yield return www;

        SceneManager.LoadScene(0);
    }
}
