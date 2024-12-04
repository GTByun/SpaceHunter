using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public Sprite[] spr_key = new Sprite[2];
    public Image img_key;
    private bool isKey = true;

    private void Init() => isKey = true;

    private void Set_sprite()
    {
        img_key.sprite = spr_key[isKey ? 1 : 0];
        isKey = !isKey;
    }

    private void Start()
    {
        Init();
        InvokeRepeating(nameof(Set_sprite), 1.0f, 1.0f);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
