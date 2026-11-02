using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    CharacterController cc;
    public float speed=6f, gravity=-20f;
    float yVelocity;
    public static float moveX, moveY;
    void Awake(){ cc=GetComponent<CharacterController>(); }
    void Update()
    {
        float x=Input.GetAxisRaw("Horizontal")+moveX;
        float z=Input.GetAxisRaw("Vertical")+moveY;
        x=Mathf.Clamp(x,-1,1); z=Mathf.Clamp(z,-1,1);
        Vector3 forward=Camera.main ? Camera.main.transform.forward : Vector3.forward;
        forward.y=0; forward.Normalize();
        Vector3 right=Camera.main ? Camera.main.transform.right : Vector3.right;
        right.y=0; right.Normalize();
        Vector3 move=(forward*z+right*x);
        if(move.sqrMagnitude>1) move.Normalize();
        if(move.sqrMagnitude>.02f) transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(move),12*Time.deltaTime);
        if(cc.isGrounded && yVelocity<0) yVelocity=-2;
        yVelocity+=gravity*Time.deltaTime;
        cc.Move((move*speed+Vector3.up*yVelocity)*Time.deltaTime);
    }
}