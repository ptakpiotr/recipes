<script setup lang="ts">
import { ref } from "vue";
import { IRatingCreateDto } from "../../Types";
import StarsRating from "./StarsRating.vue";
import Modal from "./Modal.vue";
import axios from "axios";

const props = defineProps<Pick<IRatingCreateDto, "recipeId">>();
const isOpen = ref<boolean>(true);

const rating = ref<IRatingCreateDto>({
  rating: 0,
  recipeId: props.recipeId,
});

const updateRating = (r: number) => {
  rating.value.rating = r;
};

const closeModal = () => {
  isOpen.value = false;
};

const submitRating = async () => {
  const res = await axios.post("/api/ratings", {
    rating: rating.value.rating,
    recipeId: rating.value.recipeId,
  });

  console.log(res);
};
</script>
<template>
  <Modal
    :isOpen="isOpen"
    title="Dodaj opinię"
    @close="closeModal"
    @submit="submitRating"
  >
    <StarsRating
      @update:rating="updateRating"
      :rating="rating.rating"
      :editable="true"
    />
  </Modal>
</template>
<style scoped></style>
