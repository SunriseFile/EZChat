import Vue from 'vue'
import App from './mainWindow.vue'

//stylesheet
require('./fonts/OpenSans-Regular.ttf');
require('./scss/style.scss');

new Vue({
  el: '#app',
  render(h) {
    return h(App)
  }
})
