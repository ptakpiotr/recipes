<script setup lang="ts">
import { ref, watchEffect } from "vue";
import {
  MdArrowUpward,
  MdArrowDownward,
  MdAdd,
  MdDelete,
} from "vue-icons-plus/md";
import type { CreateIngredientDto } from "../../Types";

const props = defineProps<{
  ingredients: CreateIngredientDto[];
  editable: boolean;
}>();

const ingredients = ref(props.ingredients);
const ingredient = ref("");

const emits = defineEmits(["manageIngredients"]);

watchEffect(() => {
  emits("manageIngredients", ingredients.value);
});

const swapOrder = (currentIndex: number, newIndex: number) => {
  const temp = ingredients.value[currentIndex];
  ingredients.value[currentIndex] = ingredients.value[newIndex];
  ingredients.value[newIndex] = temp;

  ingredients.value[currentIndex].order = currentIndex + 1;
  ingredients.value[newIndex].order = newIndex + 1;
};

const moveUp = (index: number) => {
  if (index > 0) {
    swapOrder(index, index - 1);
  }
};

const moveDown = (index: number) => {
  if (index < ingredients.value.length - 1) {
    swapOrder(index, index + 1);
  }
};

const addIngredient = () => {
  ingredients.value.push({
    description: ingredient.value,
    order: ingredients.value.length,
  });

  ingredient.value = "";
};

const deleteIngredient = (deleteOrder: number) => {
  ingredients.value = ingredients.value.filter((i) => i.order !== deleteOrder);
};
</script>
<template>
  <div>
    <div v-if="editable">
      <label for="ingredient" class="block font-bold">Dodaj skladnik:</label>
      <div class="flex gap-3 mb-4">
        <input
          type="text"
          id="ingredient"
          class="w-full p-2 bg-gray-50 rounded-sm text-black border-gray-900 border-1"
          v-model="ingredient"
        />
        <button
          type="button"
          class="bg-indigo-500 text-white px-2 py-1 rounded-xl"
          @click="addIngredient"
        >
          <MdAdd />
        </button>
      </div>
    </div>
    <div
      v-for="(ingredient, index) in ingredients"
      :key="index"
      class="flex items-center justify-between bg-gray-50 rounded-sm p-4 mb-2 border-1 shadow"
    >
      <span class="text-lg font-medium">{{ ingredient.description }}</span>
      <div v-if="editable" class="flex space-x-2">
        <button
          type="button"
          @click="moveUp(index)"
          :disabled="index === 0"
          class="bg-green-500 text-white px-2 py-1 rounded-xl hover:bg-green-700 disabled:opacity-50"
        >
          <MdArrowUpward />
        </button>
        <button
          type="button"
          @click="moveDown(index)"
          :disabled="index === ingredients.length - 1"
          class="bg-green-500 text-white px-2 py-1 rounded-xl hover:bg-green-700 disabled:opacity-50"
        >
          <MdArrowDownward />
        </button>
        <button
          type="button"
          @click="deleteIngredient(ingredient.order)"
          class="bg-red-500 text-white px-2 py-1 rounded-xl hover:bg-red-700 disabled:opacity-50"
        >
          <MdDelete />
        </button>
      </div>
    </div>
  </div>
</template>
