<script setup lang="ts">
import axios from "axios";
import { MdDelete } from "vue-icons-plus/md";
import { useRouter } from "vue-router";

const props = defineProps<{
  params: {
    value: string;
  };
}>();

const router = useRouter();

const deleteUser = async () => {
  const canDelete = confirm("Czy jesteś pewny?");
  if (canDelete) {
    const res = await axios.delete(`/api/users/${props.params.value}`);
    if (res.status === 204) {
      router.go(0);
    }
  }
};
</script>
<template>
  <button class="text-red-500 cursor-pointer" @click="deleteUser">
    <MdDelete />
  </button>
</template>
