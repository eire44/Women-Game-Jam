using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] AudioSource audioSource_Click;

    [SerializeField] Transform pInicio;
    [SerializeField] Transform pCreditos;
    [SerializeField] TMP_Text gameTitle;
    [SerializeField] TMP_Text gameTitle_Shadow;
    //public Transform pOpciones;

    [SerializeField] string nombreEscena;
    public static bool gameCompleted = false;

    public void Jugar()
    {
        audioSource_Click.Play();
        SceneManager.LoadScene(nombreEscena);
    }

    public void Salir()
    {
        audioSource_Click.Play();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                                                    Application.Quit();
        #endif
    }

    public void irACreditos()
    {
        audioSource_Click.Play();
        pInicio.gameObject.SetActive(false);
        pCreditos.gameObject.SetActive(true);
    }

    public void irAOpciones()
    {
        audioSource_Click.Play();
        pInicio.gameObject.SetActive(false);
        //pOpciones.gameObject.SetActive(true);
    }

    public void volverAlMenu()
    {
        audioSource_Click.Play();
        pInicio.gameObject.SetActive(true);
        //pOpciones.gameObject.SetActive(false);
        pCreditos.gameObject.SetActive(false);
    }
}
