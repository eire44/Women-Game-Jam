using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    //[SerializeField] AudioSource audioSource_Click;

    [SerializeField] Transform pInicio;
    //[SerializeField] Transform pCreditos;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void Jugar()
    {
        Time.timeScale = 1f;
        pInicio.gameObject.SetActive(false);
        //audioSource_Click.Play();
    }

    public void Salir()
    {
        //audioSource_Click.Play();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                                                    Application.Quit();
        #endif
    }

    public void Creditos()
    {
        //audioSource_Click.Play();
        pInicio.gameObject.SetActive(!pInicio.gameObject.activeInHierarchy);
        //pCreditos.gameObject.SetActive(!pCreditos.gameObject.activeInHierarchy);
    }
}
