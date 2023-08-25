using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _fireRate;
    int _counter;

    private void Start()
    {
        // Pour demander à Unity quand on veut appeller une fonction dans XX secondes
        //Invoke(nameof(SpawnParticle), _fireRate);
        //Invoke("SpawnParticle", _fireRate);


        // On demande à Unity d'appeller la fonction de Spawn toutes les _fireRate secondes
        InvokeRepeating("SpawnParticle", 0f, _fireRate);


        // Version Coroutine un peu plus avancée
        //StartCoroutine(SpawnRoutine());
    }



    void SpawnParticle()
    {
        // Mecanique pour annuler l'appel réccurent une fois que l'on a dépassé un nombre d'appel
        //_counter++;
        //if(_counter >= 10)
        //{
        //    CancelInvoke("SpawnParticle");
        //}

        // Position aléatoire dans un cercle pour faire un decalage avec la position du spawner
        Vector3 randomDirection = Random.insideUnitCircle;

        // On calcule la future position de la prochaine particule
        Vector3 destination = transform.position + randomDirection;

        // On instantie la nouvelle particule 
        Instantiate(_prefab, destination, transform.rotation);


        // Random direction version "carré"
        //Vector3 randomDirection = new Vector2( Random.Range(-1f, 1f), Random.Range(-1f, 1f) );
    }


    // Version coroutine, plus avancée que l'on verra plus tard
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnParticle();
            yield return new WaitForSeconds(_fireRate);
        }
    }

}