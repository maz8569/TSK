using UnityEngine;
using UnityEngine.Events;

public class FollowTargetScript : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float fastSpeed = 15f;

    public float speed = 5.0f;
    public float rotateSpeed = 0.1f;

    private bool isSimulationActive = false;

    public Vector3 startingPosition;
    public Vector3 startingRotation;

    public Vector2 heading;

    Vector2 p1;
    Vector2 p2;

    private UnityAction<object> onSimulationStateChange;

    private void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation.eulerAngles;
    }

    private void Awake()
    {
        onSimulationStateChange = new UnityAction<object>(OnSimulationStateChange);
    }

    private void OnEnable()
    {
        EventManager.StartListening("SimulationState", onSimulationStateChange);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SimulationState", onSimulationStateChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSimulationActive)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = fastSpeed;
            }
            else
            {
                speed = normalSpeed;
            }

            heading.y = Input.GetAxisRaw("Horizontal");
            heading.x = Input.GetAxisRaw("Vertical");

            transform.position += Time.deltaTime * speed * new Vector3(transform.forward.x, 0, transform.forward.z) * heading.x;
            transform.position += Time.deltaTime * speed * new Vector3(transform.right.x, 0, transform.right.z) * heading.y;

            if (Input.GetKeyDown(KeyCode.V))
            {
                transform.position = startingPosition;
                transform.rotation = Quaternion.Euler(startingRotation);
            }

            if (Input.GetMouseButtonDown(2))
            {
                p1 = Input.mousePosition;
            }

            if (Input.GetMouseButton(2))
            {
                p2 = Input.mousePosition;

                float dx = (p2 - p1).x * rotateSpeed * Time.deltaTime;
                float dy = (p2 - p1).y * rotateSpeed * Time.deltaTime;

                transform.rotation = Quaternion.Euler(transform.eulerAngles.x - dy, transform.eulerAngles.y + dx, transform.eulerAngles.z) ;
                p1 = p2;
            }
        }
    }

    private void OnSimulationStateChange(object data)
    {
        isSimulationActive = (bool)data;
    }

}
