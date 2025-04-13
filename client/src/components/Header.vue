<script setup lang="ts">
import { ref } from "vue";
import { RouterLink, useRouter } from "vue-router";
import {
  MdHome,
  MdPerson,
  MdLogin,
  MdLogout,
  MdAdminPanelSettings,
  MdAdd
} from "vue-icons-plus/md";
import { serverUrl } from "../utils/envVars";

const router = useRouter();
const currentRoute = ref("");

router.afterEach(() => {
  currentRoute.value = router.currentRoute.value.path.replace("/", "");
});
</script>
<template>
  <header class="flex bg-green-300 p-5">
    <div class="app-header-name">
      <div class="logo">
        <p class="logo-text">Przepisy</p>
      </div>
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
        <li>
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
  </header>
</template>
<style scoped>
.app-header-name {
  flex: 1;
}

.logo {
  display: inline-block;
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
