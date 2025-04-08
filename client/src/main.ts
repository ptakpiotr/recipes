import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { AllCommunityModule, ModuleRegistry } from "ag-grid-community";

import "./style.css";

ModuleRegistry.registerModules([AllCommunityModule]);

const app = createApp(App);

app.use(router);
app.mount("#app");
