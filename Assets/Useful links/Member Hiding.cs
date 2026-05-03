using UnityEngine;
using System.Collections;

public class Humanoid
{
    public void Yell()
    {
        Debug.Log("Humanoid version of the Yell() method");
    }
}

public class ScriptEnemy : Humanoid
{
    new public void Yell()
    {
        Debug.Log("Enemy version of the Yell() method");
    }
}

public class Orc : ScriptEnemy
{
    new public void Yell()
    {
        Debug.Log("Orc version of the Yell() method");
    }
}

public class WarBand : MonoBehaviour
{
    void Start()
    {
        Humanoid human = new Humanoid();
        Humanoid enemy = new ScriptEnemy();
        Humanoid orc = new Orc();

        human.Yell();
        enemy.Yell();
        orc.Yell();
    }
}