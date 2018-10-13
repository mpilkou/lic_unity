using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public GameObject table;
    public string[] scores;

    private Text[] t;

    private static int N = 9;

	// Pobieranie rekordów
	IEnumerator Start () {
        t = new Text[N];
        WWW data = new WWW("http://localhost/Project/SelectData.php");
        yield return data;
        scores = data.text.Split('\n');
        
        for (int i = 0; i < N; i++)
        {
            t[i] = table.transform.Find(i.ToString()).GetComponentInChildren<Text>();
        }
        
        
        for (int i = 0; i < N; i++)
        {
            try
            {
                t[i].text = "" + scores[i];
            }
            catch (System.IndexOutOfRangeException)
            {
                break;
            }
        }
    }



}
