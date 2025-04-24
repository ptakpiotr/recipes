const OFFLINE_PAGE = "offline.html";
const CACHE_KEY = "OFFLINE_PAGE";

//https://googlechrome.github.io/samples/service-worker/custom-offline-page/
self.addEventListener("install", (evt) => {
  evt.waitUntil(
    caches.open(CACHE_KEY).then(
      (cache) => cache.add(new Request(OFFLINE_PAGE, { cache: "reload" })) //force response to be loaded from network
    )
  );
});

self.addEventListener("fetch", (event) => {
  event.respondWith(
    caches
      .match(event.request)
      .then((response) => {
        if ("setAppBadge" in navigator) {
          //https://developer.mozilla.org/en-US/docs/Web/API/Badging_API
          navigator.setAppBadge(1);
        }

        return response || fetch(event.request);
      })
      .catch(async (_) => {
        const cache = await caches.open(CACHE_KEY);
        const response = await cache.match(OFFLINE_PAGE);

        return response;
      })
  );
});
