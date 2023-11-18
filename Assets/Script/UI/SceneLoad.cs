using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    public void LoadSceneByEnum(craftManager.Scenename sceneName)
    {
        // ������ ����� ����Ͽ� �� �̸��� �����ϰ� �ε��մϴ�.
        string sceneToLoad = sceneName.ToString();
        SceneManager.LoadScene(sceneToLoad);
    }
}
