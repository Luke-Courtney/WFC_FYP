using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Function to load the Overworld scene
    public void LoadOverworld()
    {
        SceneManager.LoadScene("Overworld");
    }

    // Function to load the Roads scene
    public void LoadRoads()
    {
        SceneManager.LoadScene("Roads");
    }

    // Function to load the Menu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene("WFC_Scene");
    }

    //Quit scene
    public void QuitApplication()
    {
        Application.Quit();
    }

    //Open Github page
    public void OpenGitURL()
    {
        Application.OpenURL("https://github.com/Luke-Courtney/WFC_FYP");
    }
}