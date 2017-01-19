using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //GameCore
    public GameObject gameCore;
    GameCore _gc;
    bool fly = false;
    
    public bool isMoving;
    bool collide;

    public float speed = 0.01f;
    Vector3 movement;
    Rigidbody rb;

    //grawitacja
    private float moveSpeed = 6; // move speed
    private float turnSpeed = 90; // turning speed (degrees/second)
    private float lerpSpeed = 10; // smoothing speed
    private float gravity = 10; // gravity acceleration
    private bool isGrounded;
    private float deltaGround = 0.2f; // character is grounded up to this distance
    private float jumpSpeed = 10; // vertical jump initial speed
    private float jumpRange = 30; // range to detect target wall
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float distGround; // distance from character position to ground
    private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
    private float vertSpeed = 0; // vertical jump current speed


    private Transform myTransform;
    public BoxCollider boxCollider; // drag BoxCollider ref in editor
                                    // Use this for initialization

    public GameObject gvr;
    GvrReticle gvrRet;
    public LayerMask mask = 8;
   // GameObject head;
    GvrHead head = null;
    

    private bool StereoControllerHandler = true;
    void Start()
    {
        //test
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        myNormal = transform.up; // normal starts as character up direction
        myTransform = transform;
        rb.freezeRotation = true; // disable physics rotation
                                         // distance from transform.position to ground
        distGround = boxCollider.size.y - boxCollider.center.y;
        gvrRet = gvr.GetComponent<GvrReticle>();

       
        
       
        //test
        
    }

    // Update is called once per frame
    void Update()
    {
        //test
        
        // jump code - jump to wall or simple jump
        if (jumping) return; // abort Update while jumping to a wall

        Ray ray;
        RaycastHit hit;
        

        if (GameCore.instance._fly)
        { // jump pressed:
            
            ray = new Ray(myTransform.position, myTransform.forward);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward , out hit, jumpRange, mask.value))
            { // wall ahead?
                
                JumpToWall(hit.point, hit.normal); // yes: jump to the wall
                GameCore.instance._fly = false;
            }
            //else if (isGrounded)
            //{ // no: if grounded, jump up
            //   // rb.velocity += jumpSpeed * myNormal;
            //}
        }

        // movement code - turn left/right with Horizontal axis:
        myTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        // update surface normal and isGrounded:
        ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit))
        { // use it to update myNormal and isGrounded
            isGrounded = hit.distance <= distGround + deltaGround;
            surfaceNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            // assume usual ground normal to avoid "falling forever"
            surfaceNormal = Vector3.up;
        }
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
        // find forward direction with new myNormal:
        Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
        // align character to the new myNormal while keeping the forward direction:
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        // move the character forth/back with Vertical axis:
        myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

        //----ChecK!
        //Vector3 direction = new Vector3(myForward.x, 0, myForward.z).normalized * Time.deltaTime;
        //Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
        //transform.Translate(rotation * direction, Space.Self);
        //--- 
        //test  

    }


    void FixedUpdate()
    {
        //test
        // apply constant weight force according to character normal:
        rb.AddForce(-gravity * rb.mass * myNormal);


        //test


        //float moveVertical = Input.GetAxis("Vertical");
        if (StereoControllerHandler)
        {
            head = Camera.main.GetComponent<StereoController>().Head;
            StereoControllerHandler = false;
        }



        // if (GameCore.instance.isMoving)
        // {
        // transform.Translate(Vector3.forward  * Time.deltaTime, Space.Self);
        //}
        if (GameCore.instance.isMoving)
        {
            if ( (surfaceNormal == Vector3.up) || (surfaceNormal == Vector3.back) || (surfaceNormal == Vector3.left) || (surfaceNormal == Vector3.right))
            {
                Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * Time.deltaTime;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
                myTransform.Translate(rotation * direction, Space.Self);
            }
            else if (surfaceNormal == Vector3.down)
            {
                Debug.Log("dupa");
                Vector3 direction = new Vector3(-head.transform.forward.x, 0, head.transform.forward.z).normalized * Time.deltaTime;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
                myTransform.Translate(rotation * direction, Space.Self);
            }
            //else if ( (surfaceNormal == Vector3.left) || (surfaceNormal == Vector3.right))
            //{
               
            //    Debug.Log("left and right");
            //    Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * Time.deltaTime;
            //    Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
            //    myTransform.Translate(rotation * direction, Space.Self);
            //}
            //else if (surfaceNormal == Vector3.back)
            //{
               
            //    Debug.Log("back");
            //    Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * Time.deltaTime;
            //    Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
            //    myTransform.Translate(rotation * direction, Space.Self);
            //}
        }
        
        //else
        //{
        //    Vector3 direction = new Vector3(-head.transform.forward.x, 0, head.transform.forward.z).normalized * Time.deltaTime;
        //    Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
        //    transform.Translate(rotation * direction, Space.Self);
        //    Debug.Log("dupa");
        //}


    }

   
    private void JumpToWall(Vector3 point, Vector3 normal)
    {
        // jump to wall
        jumping = true; // signal it's jumping to wall
        rb.isKinematic = true; // disable physics while jumping
        Vector3 orgPos = myTransform.position;
        Quaternion orgRot = myTransform.rotation;
        Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
        Vector3 myForward = Vector3.Cross(myTransform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);

        StartCoroutine(jumpTime(orgPos, orgRot, dstPos, dstRot, normal));
        //jumptime
    }
    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
    {
        for (float t = 0.0f; t < 1.0f;)
        {
            t += Time.deltaTime;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null; // return here next frame
        }
        myNormal = normal; // update myNormal
        rb.isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished

    }



}
