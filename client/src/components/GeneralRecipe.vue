<script setup lang="ts">
import { ref } from "vue";
import type { IGeneralRecipe } from "../../Types";

const isHover = ref(false);

const props = defineProps<IGeneralRecipe>();

const handleHover = (hover: boolean) => {
  isHover.value = hover;
};
</script>
<template>
  <div
    class="bg-green-100 px-3 my-2"
    @mouseenter="() => handleHover(true)"
    @mouseleave="() => handleHover(false)"
  >
    <div class="bg-green-200 p-1">{{ props.title }}</div>
    <div v-if="!isHover">
      <div><img :src="props.imageUrl" /></div>
      <div class="bg-green-200 p-1">{{ props.updatedAt }}</div>
    </div>
    <div v-if="isHover">
      <p>
        {{ props.description }}
      </p>
    </div>
  </div>
</template>
