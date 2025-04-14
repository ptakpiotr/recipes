<script setup lang="ts">
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import axios from "axios";
import DOMPurify from "dompurify";
import VueMarkdown from "vue-markdown-render";
import { MdCreate, MdPlusOne, MdRemove } from "vue-icons-plus/md";
import StarsRating from "../components/StarsRating.vue";
import RecipeSentiment from "../components/RecipeSentiment.vue";
import AddRatingModal from "../components/AddRatingModal.vue";
import EditRecipeModal from "../components/EditRecipeModal.vue";
import RemoveRecipeModal from "../components/RemoveRecipeModal.vue";
import type { IRecipe, RecipeType } from "../../Types";
import { formatDate } from "../utils/formatters";
import { storeToRefs } from "pinia";
import { useUsersStore } from "../store/store";
import { POSITION, useToast } from "vue-toastification";
import { serverUrl } from "../utils/envVars";

const recipe = ref<IRecipe | null>(null);
const showRatingModal = ref<boolean>(false);
const editModal = ref<boolean>(false);
const removeModal = ref<boolean>(false);
const route = useRoute();
const router = useRouter();
const toast = useToast();
const store = useUsersStore();
const { users } = storeToRefs(store);

onMounted(async () => {
  const splitRoute = route.path.split("/");

  if (splitRoute.length === 3) {
    const recipesUrl = `/api/recipes/${splitRoute[2]}`;

    const recipeRes = await axios.get<{ value: IRecipe }>(recipesUrl);

    if (recipeRes.status === 200) {
      recipe.value = recipeRes.data.value;
    }
  }
});

const openRatingModal = () => {
  showRatingModal.value = !showRatingModal.value;
  editModal.value = false;
  removeModal.value = false;
};

const openEditModal = () => {
  editModal.value = !editModal.value;
  showRatingModal.value = false;
  removeModal.value = false;
};

const openRemoveModal = () => {
  removeModal.value = !removeModal.value;
  editModal.value = false;
  showRatingModal.value = false;
};

const removeRating = async (ratingId: string) => {
  try {
    const ratingsUrl = `${serverUrl}/api/ratings/${ratingId}`;

    await axios.delete(ratingsUrl);
    toast.success("Usunięto opinie", {
      position: POSITION.BOTTOM_RIGHT,
    });
  } catch (err) {
    toast.error("Blad przy usuwaniu opinii", {
      position: POSITION.BOTTOM_RIGHT,
    });
  }
};

const openFilteredRecipesView = (type: RecipeType) => {
  const recipeType = type.toString();

  router.push(`/?filterType=${recipeType}`);
};
</script>
<template>
  <div>
    <Teleport v-if="removeModal && recipe" to="body">
      <RemoveRecipeModal :recipeId="recipe.id" />
    </Teleport>
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
          <div class="flex gap-2">
            <RecipeSentiment :description="recipe.description" />
            <button
              class="bg-fuchsia-500 p-2 rounded-xl text-white cursor-pointer hover:bg-fuchsia-700"
              @click="openEditModal"
            >
              <MdCreate />
            </button>
            <button
              class="bg-red-500 p-2 rounded-xl text-white cursor-pointer hover:bg-red-700"
              @click="openRemoveModal"
            >
              <MdRemove />
            </button>
          </div>
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
            @click="() => openFilteredRecipesView(type)"
            class="text-sm bg-fuchsia-600 text-white rounded-full px-3 py-1 hover:bg-fuchsia-700 cursor-pointer"
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
        <div class="p-2 m-2" v-for="r in recipe.ratings" :key="r.id">
          <div class="recipe-user">
            <p>
              Użytkownik: {{ users?.find((u) => u.id === r.userId)?.userName }}
            </p>
            <img
              class="recipe-image"
              :src="users?.find((u) => u.id === r.userId)?.userImageLink"
              alt="Avatar użytkownika"
            />
            <button
              @click="() => removeRating(r.id)"
              class="bg-red-500 p-1 rounded-xl text-white cursor-pointer hover:bg-red-700"
            >
              <MdRemove />
            </button>
          </div>
          <StarsRating :rating="r.rating" :editable="false" />
        </div>
      </div>
    </div>
    <div v-else></div>
  </div>
</template>
<style scoped>
.recipe-user {
  display: flex;
  column-gap: 1rem;
}
.recipe-image {
  width: 2rem;
  height: 2rem;
}
</style>
