<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { RouterLink, useRoute, useRouter } from "vue-router";
import {
  MdHome,
  MdPerson,
  MdLogin,
  MdLogout,
  MdAdminPanelSettings,
  MdAdd,
  MdSearch,
} from "vue-icons-plus/md";
import { serverUrl } from "../utils/envVars";
import { useUsersStore } from "../store/store";
import { storeToRefs } from "pinia";
import { POSITION, useToast } from "vue-toastification";

const router = useRouter();
const currentRoute = ref("");
const headerColor = ref("bg-green-300");
const store = useUsersStore();
const { isAdmin } = storeToRefs(store);
const route = useRoute();
const toast = useToast();

// https://stackoverflow.com/questions/189430/detect-if-the-internet-connection-is-offline
onMounted(() => {
  window.addEventListener("online", () => {
    headerColor.value = "bg-green-300";
  });

  window.addEventListener("offline", () => {
    headerColor.value = "bg-red-500";

    toast.error("Utracileś polaczenie z siecią", {
      draggable: true,
      position: POSITION.TOP_RIGHT,
    });
  });
});

router.afterEach(() => {
  currentRoute.value = router.currentRoute.value.path.replace("/", "");
});

const navigateToHome = () => {
  router.push("/");
};
</script>
<template>
  <header :class="['flex flex-col', headerColor]">
    <div class="app-header">
      <div class="logo cursor-pointer" @click="navigateToHome">
        <p class="logo-text">Przepisy</p>
      </div>
      <div class="app-header-search flex mt-4 items-center">
        <input class="p-1 rounded-md border border-slate-500 m-2" />
        <button
          class="search-btn bg-purple-500 p-1 rounded-md text-white cursor-pointer hover:bg-purple-700"
        >
          <MdSearch />
        </button>
      </div>
    </div>
    <div class="flex mt-2 p-5">
      <div class="app-header-name">
        <span
          v-if="route.query['filterType']"
          @click="navigateToHome"
          class="ml-4 text-sm bg-fuchsia-600 text-white rounded-full px-3 py-1 hover:bg-fuchsia-700 cursor-pointer"
        >
          {{ route.query["filterType"] }}
        </span>
      </div>

      <nav>
        <ul class="flex items-center justify-center mt-2">
          <li>
            <RouterLink to="/"
              ><p
                :class="`${'app-header-link'}${
                  currentRoute === '' ? ' active' : ''
                }`"
              >
                Strona domowa <MdHome /></p
            ></RouterLink>
          </li>
          <li>
            <RouterLink to="/about">
              <p
                :class="`${'app-header-link'}${
                  currentRoute === 'about' ? ' active' : ''
                }`"
              >
                O projekcie <MdPerson />
              </p>
            </RouterLink>
          </li>
          <li v-if="isAdmin">
            <RouterLink to="/admin">
              <p
                :class="`${'app-header-link'}${
                  currentRoute === 'admin' ? ' active' : ''
                }`"
              >
                Panel admina <MdAdminPanelSettings />
              </p>
            </RouterLink>
          </li>
          <li>
            <RouterLink to="/add">
              <p
                :class="`${'app-header-link'}${
                  currentRoute === 'add' ? ' active' : ''
                }`"
              >
                Dodaj przepis <MdAdd />
              </p>
            </RouterLink>
          </li>
          <li>
            <a :href="`${serverUrl}/login`">
              <p class="app-header-link"><MdLogin /></p>
            </a>
          </li>
          <li>
            <a :href="`${serverUrl}/logout`">
              <p class="app-header-link"><MdLogout /></p>
            </a>
          </li>
        </ul>
      </nav>
    </div>
  </header>
</template>
<style scoped>
.app-header {
  position: fixed;
  display: flex;
  left: env(titlebar-area-x, 0);
  top: env(titlebar-area-y, 0);
  width: env(titlebar-area-width, 100%);
  height: env(titlebar-area-height, 2.5rem);
  -webkit-app-region: drag;
  app-region: drag;
}

.app-header-search {
  -webkit-app-region: no-drag;
  app-region: no-drag;
}

.search-btn {
  margin-left: -2.5rem;
}

.app-header-name {
  flex: 1;
}

.logo {
  display: inline-block;
  padding: 0.5rem;
}

.logo-text {
  background: linear-gradient(135deg, #00fbff, #00ff26);
  border-radius: 1.5rem;
  padding: 0.25rem 0.5rem;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  font-size: 1rem;
  font-weight: bold;
  color: white;
  letter-spacing: 0.25rem;
  text-transform: uppercase;
  position: relative;
}

.logo-text::before {
  position: absolute;
}

li {
  @apply inline-block;
  padding-right: 0.5rem;
}

.app-header-link {
  display: flex;
  column-gap: 0.25rem;

  &:hover,
  &.active {
    opacity: 0.5;
  }
}
</style>
