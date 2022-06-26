﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace UWPDemoApp.Services {

    public class HttpBase {

        private readonly Dictionary<string, object> _responseCache;

        private readonly HttpClient _client;

        public HttpBase(string defaultBaseUrl = "") {
            _client = new HttpClient();
            if (!string.IsNullOrEmpty(defaultBaseUrl)) _client.BaseAddress = new Uri($"{defaultBaseUrl}/");

            _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.60 Safari/537.36 Edg/100.0.1185.29");

            _responseCache = new Dictionary<string, object>();
        }

        public async Task<T> GetAsync<T>(string uri, string accessToken = null, bool forceRefresh = false) {
            T result = default;

            // The responseCache is a simple store of past responses to avoid unnecessary requests for the same resource.
            // Feel free to remove it or extend this request logic as appropraite for your app.
            if (forceRefresh || !_responseCache.ContainsKey(uri)) {
                SetAuthorizationHeader(accessToken);
                var json = await _client.GetStringAsync(uri);
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(json));

                if (_responseCache.ContainsKey(uri)) _responseCache[uri] = result;
                else _responseCache.Add(uri, result);
            } else {
                result = (T)_responseCache[uri];
            }

            return result;
        }

        public async Task<bool> PostAsync<T>(string uri, T item) {
            if (item == null) return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await _client.PostAsync(uri, byteContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PostAsJsonAsync<T>(string uri, T item) {
            if (item == null) return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await _client.PostAsync(uri, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync<T>(string uri, T item) {
            if (item == null) return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await _client.PutAsync(uri, byteContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsJsonAsync<T>(string uri, T item) {
            if (item == null) return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await _client.PutAsync(uri, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string uri) {
            var response = await _client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }

        private void SetAuthorizationHeader(string token) {
            if (string.IsNullOrEmpty(token)) {
                _client.DefaultRequestHeaders.Authorization = null;
                return;
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
