using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int health=100, ammo=30, kills;
    public float fireRate=.18f, range=90f, damage=50f;
    float nextFire;
    public void Shoot()
    {
        if(Time.time<nextFire || ammo<=0) return;
        nextFire=Time.time+fireRate; ammo--;
        Camera cam=Camera.main;
        if(!cam) return;
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out RaycastHit hit,range))
        {
            EnemyBot enemy=hit.collider.GetComponentInParent<EnemyBot>();
            if(enemy && enemy.TakeDamage(damage)) kills++;
        }
    }
    public void Reload(){ammo=30;}
    public void TakeDamage(int amount){health=Mathf.Max(0,health-amount);}
}