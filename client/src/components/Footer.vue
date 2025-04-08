<script lang="ts">
import "leaflet/dist/leaflet.css";
import { LMap, LTileLayer } from "@vue-leaflet/vue-leaflet";
</script>
<template>
  <footer class="bg-green-300 p-4">
    <p>&copy; Piotr Ptak {{ new Date().getUTCFullYear() }} - Przepisy</p>
    <div style="height: 100px; width: 800px">
      <LMap ref="map" :center="[47.41322, -1.219482]">
        <LTileLayer
          :url="`https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png`"
          layer-type="base"
          name="OpenStreetMap"
        ></LTileLayer>
      </LMap>
    </div>
  </footer>
</template>
