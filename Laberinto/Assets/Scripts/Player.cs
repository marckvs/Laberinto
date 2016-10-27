using UnityEngine;
using System.Collections;

public class Player : Entity {

    private int level;
    private float currentLevelExperience;
    private float experinceToLevel;

    void Start()
    {
        LevelUp();
    }

    public void AddExperience(float exp)
    {
        currentLevelExperience += exp;
        if(currentLevelExperience >= experinceToLevel)
        {
            currentLevelExperience -= experinceToLevel;
            LevelUp();
        }

        Debug.Log("exp: " + currentLevelExperience + "     Level: " + level + "     ExperienceToLevel:  " + experinceToLevel);
    }

    private void LevelUp()
    {
        level++;
        experinceToLevel = level * 50 + Mathf.Pow(level * 2, 2);

        AddExperience(0);
    }

}
