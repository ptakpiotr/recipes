import { createWebHistory, createRouter } from "vue-router";

import HomeView from "./views/HomeView.vue";
import AboutView from "./views/AboutView.vue";
import AdminView from "./views/AdminView.vue";

const routes = [
  {
    path: "/",
    component: HomeView,
  },
  {
    path: "/about",
    component: AboutView,
  },
  {
    path: "/admin",
    component: AdminView,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
