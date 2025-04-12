<script setup lang="ts">
import { defineEmits } from "vue";

const props = defineProps<{
  isOpen: boolean;
  title: string;
}>();

const emit = defineEmits(["close", "submit"]);

const closeModal = () => {
  emit("close");
};

const submit = () => {
  emit("submit");
};
</script>
<template>
  <div
    v-if="props.isOpen"
    class="fixed inset-0 flex items-center justify-center z-50"
  >
    <div class="bg-white rounded-lg shadow-lg p-6 w-full max-w-md">
      <div class="flex justify-between items-center mb-4">
        <h2 class="text-lg font-bold text-gray-800">{{ props.title }}</h2>
        <button @click="closeModal" class="text-gray-500 hover:text-gray-700">
          &times;
        </button>
      </div>

      <div class="text-gray-800">
        <slot></slot>
      </div>

      <div class="mt-6 flex justify-end space-x-2">
        <button
          class="px-4 py-2 bg-orange-300 text-white rounded-md hover:bg-orange-500"
          @click="closeModal"
        >
          Anuluj
        </button>
        <button
          class="px-4 py-2 bg-cyan-600 text-white rounded-md hover:bg-cyan-700"
          @click="submit"
        >
          Zatwierdź
        </button>
      </div>
    </div>
  </div>
</template>
