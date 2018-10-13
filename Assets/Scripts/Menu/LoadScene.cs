using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    // Ladowanie po Id
	public void LoadIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
