<script setup lang="ts">
import { onMounted, ref } from "vue";
import axios from "axios";
import { aiUrl } from "../utils/envVars";
import { MdLightbulb, MdDownload } from "vue-icons-plus/md";
import Modal from "./Modal.vue";

import "leaflet/dist/leaflet.css";

const isOpen = ref(false);
const inspirationInput = ref("");
const inspirationResult = ref("");
const prompt = ref(null);

onMounted(() => {
  window.addEventListener("beforeinstallprompt", (evt) => {
    evt.preventDefault();

    prompt.value = evt;
  });
});

const openModal = () => {
  isOpen.value = true;
};

const hideModal = () => {
  isOpen.value = false;
};

const findInspiration = async () => {
  inspirationInput.value = "";

  const resp = await axios.post(`${aiUrl}/rag`, {
    message: inspirationInput.value,
  });

  inspirationResult.value = resp.data;
};

const downloadTheApp = () => {
  console.log(prompt.value);
  if (prompt.value) {
    prompt.value.prompt();

    prompt.value = null;
  }
};
</script>
<template>
  <footer class="bg-green-300 p-4">
    <Modal
      :isOpen="isOpen"
      :hide-cancel="true"
      @close="hideModal"
      @submit="hideModal"
      title="Zainspiruj się"
    >
      <div class="flex">
        <input
          class="flex-1 border-2 p-2 rounded-xl mr-12"
          type="text"
          v-model="inspirationInput"
        />
        <button
          class="bg-purple-500 p-2 rounded-xl text-white cursor-pointer hover:bg-purple-700"
          @click="findInspiration"
        >
          Inspiruj!
        </button>
      </div>
      <p>
        {{ inspirationResult }}
      </p>
    </Modal>
    <div class="flex gap-3">
      <p class="flex-1">
        &copy; Piotr Ptak {{ new Date().getUTCFullYear() }} - Przepisy
      </p>
      <button
        class="download-icon bg-indigo-500 p-2 rounded-xl text-white cursor-pointer hover:bg-indigo-700"
        @click="downloadTheApp"
      >
        <MdDownload />
      </button>
      <button
        class="bg-purple-500 p-2 rounded-xl text-white cursor-pointer hover:bg-purple-700"
        @click="openModal"
      >
        <MdLightbulb />
      </button>
    </div>
  </footer>
</template>
<style scoped>
.download-icon {
  display: none;
}

@media (display-mode: browser) {
  .download-icon {
    display: block;
  }
}
</style>
