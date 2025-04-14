<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { IUser, IRole } from "../../Types";
import axios from "axios";
import { AgGridVue } from "ag-grid-vue3";
import AvatarRenderer from "./renderers/AvatarRenderer.vue";
import DeleteUserRenderer from "./renderers/DeleteUserRenderer.vue";

const usersColumns = [
  {
    field: "userImageLink",
    headerName: "Avatar",
    cellRenderer: AvatarRenderer,
  },
  {
    field: "userName",
    headerName: "Użytkownik",
    filter: "agTextColumnFilter",
  },
  {
    field: "id",
    headerName: "Akcje",
    cellRenderer: DeleteUserRenderer,
  },
];

const groupsColumns = [
  {
    field: "name",
    headerName: "Nazwa grupy",
  },
  {
    field: "role",
    headerName: "Typ roli",
  },
];

const usersRows = ref<IUser[]>([]);
const groupsRows = ref<IRole[]>([]);

onMounted(async () => {
  const usersData = await axios.get<{ value: IUser[] }>("/api/users");
  const groupsData = await axios.get<{ value: IRole[] }>("/api/roles");

  if (usersData.status === 200) {
    usersRows.value = usersData.data.value;
  }

  if (groupsData.status === 200) {
    groupsRows.value = groupsData.data.value;
  }
});
</script>
<template>
  <div>
    <div class="bg-white m-3 p-4">
      <p class="text-xl mb-3">Zarządzanie użytkownikami</p>
      <AgGridVue
        :row-data="usersRows"
        :column-defs="usersColumns"
        style="height: 30vh"
      />
    </div>

    <div class="bg-white m-3 p-4">
      <p class="text-xl mb-3">Zarządzanie rolami</p>
      <AgGridVue
        :row-data="groupsRows"
        :column-defs="groupsColumns"
        style="height: 30vh"
      />
    </div>
  </div>
</template>
