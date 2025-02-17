using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelFolderName = "02DEV/Levels"; 

    public List<QuestionSo> selectedLevels = new List<QuestionSo>();

    void Start()
    {
        LoadLevels();
    }

    void LoadLevels()
    {
       
        QuestionSo[] allLevels = Resources.LoadAll<QuestionSo>(levelFolderName);

        if (allLevels.Length == 0)
        {
            Debug.LogError($"'{levelFolderName}' klasöründe hiç level bulunamadı!");
            return;
        }

        // Rastgele 10 tane seç
        selectedLevels = allLevels.OrderBy(x => Random.value).Take(10).ToList();

        // Kontrol amaçlı log bas
        Debug.Log($"'{levelFolderName}' klasöründen {selectedLevels.Count} level yüklendi.");
    }
}