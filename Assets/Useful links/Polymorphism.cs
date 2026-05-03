using UnityEngine;
using System;
using System.Collections;

public class PetShop : MonoBehaviour
{
    [SerializeReference]
    public Mammal mammal = new Cat();
}

public class Animal { }

[Serializable]
public class Mammal : Animal
{
    public void GrowFur() { }
}

public class Cat : Mammal
{
    public void Meow() { }
}

public class Dog : Mammal
{
    public void Woof() { }
}

public class RescueShelter
{
    public Mammal[] mammals;

    public RescueShelter()
    {
        mammals = new Mammal[2];
        mammals[0] = new Cat();
        mammals[1] = new Dog();

        // Compile error.
        //mammals[0].Meow();

        Cat cat = mammals[0] as Cat;
        cat.Meow();

        Dog dog = (Dog)mammals[1];
        dog.Woof();

        if (mammals[0] is Cat)
        {
            Cat safeCat = mammals[0] as Cat;
            safeCat.Meow();
        }

        if (mammals[1] is Dog)
        {
            Dog safeDog = (Dog)mammals[1];
            safeDog.Woof();
        }
    }
}