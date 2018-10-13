using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeysInport : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selected;

    private bool isSelect;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Sterowanie przeciskami
    private void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit.GameExit();
        }

        if (isSelect == false && Input.GetAxisRaw("Vertical") != 0)
        {
            eventSystem.SetSelectedGameObject(selected);
            isSelect = true;
        }
	}

    // wyłączenie
    private void OnDisable()
    {
        isSelect = false;
    }
}
