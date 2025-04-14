import { defineStore } from "pinia";
import { ref } from "vue";
import type { UserReadDto } from "../../Types";

export const useUsersStore = defineStore("users", () => {
  const users = ref<UserReadDto[] | null>([]);
  const isAdmin = ref<boolean>(false);

  function setUsers(newUsers: UserReadDto[]) {
    users.value = newUsers;
  }

  function setIsAdmin(admin: boolean) {
    isAdmin.value = admin;
  }

  return { users, isAdmin, setUsers, setIsAdmin };
});
