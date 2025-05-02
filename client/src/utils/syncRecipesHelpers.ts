import axios from "axios";
import type { IGeneralRecipe, IRecipeCreateDtoOffline } from "../../Types";
import { db } from "./db";

function isFormData(
  r: IGeneralRecipe | IRecipeCreateDtoOffline
): r is IRecipeCreateDtoOffline {
  return "isNew" in r;
}

export const syncRecipesData = async (data: IGeneralRecipe[]) => {
  await clearRecipes();
  await db.recipes.bulkAdd(data);
};

export const addSingleRecipe = async (recipe: IRecipeCreateDtoOffline) => {
  await db.recipes.add(recipe);
};

export const getRecipesData = async () => {
  return await db.recipes.toArray();
};

const getOfflineRecipesData = async () => {
  return await db.recipes.filter(isFormData).toArray();
};

const clearRecipes = async () => {
  await db.recipes.clear();
};

export const syncRecipesToBackend = async () => {
  const recipesToSync =
    (await getOfflineRecipesData()) as IRecipeCreateDtoOffline[];
  axios
    .post(
      "/api/recipes/mass-sync",
      {
        recipes: recipesToSync,
      },
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
    .then((response) => {
      console.log(":)", response);
    })
    .catch((err) => {
      console.error(err);
    });
};
