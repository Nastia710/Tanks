using UnityEngine;
using System.Collections;

public class ScriptFruit
{
    public ScriptFruit()
    {
        Debug.Log("1st Fruit Constructor Called");
    }

    public virtual void Chop()
    {
        Debug.Log("The fruit has been chopped.");
    }

    public virtual void SayHello()
    {
        Debug.Log("Hello, I am a fruit.");
    }
}

public class ScriptApple : ScriptFruit
{
    public ScriptApple()
    {
        Debug.Log("1st Apple Constructor Called");
    }

    public override void Chop()
    {
        base.Chop();
        Debug.Log("The apple has been chopped.");
    }

    public override void SayHello()
    {
        base.SayHello();
        Debug.Log("Hello, I am an apple.");
    }
}

public class ScriptFruitSalad : MonoBehaviour
{
    void Start()
    {
        ScriptApple myApple = new ScriptApple();

        myApple.SayHello();
        myApple.Chop();

        ScriptFruit myFruit = new ScriptApple();
        myFruit.SayHello();
        myFruit.Chop();
    }
}