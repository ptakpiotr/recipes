<script setup lang="ts">
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import { RouterView } from "vue-router";
import { onMounted } from "vue";
import { useUsersStore } from "./store/store";
import { POSITION, useToast } from "vue-toastification";
import axios from "axios";
import { serverUrl } from "./utils/envVars";
import type { UserBasicReadDto } from "../Types";

const usersStore = useUsersStore();
const toast = useToast();

onMounted(async () => {
  try {
    const users = await axios.get<{
      value: UserBasicReadDto[];
    }>(`${serverUrl}/api/users/basic`);

    if (users.status === 200) {
      usersStore.setUsers(users.data.value);
    }
  } catch (err) {
    toast.error("Wystąpil blad w trakcie pobierania informacji", {
      draggable: true,
      position: POSITION.TOP_RIGHT,
    });
  }

  try {
    const isAdmin = await axios.get<boolean>(`${serverUrl}/api/roles/admin`);

    if (isAdmin.status === 200) {
      usersStore.setIsAdmin(isAdmin.data);
    }
  } catch (err) {
    toast.error("Wystąpil blad w trakcie pobierania informacji", {
      draggable: true,
      position: POSITION.TOP_RIGHT,
    });
  }
});
</script>

<template>
  <main>
    <Header />
    <div class="content bg-green-50">
      <RouterView />
    </div>
    <Footer />
  </main>
</template>
<style scoped>
.content {
  height: calc(100vh - 8rem);
  overflow-y: auto;
}
</style>
