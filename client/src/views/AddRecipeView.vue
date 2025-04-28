<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import IngredientsManage from "../components/IngredientsManage.vue";
import { recipeTypes } from "../utils/constants";
import { serverUrl } from "../utils/envVars";
import axios from "axios";
import { POSITION, useToast } from "vue-toastification";
import type { CreateIngredientDto } from "../../Types";

const ingredients = ref<CreateIngredientDto[]>([]);
const router = useRouter();
const toast = useToast();

const manageIngredients = (newIngredients: CreateIngredientDto[]) => {
  ingredients.value = newIngredients;
};

const handleSubmit = () => {
  const formElement = document.querySelector("form") as HTMLFormElement;
  const formData = new FormData(formElement);

  ingredients.value.forEach((item, index) => {
    formData.append(`ingredients[${index}][description]`, item.description);
    formData.append(`ingredients[${index}][order]`, item.order.toString());
  });

  axios
    .post(formElement.action, formData)
    .then((response) => {
      router.push(`/recipe/${response.data.value.id}`);
    })
    .catch((_) => {
      toast.error("Nie udalo się dodać nowego przepisu", {
        draggable: true,
        position: POSITION.TOP_RIGHT,
      });
    });
};
</script>
<template>
  <form
    :action="`${serverUrl}/api/recipes`"
    method="POST"
    @submit.prevent="handleSubmit"
    enctype="multipart/form-data"
    class="bg-white p-6 m-4 rounded-lg shadow-md text-black border-gray-900 border-1 space-y-4"
  >
    <div>
      <label for="title" class="block font-bold">Tytul: </label>
      <input
        type="text"
        id="title"
        name="title"
        class="w-full p-2 bg-gray-50 rounded-sm text-black border-gray-900 border-1"
        required
      />
    </div>
    <div>
      <label for="description" class="block font-bold">Opis:</label>
      <textarea
        id="description"
        name="description"
        class="w-full p-2 bg-gray-50 rounded-sm text-black border-gray-900 border-1"
        required
      ></textarea>
    </div>
    <div>
      <label for="image" class="block font-bold">Obraz:</label>
      <input
        type="file"
        id="image"
        name="image"
        class="w-full p-2 bg-gray-50 rounded-sm text-black border-gray-900 border-1"
        required
      />
    </div>
    <div>
      <label for="types" class="block font-bold">Rodzaj przepisu:</label>
      <select
        id="types"
        name="types"
        multiple
        class="w-full p-2 text-black bg-gray-50 rounded-sm border-1"
      >
        <option v-for="type in recipeTypes" :key="type" :value="type">
          {{ type }}
        </option>
      </select>
      <p class="text-sm text-gray-800 mt-2">
        Trzymaj Ctrl (albo Command na Macu) by wybrać wiele typów.
      </p>
    </div>
    <IngredientsManage
      :ingredients="ingredients"
      :editable="true"
      @manage-ingredients="manageIngredients"
    />
    <button
      type="submit"
      class="bg-green-500 text-white font-bold p-2 border-gray-900 border-2 cursor-pointer rounded-md hover:bg-green-700"
    >
      Dodaj
    </button>
  </form>
</template>
<style scoped></style>
