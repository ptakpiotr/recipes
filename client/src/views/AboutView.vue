<script setup lang="ts">
import {
  SiCsharp,
  SiTypescript,
  SiVuedotjs,
  SiPostgresql,
} from "vue-icons-plus/si";
</script>
<template>
  <div>
    <h1 class="text-3xl text-bold">O Przepisach</h1>
    <br />
    <section class="about-section">
      <div class="about-text">
        <h2 class="text-xl">Witaj w Przepisach!</h2>
        <p>
          Przepisy to idealna aplikacja dla Ciebie, jeśli jesteś zainteresowany
          przygotowaniem posiłku
        </p>
        <p>
          Przeglądaj tysiące przepisów, od szybkich posiłków po wykwintne dania.
        </p>
        <p>
          Naszym celem jest uproszczenie gotowania i sprawienie, by było ono
          przyjemnością dla każdego, dzięki łatwym do wykonania instrukcjom.
        </p>
      </div>
      <div class="gallery">
        <div class="large-image">
          <img
            src="https://images.unsplash.com/photo-1611915365928-565c527a0590?q=80&w=1925&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            alt="Pizza"
          />
        </div>
        <div class="small-images">
          <div class="small-image">
            <img
              src="https://images.unsplash.com/photo-1586197132548-e2e19edf9f89?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
              alt="Lasagne"
            />
          </div>
          <div class="small-image">
            <img
              src="https://images.unsplash.com/photo-1662116663511-9d79d49da183?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
              alt="Pierogi"
            />
          </div>
        </div>
      </div>
    </section>

    <section class="technologies">
      <h2 class="text-xl">Wykorzystane technologie</h2>
      <ul class="technologies-list">
        <li><SiCsharp /></li>
        <li><SiTypescript /></li>
        <li><SiVuedotjs /></li>
        <li><SiPostgresql /></li>
      </ul>
    </section>
  </div>
</template>
<style scoped>
.technologies-list {
  display: inline-block;

  li {
    display: inline-block;
    padding: 0.5rem;
  }
}
.gallery {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 10px;
  max-width: 500px;
  margin: 50px auto;
}

.large-image {
  grid-column: span 2;
}

.large-image img {
  width: 100%;
  height: auto;
  display: block;
  border-radius: 8px;
}

.small-images {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 10px;
}

.small-image img {
  width: 100%;
  height: auto;
  display: block;
  border-radius: 8px;
}
</style>
