using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementClamp))]
public class MovBotonNO : MonoBehaviour
{
    int[,] direcciones;
    bool moving = false;
    MovementClamp movementClamp;

    Vector3 posicionDireccion;
    Collider2D botonNO;
    [SerializeField] Collider2D botonPlaceHolder;
    [SerializeField] float velocidad = 35f;
    [SerializeField] float distanciaMovimiento = 3f;
    float distanciaRecorrida = 0f;

    bool posicionable = false;

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
            //transform.position += new Vector3(posicionDireccion.x, posicionDireccion.y) * 35f * Time.deltaTime;

            //transform.position = movementClamp.KeepInsideScreen(transform.position);

            //if(BotonPosicionado())
            //{
            //    moving = false;
            //}

            float distanciaFrame = velocidad * Time.deltaTime;

            //distanciaFrame = Mathf.Min(distanciaFrame, distanciaMovimiento - distanciaRecorrida);

            Vector3 desplazamiento = posicionDireccion * distanciaFrame;

            transform.position += desplazamiento;
            transform.position = movementClamp.KeepInsideScreen(transform.position);

            //distanciaRecorrida += desplazamiento.magnitude;

            //if (distanciaRecorrida >= distanciaMovimiento)
            //{
            //    posicionable = true;
            //    moving = false;
            //}

            if (BotonSeparado())
            {
                Debug.Log("Botón separado del placeholder");
                moving = false;
                posicionable = true;
            }

            //if (posicionable && BotonPosicionado())
            //{
            //    //moving = false;
            //    FindAnyObjectByType<AceptarCandidato>().MostrarFinal("salió bien, aprendiste a decir que no");
            //}

        }

        if (posicionable && BotonPosicionado())
        {
            //moving = false;
            posicionable = false;

            FindAnyObjectByType<AceptarCandidato>().MostrarFinal("Salió bien, aprendiste a decir que no");
        }
    }

    private void OnMouseEnter()
    {
        if (moving)
            return;

        if (posicionable && BotonPosicionado())
            return;

        posicionable = false;

        CambiarDireccion();

        distanciaRecorrida = 0f;
        moving = true;
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
        //if (botonNO == null || botonPlaceHolder == null)
        //    return false;

        //ColliderDistance2D distancia = botonNO.Distance(botonPlaceHolder);

        //return !distancia.isOverlapped;
        Bounds a = botonNO.bounds;
        Bounds b = botonPlaceHolder.bounds;

        bool separadoEnX =
            a.max.x <= b.min.x || a.min.x >= b.max.x;

        bool separadoEnY =
            a.max.y <= b.min.y || a.min.y >= b.max.y;

        return separadoEnX || separadoEnY;
    }
}
