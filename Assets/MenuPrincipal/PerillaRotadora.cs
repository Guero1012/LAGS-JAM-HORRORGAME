using UnityEngine;

public class PerillaRotadora : MonoBehaviour
{
    public float duracionRotacion = 0.5f;

    private float[] angulosZ = { -90f, 0f, 90f };
    private int indiceActual = 1; // Empieza en 0°

    private Quaternion rotacionObjetivo;
    private float tiempoInicio;
    private bool rotando = false;

    void Update()
    {
        if (rotando)
        {
            float t = (Time.time - tiempoInicio) / duracionRotacion;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacionObjetivo, t);

            if (t >= 1f)
            {
                transform.localRotation = rotacionObjetivo;
                rotando = false;
            }
        }
    }

    // Llama esta función desde el botón y pasa el índice deseado: 0 = -90, 1 = 0, 2 = 90
    public void RotarAHaciaIndice(int nuevoIndice)
    {
        nuevoIndice = Mathf.Clamp(nuevoIndice, 0, angulosZ.Length - 1);

        if (nuevoIndice != indiceActual)
        {
            indiceActual = nuevoIndice;
            rotacionObjetivo = Quaternion.Euler(0f, 0f, angulosZ[indiceActual]);
            tiempoInicio = Time.time;
            rotando = true;
        }
    }
}
