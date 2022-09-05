using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CategoryController : MonoBehaviour
{

    //fields
    //root category
    public Category Root;

    //active category
    public Category Active;

    //filter menu
    public FilterMenu filterMenu;

    // Start is called before the first frame update
    void Start()
    {
        //create category tree
        CreateCategories();
        //load from json
        Root = LoadFromJSON();

        //set active category to root
        Active = Root;

        filterMenu.InstantiateFilters();
    }

    void CreateCategories()
    {
        //create category alcohol
        Category alcohol = new Category("Alcohol", "alcohol");

        //create few subcategories
        Category wine = new Category("Wine", "wine");
        Category beer = new Category("Beer", "beer");
        Category liquor = new Category("Liquor", "liquor");

        //add subcategories to alcohol
        alcohol.AddSubcategory(wine);
        alcohol.AddSubcategory(beer);
        alcohol.AddSubcategory(liquor);

        //create category food
        Category food = new Category("Food", "food");

        //create few subcategories
        Category drinks = new Category("Drinks", "drinks");
        Category snacks = new Category("Snacks", "snacks");
        Category desserts = new Category("Desserts", "desserts");

        //add subcategories to food
        food.AddSubcategory(drinks);
        food.AddSubcategory(snacks);
        food.AddSubcategory(desserts);

        //create root category
        Category root = new Category("Root", "root");

        //add categories to root
        root.AddSubcategory(alcohol);
        root.AddSubcategory(food);

        //convert to json using newtonsoft.json
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(root);

        json = JToken.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);

        //save to file
        System.IO.File.WriteAllText("Assets/Resources/categories.json", json);
    }

    //load from json file
    public Category LoadFromJSON()
    {
        //load from json file
        string json = System.IO.File.ReadAllText("Assets/Resources/categories.json");

        //convert to object
        Category root = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(json);

        return root;
    }

}
