using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Description: This is a class designed to handle loading different scenes
public static class Loader
{
    public enum Scene
    {
        GameScene,
        Loading,

    }
    private static Action onLoaderCallback;
    public static void Load(Scene s)
    {
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(s.ToString());
        };
        SceneManager.LoadScene(Scene.Loading.ToString());
    }
    //create a temporary function that deletes itself after displaying the loading scene
    public static void LoaderCallBack()
    {
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
