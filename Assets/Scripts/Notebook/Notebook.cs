using System;
using System.Collections.Generic;
using SODefinitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Notebook : MonoBehaviour
{
    public static Notebook Instance;

    // public enum Person
    // {
    //     EthanTurner,
    //     MiaTurner,
    //     BillyGilmore,
    //     FinlayGilmore,
    //     FrankieWilder,
    //     LisaWilliams,
    //     JessicaParker,
    //     MarcusGarcia
    // }
    //
    // public enum Place
    // {
    //     BenjaminFranklinHighSchool,
    //     MammaPizzeria,
    //     LaurelHill,
    //     SabrinasCafe,
    //     BenjaminFranklinHighSchoolBackEntry,
    // }
    //
    // public enum Clue
    // {
    //     Friends,
    //     Funeral,
    //     LisasDeath,
    //     MiaDating,
    //     MarcusDeath,
    //     BillysDeath,
    //     Fear
    // }

    private List<PersonSO> persons = new();
    private List<PlaceSO> places = new();
    private List<ClueSO> clues = new();

    [SerializeField] private GameObject resultSection;

    [SerializeField] private Transform peopleContainer;
    [SerializeField] private GameObject peoplePrefab;

    [SerializeField] private Transform placeContainer;
    [SerializeField] private GameObject placePrefab;

    [SerializeField] private Transform clueContainer;
    [SerializeField] private GameObject cluePrefab;
    
    [SerializeField] private TMP_Dropdown whoDropdown;
    [SerializeField] private TMP_Dropdown whereDropdown;
    [SerializeField] private TMP_Dropdown whyDropdown;

    [SerializeField] private PersonSO correctWho;
    [SerializeField] private PlaceSO correctWhere;
    [SerializeField] private ClueSO correctWhy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Unlock("Mia");
    }

    public void Unlock(string itemName)
    {
        var person = Resources.Load($"People/{itemName}");
        if (person != null)
        {
            Unlock((PersonSO) person);
            return;
        }

        var place = Resources.Load($"Places/{itemName}");
        if (place != null)
        {
            Unlock((PlaceSO) place);
            return;
        }

        var clue = Resources.Load($"Clues/{itemName}");
        if (clue != null)
        {
            Unlock((ClueSO) clue);
            return;
        }

        Debug.LogError($"Can't unlock {itemName}. Item not found.");
    }

    public void Unlock(NotebookItemSO item)
    {
        if (IsUnlock(item)) return;
        GameObject itemObject = null;
        switch (item)
        {
            case PersonSO personSo:
                persons.Add(personSo);
                whoDropdown.AddOptions(new List<string>{personSo.Name});
                itemObject = Instantiate(peoplePrefab, peopleContainer);
                break;
            case PlaceSO placeSo:
                places.Add(placeSo);
                whereDropdown.AddOptions(new List<string>{placeSo.Name});
                itemObject = Instantiate(placePrefab, placeContainer);
                break;
            case ClueSO clueSo:
                clues.Add(clueSo);
                whyDropdown.AddOptions(new List<string> {clueSo.Name});
                itemObject = Instantiate(cluePrefab, clueContainer);
                break;
        }

        if (itemObject != null) itemObject.GetComponent<NotebookItem>()?.SetItem(item);
        
        GameManager.Instance.IncreaseGameTime(GameManager.EventCost);

        CheckForResultSectionAvailable();
    }

    public bool IsUnlock(NotebookItemSO item)
    {
        return item switch
        {
            PersonSO personSo => persons.Contains(personSo),
            PlaceSO placeSo => places.Contains(placeSo),
            ClueSO clueSo => clues.Contains(clueSo),
            _ => false
        };
    }

    public void CheckForResultSectionAvailable()
    {
        if (GameManager.Instance.IsAfterMidnight() || (persons.Count >= 3 && places.Count >= 3 && clues.Count >= 3))
        {
            resultSection.SetActive(true);
        }
    }

    public void FinalCheck()
    {
        GameManager.Instance.EndGame(
            correctWhere: whereDropdown.options[whereDropdown.value].text == correctWhere.Name,
            correctWho: whoDropdown.options[whoDropdown.value].text == correctWho.Name,
            correctWhy: whyDropdown.options[whyDropdown.value].text == correctWhy.Name);
    }
}