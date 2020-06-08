using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.InventoryEngine
{
    [System.Serializable]
    public class CraftingCategory
    {
        public string name;
        public Button openingButton;
        public Inventory inventory;

        [Header("Crafting Recipes")]
        public List<Recipe> recipes = new List<Recipe>();
    }
}

