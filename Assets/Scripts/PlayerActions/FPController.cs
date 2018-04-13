using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class FPController : MonoBehaviour
{

    // public vars
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 6;
    public float jumpForce = 220;
    public LayerMask groundedMask;
    public int PlanetIntegers = 0;
    public AudioClip[] Music = new AudioClip[10];
    // System vars
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    Transform cameraTransform;
    new Rigidbody rigidbody;
    bool PlayerMovementEnabled = true;
   // bool PlayerLookControls = true;
    AudioSource glasba;
    FauxGA planet;

    void Awake()
    {
        //Cursor, kamera in rigidbody
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();
        // Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        //izbor najbljižjega planeta
        planet = FindClosestPlanet().GetComponent<FauxGA>();
        //doda audiosource iz playerju
        glasba = gameObject.AddComponent<AudioSource>();
    }
    void Update()
    {
        //Look rotation for mouse and keyboard:
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
 
        // Calculate movement:
        if (PlayerMovementEnabled)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
            Vector3 targetMoveAmount = moveDir * walkSpeed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        }
        // sledenje pritiskom na hotkeye
        if (Input.GetKeyDown("1"))
        {   PlanetJump(1);
            Debug.Log("Planet Number: 1");}
        else if (Input.GetKeyDown("2"))
        {   PlanetJump(2);
            Debug.Log("Planet Number: 2");}
        else if (Input.GetKeyDown("3"))
        {   PlanetJump(3);
            Debug.Log("Planet Number: 3");}
        else if (Input.GetKeyDown("4"))
        {   PlanetJump(4);
            Debug.Log("Planet Number: 4");}
        else if (Input.GetKeyDown("5"))
        {   PlanetJump(5);
            Debug.Log("Planet Number: 5");}
        else if (Input.GetKeyDown("6"))
        {   PlanetJump(6);
            Debug.Log("Planet Number: 6");}
        else if (Input.GetKeyDown("7"))
        {    PlanetJump(7);
            Debug.Log("Planet Number: 7");}
        else if (Input.GetKeyDown("8"))
        {   PlanetJump(8);
            Debug.Log("Planet Number: 8");}

        /* Look rotation Accelerometer
       // transform.Rotate(Input.acceleration.x, 0, -Input.acceleration.z);
        transform.Rotate(-(Vector3.up * Input.acceleration.x * mouseSensitivityX));
        verticalLookRotation += Input.acceleration.y * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;*/

        /* Jumping mechanic
        if (Input.GetButtonDown("Jump"))
        {if (grounded){rigidbody.AddForce(transform.up * jumpForce);}}
        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)){grounded = true;}
        else{grounded = false;}*/
    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
        if (PlayerMovementEnabled)
        {
            Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
            rigidbody.MovePosition(rigidbody.position + localMove);
        }
        // find nearest planet and Allow this body to be influenced by planet's gravity
        planet = FindClosestPlanet().GetComponent<FauxGA>();
        planet.Attract(rigidbody);
        
    }
    // koda za shortkeye za testirat reči
    public void PlanetJump(int PressedKey)
    {
        switch (PressedKey)
        {
            case 1://Koda za dejanski premik igralca med planeti nujni note, planet Attract je potrebno ponovno aktivirat. 
                iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("P1-P2"), "speed", 15));
                planet.Attract(rigidbody);
                break;
            case 2:
                glasba.Stop();
                glasba.PlayOneShot(Music[0],5);
                break;
            case 3:
                glasba.Stop();
                glasba.PlayOneShot(Music[1], 5);
                break;
            case 4:
                glasba.Stop();
                glasba.PlayOneShot(Music[2], 5);
                break;
            case 5:
                glasba.Stop();
                glasba.PlayOneShot(Music[0], 5);
                break;
            case 6:
                glasba.Stop();
                glasba.PlayOneShot(Music[0], 5);
                break;
            case 7:
                glasba.Stop();
                glasba.PlayOneShot(Music[0], 5);
                break;
            case 8:
                glasba.Stop();
                glasba.PlayOneShot(Music[0], 5);
                break;
        }
    }
    //funkcija za iskanje najbljižjega planeta
    public GameObject FindClosestPlanet()
    {
        GameObject[] Planets;
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        GameObject NearestPlanet = null;
        float Razdalja = Mathf.Infinity;
        Vector3 pozicija = transform.position;
        foreach (GameObject Pl in Planets)
        {
            Vector3 razlika = Pl.transform.position - pozicija;
            float trenutnarazdalja = razlika.sqrMagnitude;
            if (trenutnarazdalja < Razdalja)
            {
                NearestPlanet = Pl;
                Razdalja = trenutnarazdalja;
            }
        }
        return NearestPlanet;
    }

}