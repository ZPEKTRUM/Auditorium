using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public class MonScript : MonoBehaviour
{
    // Variables publiques accessibles depuis l'inspecteur
    public int vies = 3; // Nombre de vies du joueur
    public int points = 0; // Nombre de points du joueur

    // Fonction appelée quand le joueur perd une vie
    public void PerdreVie()
    {
        // Décrémente le nombre de vies
        vies--;

        // Si le nombre de vies est inférieur ou égal à zéro
        if (vies <= 0)
        {
            // Affiche un message dans la console
            Debug.Log("Game over");

            // Change de scène vers le menu
            ChangerScene("Menu");
        }
    }

    // Fonction appelée quand le joueur gagne un point
    public void GagnerPoint()
    {
        // Incrémente le nombre de points
        points++;

        // Affiche le nombre de points dans la console
        Debug.Log("Points : " + points);

        // Si le nombre de points est supérieur ou égal à 10
        if (points >= 10)
        {
            // Affiche un message dans la console
            Debug.Log("Bravo !");

            // Change de scène vers le niveau 2
            ChangerScene("Niveau2");
        }
    }

    // Fonction qui change de scène en fonction du nom passé en paramètre
    public void ChangerScene(string Access)
    {
        // Charge la scène dont le nom est passé en paramètre
        SceneManager.LoadScene(Access);
    }
}