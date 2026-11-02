using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerCombat player;
    bool ended;
    void Update()
    {
        if(ended || !player) return;
        int enemies=FindObjectsOfType<EnemyBot>().Length;
        if(player.health<=0){ended=true;Debug.Log("GAME OVER");}
        else if(enemies==0){ended=true;Debug.Log("BOOYAH!");}
    }
}