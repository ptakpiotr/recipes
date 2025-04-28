import type { IGeneralRecipe } from "../../Types";
import { db } from "./db";

export const syncRecipesData = async (data: IGeneralRecipe[]) => {
  await db.recipes.clear();
  await db.recipes.bulkAdd(data);
};

export const getRecipesData = async ()=>{
  return await db.recipes.toArray();
}