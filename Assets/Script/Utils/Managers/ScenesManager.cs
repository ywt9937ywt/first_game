using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager SMInstance;
    // Start is called before the first frame update
    private void Awake()
    {
        SMInstance = this;
    }

    public enum Scene
    {
        MainMenu,
        WorldLevel,
        VillageLevel,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadVillage()
    {
        SceneManager.LoadScene(Scene.VillageLevel.ToString());
    }
}
