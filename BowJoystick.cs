using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowJoystick : MonoBehaviour
{
    public Joystick joystick;

    public float bowDirectionH;
    public float bowDirectionV;

    public PlayerMStick playerMStick;

    public Transform targetJoystick;
    public Transform invisible;

    public Vector3 dir;
    public float angle;
    public Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        bowDirectionH = joystick.Horizontal;
        bowDirectionV = joystick.Vertical;


        if (bowDirectionH <= -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (bowDirectionH >= 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (playerMStick.bowCheck == false && playerMStick.isGrounded == true && bowDirectionH <= -0.1f || bowDirectionH >= 0.1f )
        {
            playerMStick.bowCheck = true;
            playerMStick.anim.SetTrigger("bow");
            playerMStick.speed = 6;

        }
        else if (playerMStick.isGrounded == true && bowDirectionH == 0f)
        {
            playerMStick.bowCheck = false;
            playerMStick.anim.SetTrigger("bow");
            playerMStick.speed = 8;
        }



/*
        dir = targetJoystick.position;

        if (dir != Vector3.zero)
        {
            angle = Mathf.Atan2(bowDirectionV, bowDirectionH) * Mathf.Rad2Deg;
            targetJoystick.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }*/

        /*ray = Mathf.Atan2(bowDirectionV, bowDirectionH) * Mathf.Rad2Deg;

        Debug.DrawLine(bowPoint.position, hit.point, Color.red);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
        }*/
    }
   
}
