using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementClamp))]
public class MovBotonNO : MonoBehaviour
{
    int[,] direcciones;
    bool moving = false;
    MovementClamp movementClamp;
    [SerializeField] AudioSource audioSource_Escape;
    [SerializeField] AudioSource audioSource_Click;

    Vector3 posicionDireccion;
    Collider2D botonNO;
    [SerializeField] Collider2D botonPlaceHolder;
    [SerializeField] float velocidad = 35f;
    [SerializeField] float distanciaMovimiento = 3f;
    float distanciaRecorrida = 0f;

    bool posicionable = false;
    bool fijo = false;

    void Start()
    {
        botonNO = GetComponent<Collider2D>();
        movementClamp = GetComponent<MovementClamp>();
        direcciones = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }; //derecha, izquierda, arriba, abajo
    }

    void Update()
    {
         if(moving)
        {
            float distanciaFrame = velocidad * Time.deltaTime;

            float distanciaRestante = distanciaMovimiento - distanciaRecorrida;
            distanciaFrame = Mathf.Min(distanciaFrame, distanciaRestante);

            Vector3 desplazamiento = posicionDireccion.normalized * distanciaFrame;

            transform.position += desplazamiento;
            transform.position = movementClamp.KeepInsideScreen(transform.position);

            distanciaRecorrida += desplazamiento.magnitude;

            if (distanciaRecorrida >= distanciaMovimiento)
            {
                moving = false;
                distanciaRecorrida = 0f;
            }

            if (BotonSeparado())
            {
                posicionable = true;
            }
        }

        if (posicionable && BotonPosicionado() && !moving)
        {
            fijo = true;
        }
    }

    private void OnMouseEnter()
    {
        if (moving || fijo)
            return;

        CambiarDireccion();

        distanciaRecorrida = 0f;
        moving = true;
        audioSource_Escape.Play();
    }

    private void OnMouseUpAsButton()
    {
        if (fijo && !moving && BotonPosicionado())
        {
            posicionable = false;
            audioSource_Click.Play();
            FindAnyObjectByType<AceptarCandidato>().MostrarFinal(
                "¡DIJISTE QUE NO!\n\n¿OTRA VEZ QUE NO?\n\n" +
                "Lo bueno es que sabes lo que querés y no aceptás una cita solo por lástima, compromiso o porque una aplicación te obligue.\n\n" +
                "Tu vida amorosa es un desastre, pero al menos es TU desastre."
            );
        }
    }

    void CambiarDireccion()
    {
        int randomIndex = Random.Range(0, direcciones.GetLength(0));

        int direccionX = direcciones[randomIndex, 0];
        int direccionY = direcciones[randomIndex, 1];

        posicionDireccion = new Vector3(direccionX, direccionY, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        if(moving)
        {
            CambiarDireccion();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    bool BotonPosicionado()
    {
        Bounds a = botonNO.bounds;
        Bounds b = botonPlaceHolder.bounds;

        float ancho = Mathf.Max(0f, Mathf.Min(a.max.x, b.max.x) - Mathf.Max(a.min.x, b.min.x));

        float alto = Mathf.Max(0f, Mathf.Min(a.max.y, b.max.y) - Mathf.Max(a.min.y, b.min.y));

        float areaSuperpuesta = ancho * alto;
        float areaObjeto = a.size.x * a.size.y;

        if (areaObjeto <= 0f)
            return false;

        float porcentaje = areaSuperpuesta / areaObjeto;

        return porcentaje >= 0.7f;
    }

    bool BotonSeparado()
    {
        Bounds a = botonNO.bounds;
        Bounds b = botonPlaceHolder.bounds;

        bool separadoEnX =
            a.max.x <= b.min.x || a.min.x >= b.max.x;

        bool separadoEnY =
            a.max.y <= b.min.y || a.min.y >= b.max.y;

        return separadoEnX || separadoEnY;
    }
}
