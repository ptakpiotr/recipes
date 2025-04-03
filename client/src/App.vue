<script setup lang="ts">
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import { RouterView } from "vue-router";
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
