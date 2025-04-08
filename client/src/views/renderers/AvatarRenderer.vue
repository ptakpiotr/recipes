<script setup lang="ts">
const props = defineProps<{
  params: {
    value: string;
  };
}>();
</script>
<template>
  <img
    :src="props.params.value"
    style="width: 2.5rem; height: 2.5rem"
    class="avatar-image"
    alt="Avatar użytkownika"
  />
</template>
