using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public string SampleScene;

    public void ChangScene()
    {
        SceneManager.LoadScene(SampleScene);

    }

}
