<script setup lang="ts">
import { ref } from "vue";
import { POSITION, useToast } from "vue-toastification";
import { useRouter } from "vue-router";
import { serverUrl } from "../utils/envVars";
import axios from "axios";
import StarsRating from "./StarsRating.vue";
import type { IRatingReadDto, UserBasicReadDto } from "../../Types";
import { MdRemove } from "vue-icons-plus/md";
import { onMounted } from "vue";

const props = defineProps<{
  user?: UserBasicReadDto;
  rating: IRatingReadDto;
  onlyOneComment: boolean;
}>();

const isEditable = ref(false);
const toast = useToast();
const router = useRouter();

//suboptimal but for now is good enough for this data volume
onMounted(async () => {
  const ownershipUrl = `/api/ratings/ownership/${props.rating.id}`;

  try {
    await axios.get(ownershipUrl);
    isEditable.value = true;
  } catch (err) {
    isEditable.value = false;
  }
});

const updateRating = async (rating: number) => {
  try {
    const ratingsUrl = `${serverUrl}/api/ratings`;

    await axios.put(ratingsUrl, {
      ratingId: props.rating.id,
      rating,
    });
  } catch (err) {
    toast.error("Blad przy aktualizacji opinii", {
      position: POSITION.BOTTOM_RIGHT,
    });
  }
};

const removeRating = async (ratingId: string) => {
  try {
    const ratingsUrl = `${serverUrl}/api/ratings/${ratingId}`;

    await axios.delete(ratingsUrl);
    toast.success("Usunięto opinie", {
      position: POSITION.BOTTOM_RIGHT,
      onClose: () => {
        router.push("/");
      },
    });
  } catch (err) {
    toast.error("Blad przy usuwaniu opinii", {
      position: POSITION.BOTTOM_RIGHT,
    });
  }
};
</script>
<template>
  <div>
    <div class="recipe-user">
      <p>Użytkownik: {{ props.user?.userName }}</p>
      <img
        class="recipe-image"
        :src="props.user?.userImageLink"
        alt="Avatar użytkownika"
      />
      <button
        v-if="isEditable"
        @click="() => removeRating(props.rating.id)"
        class="bg-red-500 p-1 rounded-xl text-white cursor-pointer hover:bg-red-700"
      >
        <MdRemove />
      </button>
    </div>
    <StarsRating
      :rating="props.rating.rating"
      :editable="onlyOneComment && isEditable"
      @update:rating="
        async (rating) => {
          await updateRating(rating);
        }
      "
    />
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
