using UnityEngine;
using System.Collections;

public class ScriptSomeClass
{
    public T GenericMethod<T>(T param)
    {
        return param;
    }
}

public class ScriptSomeOtherClass : MonoBehaviour
{
    void Start()
    {
        ScriptSomeClass myClass = new ScriptSomeClass();

        myClass.GenericMethod<int>(5);
    }
}

public class GenericClass<T>
{
    T item;

    public void UpdateItem(T newItem)
    {
        item = newItem;
    }
}

public class GenericClassExample : MonoBehaviour
{
    void Start()
    {
        GenericClass<int> myClass = new GenericClass<int>();

        myClass.UpdateItem(5);
    }
}