import { createWebHistory, createRouter } from "vue-router";

import HomeView from "./views/HomeView.vue";
import AboutView from "./views/AboutView.vue";
import AdminView from "./views/AdminView.vue";
import AddRecipeView from "./views/AddRecipeView.vue";
import RecipeView from "./views/RecipeView.vue";

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
  {
    path: "/add",
    component: AddRecipeView,
  },
  {
    path: "/recipe/:id",
    component: RecipeView,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
