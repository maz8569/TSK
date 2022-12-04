using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshFilter))]
public class WallMeshGenerator : MonoBehaviour
{

    public Bullet bullet;

    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangless;
    public float second = 0;
    private UnityAction<object> onShoot;
    private UnityAction<object> onHit;

    private void Awake()
    {
        onShoot = new UnityAction<object>(OnShoot);
        onHit = new UnityAction<object>(OnHit);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Shoot", onShoot);
        EventManager.StartListening("Hit", onHit);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Shoot", onShoot);
        EventManager.StopListening("Hit", onHit);
    }
    // Start is called before the first frame update
    void Start()
    {
        vertices = new List<Vector3>();
        vertices.Add(new Vector3());
        triangless = new List<int>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void MeshUpdate()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangless.ToArray();

        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet.isShoot && bullet.isSimulationActive)
        {
            if (bullet.timeSpend > second)
            {
                vertices.Add(bullet.transform.position);
                vertices.Add(new Vector3(bullet.transform.position.x, 0, bullet.transform.position.z));

                triangless.Add(vertices.Count - 1);
                triangless.Add(vertices.Count - 2);
                triangless.Add(vertices.Count - 3);
                triangless.Add(vertices.Count - 4);
                triangless.Add(vertices.Count - 2);
                triangless.Add(vertices.Count - 3);

                second += 0.1f;

                MeshUpdate();
            }
        }
    }

    private void OnShoot(object data)
    {
        second = 0;
        GetComponent<MeshRenderer>().enabled = false;
        mesh.Clear();
        ClearVertices();
        vertices.Add(bullet.transform.position);
        vertices.Add(new Vector3(bullet.transform.position.x, 0, bullet.transform.position.z));
    }

    private void OnHit(object data)
    {
        vertices.Add(bullet.transform.position);

        triangless.Add(vertices.Count - 1);
        triangless.Add(vertices.Count - 2);
        triangless.Add(vertices.Count - 3);

        MeshUpdate();
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void ClearVertices()
    {
        vertices.Clear();
        triangless.Clear();
    }

}
