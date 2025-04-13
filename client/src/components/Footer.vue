<script setup lang="ts">
import { ref } from "vue";
import axios from "axios";
import { aiUrl } from "../utils/envVars";
import { MdLightbulb } from "vue-icons-plus/md";
import Modal from "./Modal.vue";
// import { LMap, LTileLayer } from "@vue-leaflet/vue-leaflet";
// import "leaflet/dist/leaflet.css";

const isOpen = ref(false);
const inspirationInput = ref("");
const inspirationResult = ref("");

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
    <div class="flex">
      <p class="flex-1">
        &copy; Piotr Ptak {{ new Date().getUTCFullYear() }} - Przepisy
      </p>
      <button
        class="bg-purple-500 p-2 rounded-xl text-white cursor-pointer hover:bg-purple-700"
        @click="openModal"
      >
        <MdLightbulb />
      </button>
    </div>
    <!-- <div style="height: 100px; width: 800px">
      <LMap ref="map" :center="[47.41322, -1.219482]">
        <LTileLayer
          :url="`https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png`"
          layer-type="base"
          name="OpenStreetMap"
        ></LTileLayer>
      </LMap>
    </div> -->
  </footer>
</template>
