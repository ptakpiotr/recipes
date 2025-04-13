import { z } from "zod";
import { LevelAnalysis } from "./Types.js";

export const sentimentSchema = z.object({
  sentiment: z.nativeEnum(LevelAnalysis),
});

export const recipeSchema = z.object({
  RecipeId: z.string(),
});
