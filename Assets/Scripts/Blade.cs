using UnityEngine;



public class Blade : MonoBehaviour
{
    public static Blade bladeInstance;

    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;


    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;
    private bool slicing;
    private float clickTimeIn, clickTimeOut;
    public bool click;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickTimeIn = Time.time;
            StarSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            clickTimeOut = Time.time;
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }

        float clickedTime = clickTimeOut - clickTimeIn;

        if (clickedTime > 0.001f && clickedTime < 0.1f)
        {
            click = true;
            clickTimeIn = 0f;
            clickTimeOut = 0f;
        }
        else if (clickedTime > 0.2f)
        {
            click = false;
        }
    }

    private void StarSlicing()
    {

        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();

    }



    public void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }

}

