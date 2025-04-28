import Dexie, { type EntityTable } from "dexie";
import type { IGeneralRecipe } from "../../Types";

const db = new Dexie("GeneralRecipes") as Dexie & {
  recipes: EntityTable<IGeneralRecipe, "id">;
};

db.version(1).stores({
  recipes: "++id, title, description",
});

export { db };
