using UnityEngine;

public class MobileHUD : MonoBehaviour
{
    PlayerMotor motor;
    PlayerCombat combat;
    void Start(){motor=FindObjectOfType<PlayerMotor>();combat=FindObjectOfType<PlayerCombat>();}
    void OnGUI()
    {
        if(!motor||!combat)return;
        GUIStyle s=new GUIStyle(GUI.skin.box);s.fontSize=Mathf.Max(16,Screen.width/48);s.normal.textColor=Color.white;
        GUI.Box(new Rect(10,10,240,76),"HP: "+combat.health+"     AMMO: "+combat.ammo+"\\nKILLS: "+combat.kills,s);
        float w=Screen.width*.13f,h=Screen.height*.10f;
        float y=Screen.height-h-18;
        if(GUI.RepeatButton(new Rect(18,y,w,h),"◀"))PlayerMotor.moveX=-1;else PlayerMotor.moveX=0;
        if(GUI.RepeatButton(new Rect(18+w+8,y,w,h),"▶"))PlayerMotor.moveX=1;
        if(GUI.RepeatButton(new Rect(18,y-h-8,w,h),"▲"))PlayerMotor.moveY=1;else PlayerMotor.moveY=0;
        if(GUI.RepeatButton(new Rect(Screen.width-w-18,y,w,h),"TEMBAK"))combat.Shoot();
        if(GUI.Button(new Rect(Screen.width-w-18,y-h-8,w,h),"ISI ULANG"))combat.Reload();
    }
}