using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento3D : MonoBehaviour
{
    public float velocidad = 6f;
    public float fuerzaSalto = 6f;

    private Rigidbody rb;
    private bool enSuelo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moverX = Input.GetAxis("Horizontal");
        float moverZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moverX * velocidad, rb.linearVelocity.y, moverZ * velocidad);
        rb.linearVelocity = movimiento;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }

        if (transform.position.y < -3f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        enSuelo = true;
    }
}