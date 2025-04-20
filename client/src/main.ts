import { createApp } from "vue";
import { createPinia } from "pinia";
import Toast from "vue-toastification";
import App from "./App.vue";
import router from "./router";
import { AllCommunityModule, ModuleRegistry } from "ag-grid-community";

import "vue-toastification/dist/index.css";

import "./style.css";

ModuleRegistry.registerModules([AllCommunityModule]);

const app = createApp(App);
const pinia = createPinia();

app.use(router);
app.use(pinia);
app.use(Toast);
app.mount("#app");

// service worker

if ("serviceWorker" in navigator) {
  window.addEventListener("load", async (_) => {
    try {
      if (import.meta.env?.DEV) {
        await navigator.serviceWorker.register("/serviceworker.js", {
          type: "module",
        });
      } else {
        await navigator.serviceWorker.register("/serviceworker.js");
      }
    } catch (err) {
      console.log(err);
      console.error("Couldn't register service worker");
    }
  });
}
