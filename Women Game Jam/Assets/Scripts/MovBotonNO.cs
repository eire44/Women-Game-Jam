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
            transform.position += new Vector3(posicionDireccion.x, posicionDireccion.y) * 35f * Time.deltaTime;
            
            transform.position = movementClamp.KeepInsideScreen(transform.position);
            
            if(BotonPosicionado())
            {
                moving = false;
            }

        }
    }

    private void OnMouseEnter()
    {
        if (moving) return;

        CambiarDireccion();

        moving = true;
    }

    void CambiarDireccion()
    {
        int randomIndex = Random.Range(0, direcciones.GetLength(0));

        int direccionX = direcciones[randomIndex, 0];
        int direccionY = direcciones[randomIndex, 1];

        posicionDireccion = new Vector3(direccionX, direccionY, 0);

    }

    private void OnMouseExit()
    {
        moving = false;
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
}
