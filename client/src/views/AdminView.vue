<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { IUser } from "../../Types";
import axios from "axios";
import RevoGrid from "@revolist/vue3-datagrid";

const columns = [
  {
    prop: "userImageLink",
    name: "Avatar",
  },
  {
    prop: "userName",
    name: "Nazwa użytkownika",
  },
];

const rows = ref<IUser[]>([]);

onMounted(async () => {
  const data = await axios.get<{ value: IUser[] }>("/api/users");

  if (data.status === 200) {
    rows.value = data.data.value;
  }
});
</script>
<template>
  <div>
    <RevoGrid :columns="columns" :rows="rows" />
  </div>
</template>
