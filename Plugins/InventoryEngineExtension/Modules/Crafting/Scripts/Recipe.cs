using System.Collections.Generic;

namespace MoreMountains.InventoryEngine
{
    [System.Serializable]
    public class Recipe
    {
        public string name;
        public InventoryItem outputItem;
        public int outputQuantity = 1;
        public int craftingTimeInSecounds = 1;
        public List<Ingredient> ingredients = new List<Ingredient>();
    }
}

