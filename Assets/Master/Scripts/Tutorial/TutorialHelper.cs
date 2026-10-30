using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHelper : MonoBehaviour
{
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTutorial();
        }
    }

    private void ResetTutorial()
    {
        SaveGameService.ResetTutorialProgress();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endif

}
