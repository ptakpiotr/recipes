<script setup lang="ts">
import { MdStar } from "vue-icons-plus/md";
import { ref, computed, defineEmits } from "vue";

const props = defineProps<{
  rating?: number;
  editable: boolean;
}>();

const emit = defineEmits(["update:rating"]);

const currentRating = ref(props.rating);
const hoverValue = ref(0);

const setRating = (star: number) => {
  currentRating.value = star;
  emit("update:rating", star);
};

const hoverRating = (star: number) => {
  hoverValue.value = star;
  currentRating.value = star;
};

const resetRating = () => {
  currentRating.value = props.rating;
};
</script>
<template>
  <div class="flex items-center space-x-1">
    <MdStar
      v-for="star in 5"
      :key="star"
      :class="{
        'text-yellow-600': star <= currentRating,
        'text-gray-300': star > currentRating,
        'hover:text-yellow-600': star == hoverRating && editable,
        'cursor-pointer': editable,
      }"
      size="24"
      @click="props.editable ? setRating(star) : null"
      @mouseover="props.editable ? hoverRating(star) : null"
      @mouseleave="props.editable ? resetRating() : null"
    />
  </div>
</template>
