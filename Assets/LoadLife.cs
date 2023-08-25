using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public class MonScript : MonoBehaviour
{
    // Variables publiques accessibles depuis l'inspecteur
    public int vies = 3; // Nombre de vies du joueur
    public int points = 0; // Nombre de points du joueur

    // Fonction appel�e quand le joueur perd une vie
    public void PerdreVie()
    {
        // D�cr�mente le nombre de vies
        vies--;

        // Si le nombre de vies est inf�rieur ou �gal � z�ro
        if (vies <= 0)
        {
            // Affiche un message dans la console
            Debug.Log("Game over");

            // Change de sc�ne vers le menu
            ChangerScene("Menu");
        }
    }

    // Fonction appel�e quand le joueur gagne un point
    public void GagnerPoint()
    {
        // Incr�mente le nombre de points
        points++;

        // Affiche le nombre de points dans la console
        Debug.Log("Points : " + points);

        // Si le nombre de points est sup�rieur ou �gal � 10
        if (points >= 10)
        {
            // Affiche un message dans la console
            Debug.Log("Bravo !");

            // Change de sc�ne vers le niveau 2
            ChangerScene("Niveau2");
        }
    }

    // Fonction qui change de sc�ne en fonction du nom pass� en param�tre
    public void ChangerScene(string Access)
    {
        // Charge la sc�ne dont le nom est pass� en param�tre
        SceneManager.LoadScene(Access);
    }
}