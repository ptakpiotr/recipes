<script setup lang="ts">
import { ref, onMounted } from "vue";
import axios from "axios";
import type { IGeneralRecipe } from "../../Types";
import GeneralRecipe from "./GeneralRecipe.vue";

const recipes = ref<IGeneralRecipe[]>([]);

onMounted(async () => {
  const recipesData = await axios.get<{ value: IGeneralRecipe[] }>(
    "/api/recipes"
  );

  if (recipesData.status === 200) {
    recipes.value = recipesData.data.value;
  }
});
</script>
<template>
  <div>
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
