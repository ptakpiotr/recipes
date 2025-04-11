<script setup lang="ts">
import { ref } from "vue";
import { IRecipeEditDto } from "../../Types";
import Modal from "./Modal.vue";

const props = defineProps<{
  recipe: IRecipeEditDto;
}>();

const editedRecipe = ref<IRecipeEditDto>(props.recipe);
</script>
<template>
  <Modal :isOpen="true">
    <div>
      {{ JSON.stringify(editedRecipe) }}
    </div>
  </Modal>
</template>
<style scoped></style>
