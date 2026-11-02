using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    public Transform target;
    public float health=100, moveSpeed=2.2f, attackRange=2.4f;
    float attackTimer;
    public bool TakeDamage(float damage)
    {
        health-=damage;
        if(health<=0){Destroy(gameObject);return true;}
        return false;
    }
    void Update()
    {
        if(!target) return;
        Vector3 delta=target.position-transform.position; delta.y=0;
        float d=delta.magnitude;
        if(d>attackRange)
        {
            Vector3 step=delta.normalized*moveSpeed*Time.deltaTime;
            transform.position+=step;
            if(delta.sqrMagnitude>.01f) transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(delta),8*Time.deltaTime);
        }
        else
        {
            attackTimer-=Time.deltaTime;
            if(attackTimer<=0)
            {
                var player=target.GetComponent<PlayerCombat>();
                if(player) player.TakeDamage(6);
                attackTimer=1.1f;
            }
        }
    }
}