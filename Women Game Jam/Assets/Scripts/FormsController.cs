using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FormsController : MonoBehaviour
{
    [SerializeField] List<Preguntas> preguntasForms = new List<Preguntas>();
    int preguntasIndex = 0;

    public TMP_Text txtPregunta;
    public TMP_Text txtRespuesta1;
    public TMP_Text txtRespuesta2;
    public GameObject formulario;
    public GameObject candidatoFinal;
    public TMP_Text descripcionCandidato;
    string[] respuestasElegidas = new string[5];
    Sprite[] respuestasElegidas_Imagenes = new Sprite[5];
    public Transform fotoPlaceHolder;
    int[] picturesLayerOrder = new int[4] { 3, 2, 4, 5 };
    int picturesLayerOrderIndex = 0;

    private void Start()
    {
        SiguientePregunta();
    }

    public void Continuar(int opcionElegida)
    {
        if (opcionElegida == 0)
        {
            respuestasElegidas[preguntasIndex] = preguntasForms[preguntasIndex].respuesta2;
            respuestasElegidas_Imagenes[preguntasIndex] = preguntasForms[preguntasIndex].respuesta2_Imagen;
        } 
        else
        {
            respuestasElegidas[preguntasIndex] = preguntasForms[preguntasIndex].respuesta1;
            respuestasElegidas_Imagenes[preguntasIndex] = preguntasForms[preguntasIndex].respuesta1_Imagen;
        }
            
        preguntasIndex++;
        if (preguntasIndex < preguntasForms.Count)
        {
            SiguientePregunta();
        }
        else
        {
            candidatoFinal.SetActive(true);
            formulario.SetActive(false);
            fotoPlaceHolder.tag = "Draggable";
            foreach (Sprite s in respuestasElegidas_Imagenes)
            {
                GameObject imagen = new GameObject();

                imagen.transform.SetParent(fotoPlaceHolder);
                imagen.transform.localPosition = Vector3.zero;
                imagen.transform.localRotation = Quaternion.identity;
                imagen.transform.localScale = Vector3.one;

                SpriteRenderer sr = imagen.AddComponent<SpriteRenderer>();
                MovementClamp mc = imagen.AddComponent<MovementClamp>();
                sr.sprite = s;
                if(picturesLayerOrderIndex < picturesLayerOrder.Length)
                {
                    sr.sortingOrder = picturesLayerOrder[picturesLayerOrderIndex];
                    picturesLayerOrderIndex++;
                }
                
            }
            descripcionCandidato.text = "Aquí tienes a tu pareja ideal, lee atentamente su perfil y acéptalo como el amor de tu vida!\n" +
                $"Ama a los {respuestasElegidas[0]}\n" +
                $"Prefiere los {respuestasElegidas[1]}\n" +
                $"Quisiera vacacionar siempre en {respuestasElegidas[2]}\n";
        }
        
    }

    public void SiguientePregunta()
    {
        txtPregunta.text = preguntasForms[preguntasIndex].pregunta;
        txtRespuesta1.text = preguntasForms[preguntasIndex].respuesta1;
        txtRespuesta2.text = preguntasForms[preguntasIndex].respuesta2;
    }
}
