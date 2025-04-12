<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import axios from "axios";
import DOMPurify from "dompurify";
import VueMarkdown from "vue-markdown-render";
import { MdCreate, MdPlusOne } from "vue-icons-plus/md";
import StarsRating from "../components/StarsRating.vue";
import AddRatingModal from "../components/AddRatingModal.vue";
import EditRecipeModal from "../components/EditRecipeModal.vue";
import type { IRecipe } from "../../Types";
import { formatDate } from "../utils/formatters";

const recipe = ref<IRecipe | null>(null);
const showRatingModal = ref<boolean>(false);
const editModal = ref<boolean>(false);
const route = useRoute();

onMounted(async () => {
  const splitRoute = route.path.split("/");

  if (splitRoute.length === 3) {
    const recipeRes = await axios.get<{ value: IRecipe }>(
      `/api/recipes/${splitRoute[2]}`
    );

    if (recipeRes.status === 200) {
      recipe.value = recipeRes.data.value;
    }
  }
});

const openRatingModal = () => {
  showRatingModal.value = !showRatingModal.value;
  editModal.value = false;
};

const openEditModal = () => {
  editModal.value = !editModal.value;
  showRatingModal.value = false;
};
</script>
<template>
  <div>
    <Teleport v-if="showRatingModal && recipe" to="body">
      <AddRatingModal :recipeId="recipe.id" />
    </Teleport>
    <Teleport v-if="editModal && recipe" to="body">
      <EditRecipeModal :recipe="recipe" />
    </Teleport>
    <div v-if="recipe">
      <div class="m-4 bg-white rounded-lg shadow-lg p-6">
        <div class="flex mb-3">
          <h1 class="text-xl font-semibold mb-2 flex-1">Przepis</h1>
          <button
            class="bg-fuchsia-500 p-2 rounded-xl text-white cursor-pointer hover:bg-fuchsia-700"
            @click="openEditModal"
          >
            <MdCreate />
          </button>
        </div>
        <div v-if="recipe.imageUrl" class="mb-4">
          <img
            :src="recipe.imageUrl"
            alt="Recipe image"
            class="rounded-md w-full object-cover h-48"
          />
        </div>
        <h2 class="text-xl font-bold text-gray-800 mb-2">{{ recipe.title }}</h2>
        <p class="text-gray-600 mb-4">
          <VueMarkdown :source="DOMPurify.sanitize(recipe.description)" />
        </p>
        <div class="flex flex-wrap gap-2 mb-4">
          <span
            v-for="type in recipe.types"
            :key="type"
            class="text-sm bg-fuchsia-600 text-white rounded-full px-3 py-1 hover:bg-fuchsia-700"
          >
            {{ type }}
          </span>
        </div>
        <div class="text-gray-500 text-sm">
          <p>Utworzono o: {{ formatDate(recipe.createdAt) }}</p>
          <p>Zaktualizowano : {{ formatDate(recipe.updatedAt?.toString()) }}</p>
        </div>
      </div>
      <div class="m-4 bg-white rounded-lg shadow-lg p-6">
        <div class="flex mb-3">
          <h1 class="text-xl font-semibold mb-2 flex-1">Opinie</h1>
          <button
            class="bg-cyan-500 p-2 rounded-xl text-white cursor-pointer hover:bg-cyan-700"
            @click="openRatingModal"
          >
            <MdPlusOne />
          </button>
        </div>
        <div v-for="r in recipe.ratings" :key="r.id">
          <p>{{ r.userId }}</p>
          <StarsRating :rating="r.rating" :editable="false" />
        </div>
      </div>
    </div>
    <div v-else></div>
  </div>
</template>
