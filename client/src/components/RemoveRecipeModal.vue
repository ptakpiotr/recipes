<script setup lang="ts">
import axios from "axios";
import Modal from "./Modal.vue";
import { ref } from "vue";
import { useRouter } from "vue-router";
import { POSITION, useToast } from "vue-toastification";
import { serverUrl } from "../utils/envVars";

const props = defineProps<{
  recipeId: string;
}>();

const isOpen = ref<boolean>(true);
const router = useRouter();
const toast = useToast();

const closeRemoval = () => {
  isOpen.value = false;
};

const acceptRemoval = async () => {
  try {
    await axios.delete(`${serverUrl}/api/recipes/${props.recipeId}`);

    isOpen.value = false;

    setTimeout(() => {
      router.go(0);
    }, 200);
  } catch (err) {
    isOpen.value = false;

    toast.error("Usunięcie przepisu nie powiodlo się", {
      draggable: false,
      position: POSITION.TOP_CENTER,
    });
  }
};
</script>
<template>
  <Modal
    :isOpen="isOpen"
    title="Usuń"
    @close="closeRemoval"
    @submit="acceptRemoval"
  >
    Czy jesteś pewny, że chcesz usunąć przepis?
  </Modal>
</template>
