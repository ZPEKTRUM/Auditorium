using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class ParticlekeyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;

    // Si jamais on a renomm� le champs pour un meilleur nom, on veut garder la conf pr�c�dente, on peut utiliser
    // l'attribut FormerlySerializedAs pour lui donner l'ancien nom du champs pour conserver sa valeur
    [SerializeField, FormerlySerializedAs("_lifeTime")] float _coucou;

    float _currentLifeTime;

    // On cherche � configurer automatiquement le composant d�s que le GD ajoute notre composant
    private void Reset()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = 10f;
        _coucou = 3f;
    }

    void Start()
    {
        // Destroy peut etre appell� avec 2 param�tres : l'objet � detruire ainsi que le delay pour le destroy
        //Destroy(gameObject, 10f);

        // Demander � Unity d'appeller cette fonction pour nous dans XX secondes
        //Invoke("InvokeDestroy", 10f);

        // On donne une impulsion de d�part � notre particule
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

        // Si le sablier a d�pass� une dur�e de vie => on peut la d�truire
        if (_currentLifeTime > _coucou)
        {
            Destroy(gameObject);
        }
    }


}