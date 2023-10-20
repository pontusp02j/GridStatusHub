const CACHE_NAME = 'version-1';

self.addEventListener('install', (event) => {
    console.log('Service worker installed');

    event.waitUntil(
        caches.open(CACHE_NAME)
            .then((cache) => {
                return fetch('asset-manifest.json')
                    .then((response) => response.json())
                    .then((assets) => {
                        const urlsToCache = [
                            '/',
                            'manifest.json',
                            'logo192.png',
                            'favicon.ico',
                            ...Object.values(assets).filter(url => typeof url === 'string' && !url.startsWith('chrome-extension://'))
                        ];
                        const requests = urlsToCache.map(url => fetch(url).then(response => {
                            if (!response.ok) {
                                throw new Error('Request failed: ' + url);
                            }
                            return cache.put(url, response);
                        }));
                        return Promise.all(requests);
                    });
            })
            .then(() => self.skipWaiting())
    );
});

self.addEventListener('fetch', (event) => {
    
    if (event.request.url.includes('sockjs-node')) 
    {
        return;
    }
    
    if (event.request.url.startsWith('chrome-extension://')) 
    {
        return;
    }

    event.respondWith(
        caches.match(event.request)
            .then((response) => {
                // Cache hit - return the response from the cached version
                if (response) {
                    return response;
                }

                // If not in cache, try to fetch from the network
                return fetch(event.request)
                    .then((networkResponse) => {
                        // If response is valid, put it in cache and return it
                        if (networkResponse && networkResponse.status === 200) {
                            let responseClone = networkResponse.clone();
                            caches.open(CACHE_NAME).then(cache => {
                                cache.put(event.request, responseClone);
                            });
                            return networkResponse;
                        }

                        // If network response wasn't ok, throw to go to the catch
                        throw new Error('Network response not OK');
                    })
                    .catch(() => {
                        // Network request failed, try to get it from the cache or fall back to offline.html
                        return caches.match(event.request)
                            .then((cacheResponse) => {
                                return cacheResponse || caches.match('offline.html');
                            });
                    });
            })
    );
});

self.addEventListener('activate', (event) => {
    console.log('Service worker activated');

    event.waitUntil(
        caches.keys().then((cacheNames) => {
            return Promise.all(
                cacheNames.map((cacheName) => {
                    if (cacheName !== CACHE_NAME) {
                        console.log('Service worker: Clearing old cache');
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});
