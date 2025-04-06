using UnityEngine;

public class CharacterViewer : MonoBehaviour
{
    public GameObject[] characters; // Tus personajes asignados en el inspector
    private int currentIndex = 0;

    void Start()
    {
        ShowCharacter(currentIndex);
    }

    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= characters.Length)
            currentIndex = 0;

        ShowCharacter(currentIndex);
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = characters.Length - 1;

        ShowCharacter(currentIndex);
    }

    private void ShowCharacter(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == index);
        }
    }
}
