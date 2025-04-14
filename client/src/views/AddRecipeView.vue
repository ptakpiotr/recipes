<script setup lang="ts">
import IngredientsManage from "../components/IngredientsManage.vue";
import { recipeTypes } from "../utils/constants";
import { serverUrl } from "../utils/envVars";

const ins = [
  { description: "23", order: 1, recipeId: "45" },
  { description: "53", order: 2, recipeId: "123" },
];
</script>
<template>
  <form
    :action="`${serverUrl}/api/recipes`"
    method="POST"
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
    <IngredientsManage :ingredients="ins" />
    <button
      type="submit"
      class="bg-green-500 text-white font-bold p-2 border-gray-900 border-2 cursor-pointer rounded-md hover:bg-green-700"
    >
      Dodaj
    </button>
  </form>
</template>
<style scoped></style>
