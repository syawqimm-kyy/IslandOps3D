using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance=6f, height=2.2f, sensitivity=3f;
    float yaw, pitch=12f;
    Vector2 lastPointer;
    bool dragging;
    void LateUpdate()
    {
        if(!target) return;
        if(Input.GetMouseButtonDown(0)){lastPointer=Input.mousePosition;dragging=true;}
        if(Input.GetMouseButtonUp(0)) dragging=false;
        if(dragging && Input.GetMouseButton(0))
        {
            Vector2 now=Input.mousePosition; Vector2 delta=now-lastPointer; lastPointer=now;
            yaw+=delta.x*sensitivity*.08f; pitch=Mathf.Clamp(pitch-delta.y*sensitivity*.04f,-5,35);
        }
        if(Input.touchCount>0)
        {
            Touch t=Input.GetTouch(0);
            if(t.position.x>Screen.width*.35f && t.phase==TouchPhase.Moved)
            { yaw+=t.deltaPosition.x*sensitivity*.08f; pitch=Mathf.Clamp(pitch-t.deltaPosition.y*sensitivity*.04f,-5,35); }
        }
        Quaternion rot=Quaternion.Euler(pitch,yaw,0);
        Vector3 focus=target.position+Vector3.up*height;
        transform.position=focus+rot*new Vector3(0,0,-distance);
        transform.rotation=rot;
    }
}