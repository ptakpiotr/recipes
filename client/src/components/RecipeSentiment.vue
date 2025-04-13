<script setup lang="ts">
import axios from "axios";
import { MdStar, MdDownloading } from "vue-icons-plus/md";
import { aiUrl } from "../utils/envVars";
import { reactive } from "vue";

const data = reactive({
  loading: false,
  difficulty: -1,
});

const props = defineProps<{
  description: string;
}>();

const rateDifficulty = async () => {
  data.loading = true;
  const resp = await axios.post(`${aiUrl}/sentiment`, {
    message: props.description,
  });

  if (resp.status === 200) {
    data.loading = false;
    data.difficulty = resp.data.sentiment;
  }
};
</script>
<template>
  <div class="flex gap-2">
    <div
      v-if="data.difficulty >= 0"
      class="bg-indigo-500 text-white p-2 rounded-xl"
    >
      {{ data.difficulty }}
    </div>
    <div v-if="data.loading && data.difficulty < 0" class="bg-red-500 p-2 rounded-xl text-white cursor-progress">
      <MdDownloading />
    </div>
    <button
      class="bg-amber-500 p-2 rounded-xl text-white cursor-pointer hover:bg-amber-700 disabled:bg-amber-50"
      v-else-if="data.difficulty < 0"
      @click="rateDifficulty"
    >
      <MdStar />
    </button>
  </div>
</template>
