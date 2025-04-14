import { defineStore } from "pinia";
import { ref } from "vue";
import type { UserBasicReadDto } from "../../Types";

export const useUsersStore = defineStore("users", () => {
  const users = ref<UserBasicReadDto[] | null>([]);
  const isAdmin = ref<boolean>(false);

  function setUsers(newUsers: UserBasicReadDto[]) {
    users.value = newUsers;
  }

  function setIsAdmin(admin: boolean) {
    isAdmin.value = admin;
  }

  return { users, isAdmin, setUsers, setIsAdmin };
});
