using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public string isLevelComplete = "no";
    public string levelPrefName;

    public GameObject crown;

    void Start()
    {
        isLevelComplete = PlayerPrefs.GetString(levelPrefName);

        bool isComplete = isLevelComplete == "yes";
        if (crown)
            crown.SetActive(isComplete);
    }

    public void SetToComplete()
    {
        PlayerPrefs.SetString(levelPrefName, "yes");
    }
}
