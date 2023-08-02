using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MusicBox : MonoBehaviour
{
    [SerializeField] int _validationThreshold;
    [SerializeField] float _objectiveTime;
    [SerializeField] AudioSource _audio;

    [Header("Jauge")]
    [SerializeField] Color _activatedColor;
    [SerializeField] Color _desactivatedColor;
    [SerializeField] List<SpriteRenderer> _jauges;

    // L'ancienne version avec la méthode laborieuse dans UpdateVisual
    //[SerializeField] SpriteRenderer _jauge1;
    //[SerializeField] SpriteRenderer _jauge2;
    //[SerializeField] SpriteRenderer _jauge3;
    //[SerializeField] SpriteRenderer _jauge4;
    //[SerializeField] SpriteRenderer _jauge5;

    [Header("DEBUG")]
    [Header("Le nombre de particule dans la zone")]
    [SerializeField] int _particleCount;
    [Header("Le nombre de secondes objectif a valid")]
    [SerializeField] float _currentTime;
    [Header("Le taux de compltion du niveau entre 0 et 1 (0% => 100%)")]
    [SerializeField] float _currentPercentage;




    private void Update()
    {
        UpdateCompletion();

        CompletionTest();
    }

    private void CompletionTest()
    {
        // Victoire : si la compltion a dpass XX secondes
        if (_currentTime > _objectiveTime)
        {
            Debug.Log("WIN");
            //SceneManager.LoadScene("WinScreen");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateCompletion()
    {
        // Threshold 
        if (_particleCount >= _validationThreshold)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime < 0)
            {
                _currentTime = 0;
            }
        }

        _currentPercentage = _currentTime / _objectiveTime;

        // On met ? jour le volume par rapport au taux de completion de la jauge
        _audio.volume = _currentPercentage;
        UpdateVisual();

    }

    private void UpdateVisual()
    {
        // On calcul les tranches de notre jauges
        float part = 1f / _jauges.Count;             // float / int ==> int 

        // On calcule le nombre de jauges ? activer
        int nbActivated = (int)(_currentPercentage / part);

        // On parcours le tableau et on lui cale la couleur qui lui correspond par rapport ? notre compteur de lampe ? activer
        for (int i = 0; i < _jauges.Count; i++)
        {
            // Si on a une 
            if (nbActivated > 0)
            {
                _jauges[i].color = _activatedColor;
                nbActivated--;
            }
            else
            {
                _jauges[i].color = _desactivatedColor;
            }
        }

        return;

#if false
        //0.0-0.2 /  0 lumi?re
        //0.2-0.4 /  1 lumi?re
        //0.4-0.6 /  2 lumi?re
        //0.6-0.8 /  3 lumi?re
        //0.8-1.0 /  4 lumi?re        
        // 1         5 lumi?re

        // La méthode laborieuse qui est pas trs lgante mais au moins elle marche
        if (_currentPercentage < 0.2)    // On desactive tout
        {
            _jauge1.color = _desactivatedColor;
            _jauge2.color = _desactivatedColor;
            _jauge3.color = _desactivatedColor;
            _jauge4.color = _desactivatedColor;
            _jauge5.color = _desactivatedColor;
        }
        else if (_currentPercentage >= 1) // 5
        {
            _jauge1.color = _activatedColor;
            _jauge2.color = _activatedColor;
            _jauge3.color = _activatedColor;
            _jauge4.color = _activatedColor;
            _jauge5.color = _activatedColor;
        }
        else if (_currentPercentage > 0.8) // 4
        {
            _jauge1.color = _activatedColor;
            _jauge2.color = _activatedColor;
            _jauge3.color = _activatedColor;
            _jauge4.color = _activatedColor;
            _jauge5.color = _desactivatedColor;
        }
        else if (_currentPercentage > 0.6)   // 3
        {
            _jauge1.color = _activatedColor;
            _jauge2.color = _activatedColor;
            _jauge3.color = _activatedColor;
            _jauge4.color = _desactivatedColor;
            _jauge5.color = _desactivatedColor;
        }
        else if (_currentPercentage > 0.4)   // En allumer 2
        {
            _jauge1.color = _activatedColor;
            _jauge2.color = _activatedColor;
            _jauge3.color = _desactivatedColor;
            _jauge4.color = _desactivatedColor;
            _jauge5.color = _desactivatedColor;
        }
        else if (_currentPercentage > 0.2)    // Allumer une loupiotte
        {
            _jauge1.color = _activatedColor;
            _jauge2.color = _desactivatedColor;
            _jauge3.color = _desactivatedColor;
            _jauge4.color = _desactivatedColor;
            _jauge5.color = _desactivatedColor;
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.CompareTag("Particle") == false) return;

        _particleCount++;

        // Methode 2
        //if (collision.attachedRigidbody.gameObject.CompareTag("Particle") == true)
        //{
        //    _particleCount++;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.gameObject.CompareTag("Particle") == false) return;
        _particleCount--;
    }

}