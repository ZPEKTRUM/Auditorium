using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class ParticlekeyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;

    // Si jamais on a renommé le champs pour un meilleur nom, on veut garder la conf précédente, on peut utiliser
    // l'attribut FormerlySerializedAs pour lui donner l'ancien nom du champs pour conserver sa valeur
    [SerializeField, FormerlySerializedAs("_lifeTime")] float _coucou;

    float _currentLifeTime;

    // On cherche à configurer automatiquement le composant dès que le GD ajoute notre composant
    private void Reset()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = 10f;
        _coucou = 3f;
    }

    void Start()
    {
        // Destroy peut etre appellé avec 2 paramètres : l'objet à detruire ainsi que le delay pour le destroy
        //Destroy(gameObject, 10f);

        // Demander à Unity d'appeller cette fonction pour nous dans XX secondes
        //Invoke("InvokeDestroy", 10f);

        // On donne une impulsion de départ à notre particule
        _rb.velocity = transform.right * _speed;
    }

    //void InvokeDestroy()
    //{
    //    Destroy(gameObject);
    //}

    // Version de la gestion du temps avec notre "sablier" qui va venir se cumuler progressivement
    private void Update()
    {
        _currentLifeTime += Time.deltaTime;     // On ajoute un nouveau petit temps dans notre sablier

        // Si le sablier a dépassé une durée de vie => on peut la détruire
        if (_currentLifeTime > _coucou)
        {
            Destroy(gameObject);
        }
    }


}