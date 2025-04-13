<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import DOMPurify from "dompurify";
import VueMarkdown from "vue-markdown-render";
import { MdOpenInNew } from "vue-icons-plus/md";
import { formatDate } from "../utils/formatters";
import type { IGeneralRecipe } from "../../Types";
const props = defineProps<IGeneralRecipe>();

const isHover = ref(false);
const router = useRouter();

const handleHover = (hover: boolean) => {
  isHover.value = hover;
};

const recipeDetails = (recipeId: string) => {
  router.push(`/recipe/${recipeId}`);
};
</script>
<template>
  <div
    class="bg-green-100 px-3 my-2 rounded-2xl w-[300px]"
    @mouseenter="() => handleHover(true)"
    @mouseleave="() => handleHover(false)"
  >
    <div class="bg-green p-6 text-green-950">
      <h1 class="text-2xl font-bold">{{ props.title }}</h1>
      <div class="general-recipe-content">
        <p v-if="isHover" class="text-lg my-4">
          <VueMarkdown
            :source="DOMPurify.sanitize(props.description.substring(0, 150))"
          />...
        </p>
        <div v-else class="my-4">
          <img
            :src="props.imageUrl"
            :alt="props.title"
            class="rounded-md max-w-full h-auto"
          />
        </div>
      </div>
      <p class="text-sm">{{ formatDate(props.updatedAt?.toString()) }}</p>
      <div>
        <button
          class="bg-cyan-500 p-2 mt-2 rounded-xl text-white cursor-pointer hover:bg-cyan-700"
          @click="
            () => {
              recipeDetails(props.id);
            }
          "
        >
          <MdOpenInNew />
        </button>
      </div>
    </div>
  </div>
</template>
<style scoped>
.general-recipe-content {
  max-width: 250px;
  height: 200px;
}
</style>
