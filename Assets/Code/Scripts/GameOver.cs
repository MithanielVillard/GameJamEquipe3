using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image _logo;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _LooseTexture;
    [SerializeField] private Sprite _WinTexture;

    
    public void CStart(int win)
    {
        _logo.sprite =  win == 1 ? _WinTexture : _LooseTexture;
        GetComponentInChildren<Animation>().Play();
    }

    public void Bruh()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
