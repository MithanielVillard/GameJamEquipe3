using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    
    public void OnDeath()
    {
        GameObject go = Instantiate(this.gameOver);
        go.GetComponentInChildren<GameOver>().CStart(0);

    }
    
    public void OnWin()
    {
        GameObject go = Instantiate(this.gameOver);
        go.GetComponentInChildren<GameOver>().CStart(1);
    }
}
