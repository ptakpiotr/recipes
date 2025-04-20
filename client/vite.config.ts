import { defineConfig } from "vite";
import path from "path";
import vue from "@vitejs/plugin-vue";
import tailwindcss from "@tailwindcss/vite";

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue(), tailwindcss()],
  server: {
    cors: {
      origin: ["https://localhost:7151"],
      methods: "*",
      credentials: true,
    },
    proxy: {
      "/api": {
        target: "https://localhost:7151/api",
        changeOrigin: true,
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, ""),
      },
    },
  },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "src"),
    },
  },
});
