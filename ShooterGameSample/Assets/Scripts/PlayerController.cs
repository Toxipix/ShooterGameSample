/*
 * Created By: Shawn Dean
 * Date Created: September 13, 2021
 * 
 * Last Edited By: 
 * Last Updated: September 15, 2021
 * 
 * Description: Player control movements
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool MouseLock = true; //Are we looking at mouse?
    public string HorzAxis = "Horizontal";
    public string VertAxis = "Vertical";
    public string FireAxis = "Fire1";
    public float MaxSpeed = 5f; //Speed

    private Rigidbody ThisBody = null; //Var for the ship's rigidbody.

    // Awake is called before start
    private void Awake()
    {
        ThisBody = GetComponent<Rigidbody>(); //Sets the object's rigidbody to a variable.
    }//end Awake()

    private void FixedUpdate()
    {
        float Horz = Input.GetAxis(HorzAxis);
        float Vert = Input.GetAxis(VertAxis);

        Vector3 MoveDirection = new Vector3(Horz, 0.0f, Vert);
        ThisBody.AddForce(MoveDirection.normalized * MaxSpeed);

        ThisBody.velocity = new Vector3(Mathf.Clamp(ThisBody.velocity.x, -MaxSpeed, MaxSpeed), 
            Mathf.Clamp(ThisBody.velocity.y, -MaxSpeed, MaxSpeed), 
            Mathf.Clamp(ThisBody.velocity.z, -MaxSpeed, MaxSpeed));

        //Look at mouse
        if(MouseLock)
        {
            Vector3 MousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            MousePosWorld = new Vector3(MousePosWorld.x, 0.0f, MousePosWorld.z);

            Vector3 LookDirection = MousePosWorld - transform.position;
            transform.localRotation = Quaternion.LookRotation(LookDirection.normalized, Vector3.up);
        }
    }//end FixedUpdate()
}
