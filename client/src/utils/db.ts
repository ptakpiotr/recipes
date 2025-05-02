import Dexie, { type EntityTable } from "dexie";
import type { IGeneralRecipe, IRecipeCreateDtoOffline } from "../../Types";

const db = new Dexie("GeneralRecipes") as Dexie & {
  recipes: EntityTable<IGeneralRecipe | IRecipeCreateDtoOffline>;
};

db.version(1).stores({
  recipes: "++id, title, description",
});

export { db };
