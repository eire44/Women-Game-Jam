using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    Camera mainCamera;
    Vector3 offset;
    //[SerializeField] MovementClamp movementClamp;

    enum Estado 
    {
        dragging,
        clicked,
        released
    }

    Estado estadoActual = Estado.released;
    GameObject interactingObject;


private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = GetMouseWorldPosition();

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if(hit.collider.gameObject.CompareTag("Draggable"))
                {
                    interactingObject = hit.collider.gameObject;
                    offset = interactingObject.transform.position - mousePosition;
                    estadoActual = Estado.dragging;
                } 
                else if (hit.collider.gameObject.CompareTag("Accept Button"))
                {
                    FindAnyObjectByType<AceptarCandidato>().MostrarFinal("salió mal");
                }
                else if (hit.collider.gameObject.CompareTag("Forms Option Button"))
                {
                    Respuestas respuesta_Indice = hit.collider.gameObject.GetComponent<Respuestas>();
                    if (respuesta_Indice != null)
                    {
                        FindAnyObjectByType<FormsController>().Continuar(respuesta_Indice.indice);
                    }
                    
                }
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            estadoActual = Estado.released;
            interactingObject = null;
        }

        if (estadoActual == Estado.dragging)
        {
            //interactingObject.transform.position = GetMouseWorldPosition() + offset;

            Vector3 mousePosition = GetMouseWorldPosition();

            Vector3 elementNewPosition = mousePosition + offset;

            MovementClamp movementClamp = interactingObject.GetComponent<MovementClamp>();

            if(movementClamp != null)
            {
                interactingObject.transform.position = movementClamp.KeepInsideScreen(elementNewPosition);
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, -mainCamera.transform.position.z);

        return mainCamera.ScreenToWorldPoint(screenPosition);
    }
}
