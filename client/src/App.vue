<script setup lang="ts">
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import { RouterView } from "vue-router";
import { onMounted, onUnmounted } from "vue";
import { useUsersStore } from "./store/store";
import { POSITION, useToast } from "vue-toastification";
import axios from "axios";
import { serverUrl, vapidKey } from "./utils/envVars";
import type { UserBasicReadDto } from "../Types";
import { syncRecipesToBackend } from "./utils/syncRecipesHelpers";

const usersStore = useUsersStore();
const toast = useToast();

const syncData = async () => {
  await syncRecipesToBackend();
};

onMounted(async () => {
  try {
    const users = await axios.get<{
      value: UserBasicReadDto[];
    }>(`${serverUrl}/api/users/basic`);

    if (users.status === 200) {
      usersStore.setUsers(users.data.value);
      Notification.requestPermission().then(function (permission) {
        if (permission === "granted") {
          console.log("Notification permission granted.");
        } else {
          console.log("Notification permission denied.");
        }
      });

      navigator.serviceWorker.ready.then((registration) => {
        registration.pushManager
          .subscribe({
            userVisibleOnly: true,
            applicationServerKey: vapidKey,
          })
          .then((subscription) => {
            console.log("Push subscription:", subscription);
          })
          .catch((error) => {
            console.error("Push subscription failed:", error);
          });
      });
    }
  } catch (err) {
    toast.error("Wystąpil blad w trakcie pobierania informacji", {
      draggable: true,
      position: POSITION.TOP_RIGHT,
    });
  }

  try {
    window.addEventListener("online", syncData);
  } catch (err) {}

  try {
    const isAdmin = await axios.get<boolean>(`${serverUrl}/api/roles/admin`);

    if (isAdmin.status === 200) {
      usersStore.setIsAdmin(isAdmin.data);
    }
  } catch (err) {
    //user is not an admin
  }
});

onUnmounted(() => {
  window.removeEventListener("online", syncData);
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
  height: calc(100vh - 9rem);
  overflow-y: auto;
}

@media (display-mode: minimal-ui) {
  .content {
    display: none;
  }
}
</style>
