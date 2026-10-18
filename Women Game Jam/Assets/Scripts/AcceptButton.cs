using UnityEngine;

public class AcceptButton : MonoBehaviour
{
    [SerializeField] AudioSource audioSource_Click;
    private void OnMouseUpAsButton()
    {
        audioSource_Click.Play();
        FindAnyObjectByType<AceptarCandidato>().MostrarFinal("¡Gracias por confiar en el amor!\r\n\nSe casaron a las dos semanas.\r\nSe divorciaron al mes.\r\nLa app te cobró la suscripción premium.\r\n");
    }
}
