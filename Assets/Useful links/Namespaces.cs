using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections;

public class PlayerMovement1 : MonoBehaviour
{
    //..
}

public class PlayerMovement2 : UnityEngine.MonoBehaviour
{
    //..
}

public class PlayerMovement3 : MonoBehaviour
{
    void Start()
    {
        float speed = Random.value;
        //..
    }
}

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //..
    }
}

namespace EditorTools.MapCreation
{
    public class Drawing
    {
        //..
    }
}

public class AIManager
{
    //..
}

public class Inventory : MonoBehaviour
{
    public System.Collections.Generic.List<Item> items;
}

public class Item
{
    //..
}