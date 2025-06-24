using System.Collections.Generic;
using UnityEngine;

public class StudyInheritance : MonoBehaviour
{
    public List<Person> persons = new List<Person>();

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Student student = new Student();
            persons.Add(student);
            
            Soldier soldier = new Soldier();
            persons.Add(soldier);
        }
    }

    public void AllMove()
    {
        foreach (var person in persons)
            person.Walk();
    }
}