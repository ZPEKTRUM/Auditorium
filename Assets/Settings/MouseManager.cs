using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    [SerializeField] Camera _camera;

    [SerializeField] InputActionReference _mousePos;
    [SerializeField] InputActionReference _mouseClick;

    [SerializeField] Texture2D _resizeIcon;
    [SerializeField] Texture2D _moveIcon;

    GameObject _currentMoveCircle;
    GameObject _currentResizeCircle;


    void Update()
    {
        // On récupère si le bouton gauche est pressé ou pas
        bool isButtonPressed;
        isButtonPressed = _mouseClick.action.IsPressed();
        //Debug.Log($"Left button : {isButtonPressed}");

        // On récupère les coordonnées de la souris
        Vector2 mousePosition;
        mousePosition = _mousePos.action.ReadValue<Vector2>();
        //Debug.Log($"Mouse Pos : {mousePosition}");


        // On récupère un rayon de la part de la camera par rapport à la position du curseur du jeu
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        // On demande au moteur physique de produire le raycast pour detecter un collider sur le chemin
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green);

        // On écrit le nom du collider que l'on vient de toucher
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.gameObject.CompareTag("Resize"))
            {
                Cursor.SetCursor(_resizeIcon, Vector2.zero, CursorMode.ForceSoftware);

                // Ici le curseur est au dessus du cercle violet + le joueur a cliqué
                if (isButtonPressed)
                {
                    _currentResizeCircle = hit.collider.gameObject;
                }
                else
                {
                    _currentResizeCircle = null;
                }

            }
            else if (hit.collider.gameObject.CompareTag("Move") && _currentResizeCircle == null)
            {
                Cursor.SetCursor(_moveIcon, Vector2.zero, CursorMode.ForceSoftware);

                // Ici le curseur est au dessus du cercle violet + le joueur a cliqué
                if (isButtonPressed)
                {
                    _currentMoveCircle = hit.collider.gameObject;
                }
                else
                {
                    _currentMoveCircle = null;
                }

            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        }


        // On met à jour nos cercles
        if (_currentMoveCircle != null)
        {
            // Pour bouger l'effector on doit déplacer l'objet par rapport à la position du curseur.
            // Pour ça on demande à la camera de nous fournir la position du monde pour notre coordonnée de souris
            Vector3 pos = _camera.ScreenToWorldPoint(mousePosition);
            Debug.Log(pos);
            // Le ScreenToWorldPoint nous donne un Z=-10 qui est pas top pour notre jeu, on retravail donc ce Z avant de le claquer dans la nouvelle position de notre objet
            pos.z = 0;
            // 
            _currentMoveCircle.transform.parent.parent.position = pos;
        }
        if (_currentResizeCircle != null)
        {
            //_currentMoveCircle.GetComponent<CircleShape>().Radius = Mathf.Clamp(Vector2.Distance(_currentResizeCircle.transform.position, _camera.ScreenToWorldPoint(mousePosition)), 1, 10);

            // Récupère la position du curseur dans le monde
            Vector3 pos = _camera.ScreenToWorldPoint(mousePosition);

            // On calcule distance entre le cercle et le curseur, on obtient le radius
            var dist = Vector2.Distance(_currentResizeCircle.transform.position, pos);

            // On choppe le composant CircleShape et on lui change son radius
            var cs = _currentResizeCircle.GetComponent<CircleShape>();
            cs.Radius = dist;

            cs.Radius = Mathf.Clamp(cs.Radius, 1, 10);

            // Equivalent du clamp
            //if(cs.Radius < 1) cs.Radius = 1;
            //if (cs.Radius > 10) cs.Radius = 10;

        }

        #region 
        // Explication de base du raycast
        Vector3 origin = new Vector3(1, 1, 0);
        Vector3 dir = Vector3.right * 2;
        if (Physics2D.Raycast(origin, dir, 2f))
        {
            Debug.DrawRay(origin, dir, Color.green);
        }
        else
        {
            Debug.DrawRay(origin, dir, Color.red);
        }
        #endregion


    }
}