using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level levelPrefab;
    // private List<Level> levels;

    private Level currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        Level firstLevel = AddLevel();
        firstLevel.AddObject().ScheduleSpawn(2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartLevel(Level level) {
        if (currentLevel != null) {
            currentLevel.EndLevel();
        }

        currentLevel = level;
        level.BeginLevel();
    }

    Level AddLevel() {
        Level level = Instantiate(levelPrefab, transform.position, Quaternion.identity);
        level.gameObject.SetActive(false);
        // level.transform.parent = this.transform;
        return level;
    }

    Level AdvanceLevel() {
        return null;
    }

    public Level CurrentLevel {
        get {
            return currentLevel;
        }

        set {
            currentLevel = value;
        }
    }

    
}
