<script setup lang="ts">
import { ref } from "vue";
import Modal from "./Modal.vue";
import { serverUrl } from "../utils/envVars";
import type { IRecipeEditDto } from "../../Types";
import { recipeTypes } from "../utils/constants";

const props = defineProps<{
  recipe: IRecipeEditDto;
}>();

const isOpen = ref(true);
const editedRecipe = ref<IRecipeEditDto>(props.recipe);

const closeModal = ()=>{
  isOpen.value = false;
};

</script>
<template>
  <Modal :isOpen="isOpen" @close="closeModal" title="Edytuj przepis">
    <form
      :action="`${serverUrl}/api/recipes`"
      class="bg-green p-6 m-4 rounded-lg shadow-md text-black border-gray-900 border-2 space-y-4"
    >
      <div>
        <label for="title" class="block font-bold">Title:</label>
        <input
          type="text"
          id="title"
          name="title"
          :value="editedRecipe.title"
          class="w-full p-2 rounded-md text-black border-gray-900 border-2"
          required
        />
      </div>
      <div>
        <label for="description" class="block font-bold">Description:</label>
        <textarea
          id="description"
          name="description"
          :value="editedRecipe.description"
          class="w-full p-2 rounded-md text-black border-gray-900 border-2"
          required
        ></textarea>
      </div>
      <div>
        <label for="types" class="block font-bold">Recipe Types:</label>
        <select
          id="types"
          name="types"
          :value="editedRecipe.types"
          multiple
          class="w-full p-2 rounded-md text-black border-gray-900 border-2"
        >
          <option v-for="type in recipeTypes" :key="type" :value="type">
            {{ type }}
          </option>
        </select>
        <p class="text-sm text-gray-800 mt-2">
          Hold Ctrl (or Command on Mac) to select multiple types.
        </p>
      </div>
      <!-- <IngredientsManage :ingredients="ins" /> -->
      <button
        type="submit"
        class="bg-green-500 text-green font-bold p-2 border-gray-900 border-2 rounded-md hover:bg-green-200"
      >
        Submit
      </button>
    </form>
  </Modal>
</template>
<style scoped></style>
