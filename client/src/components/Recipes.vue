<script setup lang="ts">
import { ref, onMounted } from "vue";
import axios from "axios";
import type { IGeneralRecipe } from "../../Types";
import GeneralRecipe from "./GeneralRecipe.vue";
import { useRoute } from "vue-router";
import { syncRecipesData, getRecipesData } from "../utils/syncRecipesHelpers";

const recipes = ref<IGeneralRecipe[]>([]);
const route = useRoute();
const splitQuery = route.query;

onMounted(async () => {
  const recipeFilterQuery = splitQuery["filterType"];

  const recipesUrl = `/api/recipes${
    !!recipeFilterQuery ? `?filterType=${recipeFilterQuery}` : ""
  }`;

  try {
    const recipesData = await axios.get<{ value: IGeneralRecipe[] }>(
      recipesUrl
    );

    if (recipesData.status === 200) {
      recipes.value = recipesData.data.value;
      await syncRecipesData(recipesData.data.value);
    }
  } catch (err) {
    recipes.value = await getRecipesData();
  }
});
</script>
<template>
  <div class="recipes">
    <GeneralRecipe
      v-for="r in recipes"
      :id="r.id"
      :title="r.title"
      :description="r.description"
      :image-url="r.imageUrl"
      :updated-at="r.updatedAt"
      :key="r.id"
    />
  </div>
</template>
<style scoped>
.recipes {
  display: flex;
  column-gap: 2rem;
  padding: 1rem;
  flex-wrap: wrap;
}
</style>
